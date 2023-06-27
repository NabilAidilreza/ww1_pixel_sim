using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankG : MonoBehaviour
{
    private Animator Anim;
    private float dmg;
    private float Bdmg;
    public float speed;
    private float DistanceToP;
    public float Longrange;
    public float Shortrange;
    public float MaxD;
    public float MinD;
    public float MaxDB;
    public float MinDB;
    private float timeT;
    private float timeTS;
    private float timeBtw;
    public float startTimeBtw;
    private float timeBtwS;
    public float startTimeBtwS;
    private float AttackRange;
    private float ran;
    public float attackRadius;
    public LayerMask WhatIsEnemies;
    public GameObject Burst;
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        ran = Random.Range(0, 2);
        timeTS = 5;
        timeT = timeTS;
        timeBtwS = startTimeBtwS;
        dmg = Random.Range(MinD, MaxD);
        Bdmg = Random.Range(MinDB, MaxDB);
        Anim = GetComponent<Animator>();
        AttackRange = Random.Range(Shortrange, Longrange); // CHANGE WHEN SHOOT
        timeBtw = startTimeBtw;

    }


    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        if (timeT <= 0)
        {
            AttackRange = Random.Range(Shortrange, Longrange);
            dmg = Random.Range(MinD, MaxD);
            Bdmg = Random.Range(MinDB, MaxDB);
            timeT = timeTS;
            ran = Random.Range(0, 2);

        }
        else
        {
            timeT -= Time.deltaTime;
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
            Anim.SetBool("Spray", false);
            Anim.SetBool("Boom", false);
        }
        else
        {
            transform.position = this.transform.position;
            if (ran == 0)
            {


                if (timeBtw <= 0)
                {
                    Anim.SetBool("Spray", false);
                    closestEnemy.TakeDamage(dmg);
                    timeBtw = startTimeBtw;
                    


                }
                else
                {
                    //Decrease Over Time
                    timeBtw -= Time.deltaTime;
                    Anim.SetBool("Spray", true);
                }
            }
            else
            {
                if (timeBtwS <= 0)
                {
                    Instantiate(Burst, closestEnemy.transform.position, Quaternion.identity);
                    Anim.SetBool("Boom", true);
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(closestEnemy.transform.position, attackRadius, WhatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if(enemiesToDamage[i].GetComponent<Enemy>() == null)
                        {
                            Debug.Log("Object not found");
                        }
                        else
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(Bdmg);
                        }
                    }
                    timeBtwS = startTimeBtwS;
                    

                }
                else
                {
                    //Decrease Over Time
                    timeBtwS -= Time.deltaTime;
                    Anim.SetBool("Boom", false);
                }
            }


        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);

    }


}