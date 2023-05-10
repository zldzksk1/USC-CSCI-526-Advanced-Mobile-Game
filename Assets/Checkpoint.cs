using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;
    public bool Activated { get { return activated; } }
    public GameObject spawnPoint;
    public Checkpoint prevPoint;
    private MeshRenderer renderer;
    private Transform flagTransform;
    private float startTime;
    public static string checkpointname;
    public static string checkname = "initial";
    public static string checkpointTimer;
    private Color c_activated = new Color(0, 183f / 255f, 77f / 255f);
    public static float t = 0.0f;

    private int hintCount = 0;
    public static string hintString;

    void Start()
    { 
        startTime = Time.time;

        {
            Transform flagParent = transform.Find("Flag");
            if (flagParent)
            {
                Vector3 parentScale = transform.localScale;
                flagParent.localScale = new Vector3(8f / parentScale.x, 1f, 8f / parentScale.z);
                flagTransform = flagParent.Find("Flag");
            }
        }
    }

    public void ActivateCheckpoint()
    {
        if (activated) return;
        prevPoint?.ActivateCheckpoint();

        activated = true;
        if (spawnPoint != null)
        {
            spawnPoint.transform.position = transform.position + new Vector3(0, 2f, 0);
        }

        if (TryGetComponent<MeshRenderer>(out renderer))
        {
            renderer.material.color = c_activated;
        }

        StartCoroutine(RaiseFlag(5.8f, .3f, 0.02f));

        // Get the name of the checkpoint
        string tempcheckpointName = gameObject.name;
        checkname = tempcheckpointName;
        checkpointname = checkpointname + "," + tempcheckpointName;
        // Calculate the time taken to reach the checkpoint
        float timeTaken = Time.time - startTime;
        t = timeTaken;
        Debug.Log("Timetaken:"+ timeTaken+", Time.time"+Time.time+", starttime"+startTime);
        checkpointTimer = checkpointTimer + "," + timeTaken.ToString();
        Debug.Log("TIME SCALE:"+ Time.timeScale);

        Debug.Log("Time taken to reach checkpoint " + tempcheckpointName + ": " + timeTaken + " seconds");
        Manager.Instance.checkpointTimes[tempcheckpointName] = timeTaken;
    }

        void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hintCount++;
            float timeTakenforHint = Time.time - startTime;
            Vector3 playerPos = transform.position;
            Debug.Log("Hint requested at " + timeTakenforHint + " seconds at position (" + playerPos.x + ", " + playerPos.y + ", " + playerPos.z + ")");
            Debug.Log("Hint count: " + hintCount);


            hintString = hintString + "," + hintCount.ToString() + "_" + timeTakenforHint.ToString() + "_" + "(" + playerPos.x + ", " + playerPos.y + ", " + playerPos.z + ")";
            //Debug.Log("Hint String : "+ hintString);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

            ActivateCheckpoint();
    }

    IEnumerator RaiseFlag(float targetY, float time, float frame)
    {
        if (flagTransform)
        {
            float timer = 0f;
            float currY = flagTransform.localPosition.y;
            float currX = flagTransform.localPosition.x;
            while (timer < time)
            {
                timer = Mathf.Min(timer + frame, time);
                flagTransform.localPosition = new Vector3(currX, Mathf.Lerp(currY, targetY, timer / time), 0f);
                yield return new WaitForSeconds(frame);
            }
        }
    }
}
