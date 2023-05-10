using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int multiplier = 2;

    Transform multiplySign;
    Transform number;
    Collider collider;
    MathManager mathManager;

    private const float timeToReset = 5f;

    private void Awake()
    {
        multiplySign = transform.GetChild(0);
        number = transform.GetChild(1);
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        mathManager = MathManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mathManager)
            mathManager.Multiplier = multiplier;
        StartCoroutine(DelayedReset());
    }

    IEnumerator DelayedReset()
    {
        multiplySign?.gameObject?.SetActive(false);
        number?.gameObject?.SetActive(false);
        if (collider) collider.enabled = false;

        yield return new WaitForSeconds(timeToReset);

        multiplySign?.gameObject?.SetActive(true);
        number?.gameObject?.SetActive(true);
        if (collider) collider.enabled = true;
    }
}
