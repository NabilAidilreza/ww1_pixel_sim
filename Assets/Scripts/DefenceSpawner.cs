using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceSpawner : MonoBehaviour
{
    public GameObject maxim;
    public GameObject arti;
    public GameObject mortar;
    public GameObject sandbag;
    private float spawnRadius = 10f;
    public LayerMask WhoIsWho;
    private float spawnCount = 7;
    private float MortarCount = 2;
    private float ArtiCount = 3;
    private int ran;
    // Start is called before the first frame update
    void Start()
    {
        SpawnDefence();
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 10f);
    }
    public void SpawnDefence()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = GetSpawnPosition();
            ran = Random.Range(0,3);
            if(spawnPos != Vector3.zero)
            {
                if(ran == 0)
                {
                    GameObject def = Instantiate(maxim, spawnPos, Quaternion.identity) as GameObject;
                }
                if (ran == 1)
                {
                    if(ArtiCount == 0)
                    {
                        Debug.Log("No more Arti");
                    }
                    else
                    {
                        GameObject def = Instantiate(arti, spawnPos, Quaternion.identity) as GameObject;
                        ArtiCount -= 1;
                    }
                }
                if (ran == 2)
                {
                    if(MortarCount == 0)
                    {
                        Debug.Log("No more Mortar");
                    }
                    else
                    {
                        GameObject def = Instantiate(mortar, spawnPos, Quaternion.identity) as GameObject;
                        MortarCount -= 1;
                    }
                }
                if(ran == 3)
                {
                    GameObject def = Instantiate(sandbag, spawnPos, Quaternion.identity) as GameObject;
                }
            }
        }
    }
    Vector2 GetSpawnPosition()
    {
        Vector2 spawnpos = new Vector3();
        float startTime = Time.realtimeSinceStartup;
        bool full = false;
        while(full == false)
        {
            Vector3 spawnPosRaw = Random.insideUnitCircle * spawnRadius;
            spawnpos = this.transform.position + spawnPosRaw;
            full = !Physics2D.OverlapCircle(spawnpos, 3.0f, WhoIsWho);
            if(Time.realtimeSinceStartup - startTime > 0.2f)
            {
                return Vector3.zero;
            }
        }
        return spawnpos;
    }
}
