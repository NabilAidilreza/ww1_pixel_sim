using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip gun, mg, tank;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        tank = Resources.Load<AudioClip>("tank");
        gun = Resources.Load<AudioClip>("gun");
        mg = Resources.Load<AudioClip>("mg");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "gun":
                audioSrc.volume = 0.8f;
                audioSrc.PlayOneShot(gun);
                break;
            case "mg":
                audioSrc.volume = 0.2f;
                audioSrc.PlayOneShot(mg);
                break;
            case "tank":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(tank);
                break;
        }
    }
}
