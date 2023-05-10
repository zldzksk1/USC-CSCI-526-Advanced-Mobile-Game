using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static UnityEngine.Debug;

public class hintsyst1 : MonoBehaviour
{
    public Text hint;
    public float displayTime = 5f;
    public Text hint1;
    public Text hint2;
    public Text hint3;
    
    // Start is called before the first frame update
    void Start()
    {
        hint = GameObject.Find("hint").GetComponent<Text>();
        hint.enabled=false;
        hint1 = GameObject.Find("hint1").GetComponent<Text>();
        hint2 = GameObject.Find("hint2").GetComponent<Text>();
        hint3 = GameObject.Find("hint3").GetComponent<Text>();
        hint1.enabled = false;
        hint2.enabled = false;
        hint3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-Checkpoint.t>20.0f)
        {
        Checkpoint.t = Checkpoint.t + 20.0f;
        StartCoroutine(DisplayText());
        }

         if (Input.GetKeyDown(KeyCode.H) && Checkpoint.checkname == "Checkpoint1")
        {
            hint2.enabled = false;
            hint1.enabled = true;
            hint3.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.H) && Checkpoint.checkname == "Checkpoint1")
        {
            hint2.enabled = false;
            hint1.enabled = false;
            hint3.enabled = false;
        }
      
        if (Input.GetKeyDown(KeyCode.H) && Checkpoint.checkname == "Checkpoint2")
        {
            hint2.enabled = true;
            hint1.enabled = false;
            hint3.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.H) && Checkpoint.checkname == "Checkpoint2")
        {
            hint2.enabled = false;
            hint1.enabled = false;
            hint3.enabled = false;
        }
        
        if (Input.GetKeyDown(KeyCode.H) && Checkpoint.checkname == "initial")
        {
            hint2.enabled = false;
            hint1.enabled = false;
            hint3.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.H) && Checkpoint.checkname == "initial")
        {
            hint3.enabled = false;
            hint2.enabled = false;
            hint1.enabled = false;
        }

    }
    
      IEnumerator DisplayText()
    {
        hint.enabled = true;
        yield return new WaitForSeconds(displayTime);
        hint.enabled = false;
    }
}
