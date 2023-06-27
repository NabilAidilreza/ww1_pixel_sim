using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : MonoBehaviour
{
    // Basic Troop Variables //
    private Animator Anim;
    public float speed;
    private float DistanceToP;
    private bool ready;
    private bool flip;
    private bool injured;
    // Healing Variables //
    private float healValue;
    private float healRate;
    private float healRadius;
    public LayerMask WhatIsTargets;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        flip = false;
        // Healing Vars Instantiated //
        healValue = 5f;
        healRate = 0.1f;
        healRadius = 8.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Ally>().currhp < 20)
        {
            if (flip == false)
            {
                flip = true;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            injured = true;
            this.GetComponent<Ally>().Retreat();
            Anim.SetBool("Running", true);
            Anim.SetBool("Crouching", false);
        }
        else
        {
            if (flip == true)
            {
                flip = false;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            if (injured == false)
            {
                FindClosestAlly();
            }
            else
            {
                if (this.GetComponent<Ally>().currhp > 90)
                {
                    injured = false;
                }
                this.GetComponent<Ally>().Retreat();
            }

        }
        if (healRate <= 0)
        {
            MedicHeal();
            healRate = 0.1f;
        }
        else
        {
            healRate -= Time.deltaTime;
        }
    }
    void FindClosestAlly()
    {
        float DtoC = Mathf.Infinity;
        Ally closestAlly = null;
        Ally[] all = GameObject.FindObjectsOfType<Ally>();
        foreach (Ally curr in all)
        {
            if(curr.tag == "Troop")
            {
                float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
                if (DtoE < DtoC)
                {
                    DtoC = DtoE;
                    closestAlly = curr;
                }
            }
        }
        DistanceToP = Vector2.Distance(transform.position, closestAlly.transform.position);
        if (DistanceToP > (healRadius-4))
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
            transform.position = Vector2.MoveTowards(transform.position, closestAlly.transform.position, speed * Time.deltaTime);
            Anim.SetBool("Running", true);
            Anim.SetBool("Crouching", false);
        }
        else
        {
            transform.position = this.transform.position;
            Anim.SetBool("Running", false);
            Anim.SetBool("Crouching", true);
        }
        Debug.DrawLine(this.transform.position, closestAlly.transform.position);
    }
    void MedicHeal()
    {
        Collider2D[] targetsToHeal = Physics2D.OverlapCircleAll(this.transform.position, healRadius, WhatIsTargets);
        for (int i = 0; i < targetsToHeal.Length; i++)
        {
            targetsToHeal[i].GetComponent<Ally>().Heal(healValue);   
        }
    }
}
