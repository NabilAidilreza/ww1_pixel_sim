using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    public float health;
    public float currhp;
    private float armor;
    // Start is called before the first frame update
    void Start()
    {
        currhp = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currhp >= health)
        {
            currhp = health;
        }
        if (currhp <= 0)
        {
            StartCoroutine("Pause");
            Die();
        }
    }
    public void Retreat()
    {
        float DtoC = Mathf.Infinity;
        Ally closestTent = null;
        Ally[] objects = GameObject.FindObjectsOfType<Ally>();
        foreach (Ally obj in objects)
        {
            if (obj.tag == "MedicTent")
            {
                float DtoE = (obj.transform.position - this.transform.position).sqrMagnitude;
                if (DtoE < DtoC)
                {
                    DtoC = DtoE;
                    closestTent = obj;
                }
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, closestTent.transform.position, 2 * Time.deltaTime);
        if(transform.position == closestTent.transform.position)
        {
            Wait();
        }

    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.3f);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void Wait()
    {
        if(currhp < 90)
        {
            this.transform.position = this.transform.position;
        }
    }
    public void Heal(float healValue)
    {
        currhp += healValue;
    }
    public void TakeDamage(float dmg)
    {
        if (this.gameObject.tag == "Tank")
        {
            armor = Random.Range(10, 50);
            if (armor >= dmg)
            {
                currhp -= 0;
            }
            else
            {
                dmg = dmg - armor;
                currhp -= dmg;
            }
        }
        else
        {
            currhp -= dmg;
        }

    }

}
