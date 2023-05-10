using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_boss : MonoBehaviour
{
    public TextMeshProUGUI hint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "Grapple")
        {
            hint.text = "Hint: Right Click to GRAPPLE POINT";
        }

        if (gameObject.name == "SwingPoint")
        {
            hint.text = "Hint: Press R to create GRAPPLE POINT";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (gameObject.name == "Grapple")
        {
            hint.text = "";
        }

        if (gameObject.name == "SwingPoint")
        {
            hint.text = "";
        }
    }
}
