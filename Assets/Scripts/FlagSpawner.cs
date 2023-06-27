using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    public GameObject infantry;
    public GameObject gunner;
    public GameObject officer;
    public GameObject sniper;
    public GameObject tank;
    public GameObject specialunit1;
    public GameObject specialunit2;
    public GameObject specialunit3;
    public float duration;
    private float resetValue;
    private float tankSpawn;
    private float tankReset;
    private int ran;
    // Start is called before the first frame update
    void Start()
    {
        resetValue = duration;
        tankReset = 150;
        tankSpawn = tankReset;

        ran = Random.Range(1,6);
    }

    // Update is called once per frame
    void Update()
    {
        if(tankSpawn <= 0)
        {
            Instantiate(tank, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
            tankSpawn = tankReset;
        }
        else
        {
            tankSpawn -= Time.deltaTime;
        }
        if (GameObject.FindGameObjectsWithTag("Troop").Length < 50)
        {
            if (duration <= 0)
            {
                if (ran == 1)
                {
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                }
                if (ran == 2)
                {
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(gunner, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(gunner, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(gunner, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(gunner, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                }
                if (ran == 3)
                {
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(officer, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(sniper, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(sniper, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                }
                if (ran == 4)
                {
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(sniper, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(sniper, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(gunner, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(sniper, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                }
                if (ran == 5)
                {
                    Instantiate(specialunit1, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(specialunit1, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(specialunit2, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(specialunit2, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(specialunit2, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                    Instantiate(specialunit3, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
                }
                ran = Random.Range(1, 5);
                duration = resetValue;
            }
            else
            {
                duration -= Time.deltaTime;
            }
        }
    }
    public void Spawn_Infantry()
    {
        Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
        Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
        Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
        Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
        Instantiate(infantry, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
    }
    public void Spawn_Gunner()
    {
        Instantiate(gunner, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
        Instantiate(gunner, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
    }
    public void Spawn_Officer()
    {
        Instantiate(officer, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
    }
    public void Spawn_Sniper()
    {
        Instantiate(sniper, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
        Instantiate(sniper, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
    }
    public void Spawn_Tank()
    {
        Instantiate(tank, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
    }
    public void Spawn_Special1()
    {
        Instantiate(specialunit1, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
        Instantiate(specialunit1, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
    }
    public void Spawn_Special2()
    {
        Instantiate(specialunit2, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
        Instantiate(specialunit2, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
    }
    public void Spawn_Special3()
    {
        Instantiate(specialunit3, new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
    }

}
