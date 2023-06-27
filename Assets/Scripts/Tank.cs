using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
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
        FindClosestAlly();
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
    void FindClosestAlly()
    {
        float DtoC = Mathf.Infinity;
        Ally closestAlly = null;
        Ally[] lall = GameObject.FindObjectsOfType<Ally>();
        foreach (Ally curr in lall)
        {
            float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
            if (DtoE < DtoC)
            {
                DtoC = DtoE;
                closestAlly = curr;
            }
        }
        DistanceToP = Vector2.Distance(transform.position, closestAlly.transform.position);
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
                if (hit.GetComponent<Enemy>() != null && hit.transform != transform)
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
            transform.position = Vector2.MoveTowards(transform.position, closestAlly.transform.position, speed * Time.deltaTime);
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
                    closestAlly.TakeDamage(dmg);
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
                    Instantiate(Burst, closestAlly.transform.position, Quaternion.identity);
                    Anim.SetBool("Boom", true);
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(closestAlly.transform.position, attackRadius, WhatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Ally>().TakeDamage(Bdmg);
                    }
                    SoundManagerScript.PlaySound("tank");
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
        Debug.DrawLine(this.transform.position, closestAlly.transform.position);

    }


}