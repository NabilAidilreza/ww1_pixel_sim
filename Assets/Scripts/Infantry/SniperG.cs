﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperG : MonoBehaviour
{
    private Animator Anim;
    private float dmg;
    public float speed;
    private float DistanceToP;
    public float Longrange;
    public float Shortrange;
    public float MaxD;
    public float MinD;
    private float timeBtw;
    private float startTimeBtw;
    private float timeBtwS;
    public float startTimeBtwS;
    private float AttackRange;
    // Start is called before the first frame update
    void Start()
    {
        
        startTimeBtw = startTimeBtwS - (1 / 2);
        timeBtwS = startTimeBtwS;
        dmg = Random.Range(MinD, MaxD);
        Anim = GetComponent<Animator>();
        AttackRange = Random.Range(Shortrange, Longrange); // CHANGE WHEN SHOOT
        timeBtw = startTimeBtw;

    }


    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        if (timeBtw <= 0)
        {
            AttackRange = Random.Range(Shortrange, Longrange);
            dmg = Random.Range(MinD, MaxD);
            timeBtw = startTimeBtw;

        }
        else
        {
            //Decrease Over Time
            timeBtw -= Time.deltaTime;
        }




    }
    void FindClosestEnemy()
    {
        float DtoC = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] lall = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy curr in lall)
        {
            float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
            if (DtoE < DtoC)
            {
                DtoC = DtoE;
                closestEnemy = curr;
            }
        }
        int strength = 1 / 2;
        transform.rotation = Quaternion.Lerp(transform.rotation, (Quaternion.LookRotation(closestEnemy.transform.position - transform.position)), Mathf.Min(strength * Time.deltaTime, 1));
        DistanceToP = Vector2.Distance(transform.position, closestEnemy.transform.position);
        if (DistanceToP > AttackRange)
        {
            // Grouping Behaviour //
            float separateSpeed = 1;
            float separateRadius = 1f;
            Vector2 sum = Vector2.zero;
            float count = 0f;
            // Sphere For Check Ally
            var hits = Physics2D.OverlapCircleAll(transform.position, separateRadius);
            foreach (var hit in hits)
            {
                if (hit.GetComponent<Ally>() != null && hit.transform != transform)
                {
                    Vector2 difference = transform.position - hit.transform.position;
                    difference = difference.normalized / Mathf.Abs(difference.magnitude);
                    sum += difference;
                    count++;
                }
            }
            if (count > 0)
            {
                sum /= count;
                sum = sum.normalized * separateSpeed;
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)sum, separateSpeed * Time.deltaTime);
            }
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, speed * Time.deltaTime);
            Anim.SetBool("Running", true);
            Anim.SetBool("Crouching", false);
            Anim.SetBool("CShooting", false);
        }
        else
        {
            transform.position = this.transform.position;
            Anim.SetBool("Crouching", true);
            
            Anim.SetBool("Running", false);
            if (timeBtwS <= 0)
            {
                Anim.SetBool("CShooting", true);
                if (closestEnemy.tag == "Tank")
                {
                    dmg -= 50;
                    closestEnemy.TakeDamage(dmg);
                }
                else
                {
                    closestEnemy.TakeDamage(dmg);
                }
                timeBtwS = startTimeBtwS;
                
            }
            else
            {
                //Decrease Over Time
                timeBtwS -= Time.deltaTime;
                Anim.SetBool("CShooting", false);
            }



        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);

    }


}
