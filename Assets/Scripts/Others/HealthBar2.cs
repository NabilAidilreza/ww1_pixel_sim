using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar2 : MonoBehaviour
{
    Vector3 localscale;
    public int scale;
    // Start is called before the first frame update
    void Start()
    {
        localscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localscale.x = transform.parent.GetComponent<Enemy>().currhp / scale;
        transform.localScale = localscale;
    }
}
