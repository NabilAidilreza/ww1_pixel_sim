using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Blowup");
     
    }
    IEnumerator Blowup()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
