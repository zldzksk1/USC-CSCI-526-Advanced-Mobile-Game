using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update

    public float minimum = 0.0f;
    public float maximum = 100.0f;
    public Image mask;

    void Start()
    {
        mask = transform.Find("Mask").GetComponent<Image>();
    }

    public void SetProgressPercentage(float percent)
    {
        percent = Mathf.Clamp(percent, minimum, maximum);
        mask.fillAmount = percent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
