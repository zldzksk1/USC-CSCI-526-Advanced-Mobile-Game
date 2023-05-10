using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreakTimer : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField]
    private float timeToBreak;
    [SerializeField]
    private float warningTime;
    private bool isTicking = false;
    private bool showWarning = false;
    private bool playerOnBox = false;
    private SwingController controller;

    private const float timeToReset = 5f;

    private MeshRenderer renderer;
    private Collider collider;
    private Color c_Opaque = new Color(1, 1, 1, 1);
    private Color c_Transp = new Color(1, 1, 1, .3f);

    public void StartTicking(SwingController swingController)
    {
        isTicking = true;
        controller = swingController;
    }

    public void StopTicking()
    {
        isTicking = false;
        showWarning = false;
        timer = timeToBreak;
    }

    private void Awake()
    {
        timer = timeToBreak;
    }

    private void Start()
    {
        if (!TryGetComponent<MeshRenderer>(out renderer))
        {
            Debug.LogError(gameObject.name + " has no MeshRenderer Component.");
        }
        if (!TryGetComponent<Collider>(out collider))
        {
            Debug.LogError(gameObject.name + " has no Collider Component.");
        }
        collider.isTrigger = true; // set collider to trigger
    }

    private void Update()
    {
        if (playerOnBox && !isTicking)
        {
            isTicking = true;
        }
        if (!isTicking)
        {
            if (timer < timeToBreak) timer += Time.deltaTime;
            return;
        }

        if (controller != null && !controller.IsSwinging)
        {
            StopTicking();
            return;
        }

        timer -= Time.deltaTime;
        if (!showWarning && timer <= warningTime)
        {
            showWarning = true;
            StartCoroutine(WarningLoop());
        }
        if (timer <= 0f)
        {
            controller?.BreakRope();
            StartCoroutine(DelayedReset());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnBox = false;
        }
    }

    IEnumerator WarningLoop()
    {
        while (showWarning)
        {
            if(renderer.material.GetColor("_BaseColor") != null)
            {
                renderer.material.SetColor("_BaseColor", (renderer.material.GetColor("_BaseColor").a == 1.0f) ? c_Transp : c_Opaque);
            }
            else if(renderer.material.GetColor("_Color") != null)
            {
                renderer.material.SetColor("_Color", (renderer.material.GetColor("_Color").a == 1.0f) ? c_Transp : c_Opaque);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator DelayedReset()
    {
        renderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(timeToReset);
        renderer.material.color = c_Opaque;
        renderer.enabled = true;
        collider.enabled = true;
    }
}
