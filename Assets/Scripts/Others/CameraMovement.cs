using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraMovement : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Level0");
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Time.timeScale = 2.0f;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Time.timeScale = 1.2f;
        }
        //Keyboard commands
        Vector3 p = GetBaseInput();

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        transform.Translate(p);

    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-20, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(20, 0, 0);
        }
        return p_Velocity;
    }
}