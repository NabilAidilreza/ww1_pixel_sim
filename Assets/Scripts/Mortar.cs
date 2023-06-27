using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    private Animator Anim;
    private float dmg;
    public float speed;
    private float DistanceToP;
    public float Longrange;
    public float Shortrange;
    public float MaxD;
    public float MinD;
    private float reloadtime;
    public float startreloadtime;
    private float shoottime;
    private float startshoottime;
    private float AttackRange;
    public float attackRadius;
    public LayerMask WhatIsEnemies;
    public GameObject Burst;
    public Transform target;
    public int spread;
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        
        reloadtime = startreloadtime;
        startshoottime = startreloadtime - (1 / 2);
        shoottime = startshoottime;
        dmg = Random.Range(MinD, MaxD);
        Anim = GetComponent<Animator>();
        AttackRange = Random.Range(Shortrange, Longrange); // CHANGE WHEN SHOOT

    }


    // Update is called once per frame
    void Update()
    {
        FindClosestAlly();
        if (shoottime <= 0)
        {
            AttackRange = Random.Range(Shortrange, Longrange);
            dmg = Random.Range(MinD, MaxD);
            shoottime = startshoottime;


        }
        else
        {
            //Decrease Over Time
            shoottime -= Time.deltaTime;


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
            transform.position = this.transform.position;
            Anim.SetBool("Firing", false);
            Anim.SetBool("Reloading", true);
        }
        else
        {
            transform.position = this.transform.position;

            if (reloadtime <= 0)
            {
                target.position = new Vector3(closestAlly.transform.position.x + Random.Range(-spread, spread), closestAlly.transform.position.y + Random.Range(-spread, spread), closestAlly.transform.position.z + Random.Range(-spread, spread));
                Instantiate(Burst, target.position, Quaternion.identity);
                Anim.SetBool("Firing", true);
                Anim.SetBool("Reloading", false);
                
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(target.position, attackRadius, WhatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if(enemiesToDamage[i].tag == "Tank")
                    {
                        enemiesToDamage[i].GetComponent<Ally>().TakeDamage(dmg+50);
                    }
                    else
                    {
                        enemiesToDamage[i].GetComponent<Ally>().TakeDamage(dmg);
                    }
                    
                }
                SoundManagerScript.PlaySound("tank");
                reloadtime = startreloadtime;
            }
            else
            {
                reloadtime -= Time.deltaTime;
                Anim.SetBool("Firing", false);
                Anim.SetBool("Reloading", true);
            }



        }
        Debug.DrawLine(this.transform.position, closestAlly.transform.position);

    }


}
