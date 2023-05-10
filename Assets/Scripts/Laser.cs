using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public float onTime = 2.0f;
    public float offTime = 2.0f;

    private bool isOn;
    private float timer = 0.0f;
    
    private LineRenderer mLine;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        mLine = GetComponent<LineRenderer>();
        mLine.SetPosition(0, start.transform.position);
        mLine.SetPosition(1, end.transform.position);

        audio = GetComponent<AudioSource>();
    }
    
    void FixedUpdate()
    {
        if (isOn)
        {
            RaycastHit hit;
            var dir = end.transform.position - start.transform.position;
            dir.Normalize();
            if (Physics.Raycast(start.transform.position, dir, out hit))
            {
                if (hit.collider)
                {
                    if (hit.collider.gameObject.transform.CompareTag("Player"))
                    {
                        UnityEngine.Debug.Log("laser hit: " + mLine.gameObject.transform.parent.gameObject.name);
                        Manager.Instance.allLasers[mLine.gameObject.transform.parent.gameObject.name] += 1;
                        audio.Play();
                        Manager.Instance.RespawnPlayer();
                    }
                }
            }

            timer += Time.deltaTime;
            if (timer >= onTime)
            {
                mLine.enabled = false;
                isOn = false;
                timer = 0.0f;
            }
        }
        else // off
        {
            timer += Time.deltaTime;
            if (timer >= offTime)
            {
                mLine.enabled = true;
                isOn = true;
                timer = 0.0f;
            }
        }
    }
}
