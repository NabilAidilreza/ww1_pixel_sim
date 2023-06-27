using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Officer : MonoBehaviour
{
    private Animator Anim;
    private float timeBtw;
    private float startTimeBtw;
    private float AttackRange;
    private float DistanceToP;
    public float Longrange;
    public float Shortrange;
    private float dmg;
    public float speed;
    private float ran;
    public float MaxD;
    public float MinD;
    private float timeBtwS;
    public float startTimeBtwS;
    public GameObject Infantry;
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        ran = Random.Range(0, 2);
        startTimeBtw = 8;
        timeBtwS = startTimeBtwS;
        dmg = Random.Range(MinD, MaxD);
        Anim = GetComponent<Animator>();
        AttackRange = Random.Range(Shortrange, Longrange); // CHANGE WHEN SHOOT
        timeBtw = startTimeBtw;

    }


    // Update is called once per frame
    void Update()
    {
        FindClosestAlly();
        if (timeBtw <= 0)
        {
            AttackRange = Random.Range(Shortrange, Longrange);
            dmg = Random.Range(MinD, MaxD);
            Instantiate(Infantry, Pos1.position, Quaternion.identity);
            Instantiate(Infantry, Pos2.position, Quaternion.identity);
            Instantiate(Infantry, Pos3.position, Quaternion.identity);
            timeBtw = startTimeBtw;

        }
        else
        {
            //Decrease Over Time
            timeBtw -= Time.deltaTime;
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
        int strength = 1 / 2;
        transform.rotation = Quaternion.Lerp(transform.rotation, (Quaternion.LookRotation(closestAlly.transform.position - transform.position)), Mathf.Min(strength * Time.deltaTime, 1));
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
            Anim.SetBool("Running", true);
            Anim.SetBool("Shooting", false);
            Anim.SetBool("Crouching", false);
            Anim.SetBool("CShooting", false);
        }
        else
        {
            transform.position = this.transform.position;
            if (ran == 0)
            {

                Anim.SetBool("Running", false);
                if (timeBtwS <= 0)
                {
                    Anim.SetBool("Shooting", true);
                    closestAlly.TakeDamage(dmg);
                    timeBtwS = startTimeBtwS;
                    ran = Random.Range(0, 2);


                }
                else
                {
                    //Decrease Over Time
                    timeBtwS -= Time.deltaTime;
                    Anim.SetBool("Shooting", false);
                }
            }
            else
            {
                Anim.SetBool("Crouching", true);

                Anim.SetBool("Running", false);
                if (timeBtwS <= 0)
                {
                    Anim.SetBool("CShooting", true);
                    closestAlly.TakeDamage(dmg);
                    timeBtwS = startTimeBtwS;
                    ran = Random.Range(0, 2);

                }
                else
                {
                    //Decrease Over Time
                    timeBtwS -= Time.deltaTime;
                    Anim.SetBool("CShooting", false);
                }
            }


        }
        Debug.DrawLine(this.transform.position, closestAlly.transform.position);

    }


}
