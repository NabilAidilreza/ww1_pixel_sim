using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTent : MonoBehaviour
{
    public bool isEnemy;
    private float healValue;
    private float healRate;
    private float healRadius;
    public LayerMask WhatIsTargets;
    // Start is called before the first frame update
    void Start()
    {
        healValue = 10f;
        healRate = 0.01f;
        healRadius = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(healRate <= 0)
        {
            Collider2D[] targetsToHeal = Physics2D.OverlapCircleAll(this.transform.position, healRadius, WhatIsTargets);
            for (int i = 0; i < targetsToHeal.Length; i++)
            {
                targetsToHeal[i].GetComponent<Ally>().Heal(healValue);
            }
            healRate = 0.1f;
        }
        else
        {
            healRate -= Time.deltaTime;
        }
    }
}
