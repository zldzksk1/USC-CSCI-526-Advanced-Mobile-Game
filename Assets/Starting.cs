using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject controlsUI;

    void Start()
    {
        Manager.Instance.canStart = false;
        controlsUI = GameObject.Find("UI").transform.Find("ControlsUI").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            controlsUI.SetActive(false);
            Manager.Instance.canStart = true;
        }
    }
}
