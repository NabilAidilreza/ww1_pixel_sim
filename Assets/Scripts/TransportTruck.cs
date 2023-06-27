using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportTruck : MonoBehaviour
{
    private Animator Anim;
    private float timeBtw;
    private float startTimeBtw;
    private float AttackRange;
    private float DistanceToP;
    public float Longrange;
    public float Shortrange;
    public float speed;
    private float ran;
    private float timeBtwS;
    public float startTimeBtwS;
    public GameObject Infantry;
    public GameObject Gunner;
    public GameObject Sniper;
    public GameObject Medic;
    public GameObject AtGunner;
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;
    public Transform Pos4;
    public Transform Pos5;
    public Transform Pos6;
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        ran = Random.Range(0, 2);
        timeBtwS = startTimeBtwS;
        Anim = GetComponent<Animator>();
        AttackRange = Random.Range(Shortrange, Longrange); // CHANGE WHEN SHOOT
        timeBtw = startTimeBtwS;

    }


    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        if (timeBtw <= 0)
        {
            AttackRange = Random.Range(Shortrange, Longrange);
            Instantiate(Infantry, Pos1.position, Quaternion.identity);
            Instantiate(Gunner, Pos6.position, Quaternion.identity);
            Instantiate(Sniper, Pos3.position, Quaternion.identity);
            Instantiate(AtGunner, Pos4.position, Quaternion.identity);
            Instantiate(Medic, Pos5.position, Quaternion.identity);
            Instantiate(Infantry, Pos2.position, Quaternion.identity);
            timeBtw = startTimeBtwS; 

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
        Enemy[] all = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy curr in all)
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
        }
        else
        {
            transform.position = this.transform.position;
        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }


}
