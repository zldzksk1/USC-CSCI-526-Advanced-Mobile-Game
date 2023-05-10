using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowmo : MonoBehaviour
{
     public float slowDownFactor = 0.2f;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = slowDownFactor;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            Time.timeScale = 1f;
        }
    }
}
