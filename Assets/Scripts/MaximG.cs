using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaximG : MonoBehaviour
{
    private Animator Anim;
    private float dmg;
    public float speed;
    private float DistanceToP;
    public float Longrange;
    public float Shortrange;
    public float MaxD;
    public float MinD;
    private float timeBtwS;
    public float startTimeBtwS;
    private float AttackRange;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwS = startTimeBtwS;
        dmg = Random.Range(MinD, MaxD);
        Anim = GetComponent<Animator>();
        AttackRange = Random.Range(Shortrange, Longrange); // CHANGE WHEN SHOOT
    }


    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        AttackRange = Random.Range(Shortrange, Longrange);
        dmg = Random.Range(MinD, MaxD);

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
            transform.position = this.transform.position;
            Anim.SetBool("Reloading", true);
            Anim.SetBool("Firing", false);
        }
        else
        {
            transform.position = this.transform.position;
            Anim.SetBool("Reloading", false);
            Anim.SetBool("Firing", true);
            if (timeBtwS <= 0)
            {
                closestEnemy.TakeDamage(dmg);
                timeBtwS = startTimeBtwS;

            }
            else
            {
                //Decrease Over Time
                timeBtwS -= Time.deltaTime;

            }



        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);

    }


}
