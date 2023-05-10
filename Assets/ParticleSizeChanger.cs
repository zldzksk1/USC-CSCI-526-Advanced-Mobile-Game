using UnityEngine;
using System.Collections;

public class ParticleSizeChanger : MonoBehaviour
{
    public float expandDistance = 5f; // distance at which to expand
    public float startScale = 1f;
    public float endScale = 5f;
    public float duration = 5f;
    private bool shouldStop = false;

    //private Rigidbody rb;
    private float distanceTraveled = 0f;
    private Vector3 lastPosition;

    void Start()
    {
        /*rb = GetComponent<Rigidbody>();
        lastPosition = rb.position;*/
    }

    void FixedUpdate()
    {
        // calculate distance traveled since last frame
        /*distanceTraveled += Vector3.Distance(rb.position, lastPosition);
        lastPosition = rb.position;

        // check if distance threshold has been met
        if (distanceTraveled >= expandDistance)
        {
            distanceTraveled = 0f;
            rb.isKinematic = true;
            shouldStop = true;
            StartCoroutine(ChangeScale());
        }*/
        StartCoroutine(ChangeScale());
    }
    IEnumerator ChangeScale()
    {
        float timer = 0f;
        

        while (timer < duration)
        {
            float t = timer / duration;
            float currentScale = Mathf.Lerp(startScale, endScale, t);
            transform.localScale = new Vector3(currentScale*3.0f, currentScale*3.0f, currentScale*3.0f);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(endScale, endScale, endScale);
    }
    
}

