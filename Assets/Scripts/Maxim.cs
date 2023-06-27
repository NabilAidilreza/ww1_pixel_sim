using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maxim : MonoBehaviour
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
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        timeBtwS = startTimeBtwS;
        dmg = Random.Range(MinD, MaxD);
        Anim = GetComponent<Animator>();
        AttackRange = Random.Range(Shortrange, Longrange); // CHANGE WHEN SHOOT
        startTimeBtw = 1;
        timeBtw = startTimeBtw;
    }


    // Update is called once per frame
    void Update()
    {
        FindClosestAlly();
        AttackRange = Random.Range(Shortrange, Longrange);
        dmg = Random.Range(MinD, MaxD);




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
                closestAlly.TakeDamage(dmg);
                timeBtwS = startTimeBtwS;

            }
            else
            {
                //Decrease Over Time
                timeBtwS -= Time.deltaTime;

            }
            if(timeBtw <= 0)
            {
                SoundManagerScript.PlaySound("mg");
                timeBtw = 3/2;
            }
            else
            {
                timeBtw -= Time.deltaTime;
            }


        }
        Debug.DrawLine(this.transform.position, closestAlly.transform.position);

    }


}
