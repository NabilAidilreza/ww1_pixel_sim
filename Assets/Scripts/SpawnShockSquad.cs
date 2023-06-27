using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShockSquad : MonoBehaviour
{
    private float hp;
    public GameObject ShockFlame;
    public GameObject ShockShotGun;
    private float timeBtwS;
    private float startTimeBtwS;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;
    // Start is called before the first frame update
    void Start()
    {
        startTimeBtwS = 7.0f;
        timeBtwS = startTimeBtwS;
        hp = GetComponent<Enemy>().currhp;
    }

    // Update is called once per frame
    void Update()
    {
        hp = GetComponent<Enemy>().currhp;
        if (hp <= 200)
        {
            if(timeBtwS <= 0)
            {
                Instantiate(ShockShotGun, pos1.position, Quaternion.identity);
                Instantiate(ShockShotGun, pos2.position, Quaternion.identity);
                Instantiate(ShockShotGun, pos3.position, Quaternion.identity);
                Instantiate(ShockFlame, pos4.position, Quaternion.identity);
                Instantiate(ShockFlame, pos5.position, Quaternion.identity);
                timeBtwS = startTimeBtwS;
            }
            else
            {
                timeBtwS -= Time.deltaTime;
            }
        }
    }
}
