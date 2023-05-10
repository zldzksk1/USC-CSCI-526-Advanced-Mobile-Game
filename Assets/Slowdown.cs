using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowdown : MonoBehaviour
{
    public bool shouldStopGame = false;

    bool IsTriggered = false;
    bool IsSlowed = false;
    public GameObject tutorialObject;
    public PlayerMovement playerMove;
    public string sendHandicapMessage;
    bool waitingInput = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waitingInput)
        {
            if (Input.GetKey(KeyCode.G))
            {
                waitingInput = false;
                tutorialObject?.SetActive(false);
                if (playerMove)
                    playerMove.movePlayer = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomb"))
            return;
        if (IsTriggered) 
            return;
        IsTriggered = true;

        if (shouldStopGame)
        {
            if (!tutorialObject) 
                return;
            tutorialObject.SetActive(true);
            if (playerMove)
            {

                playerMove.movePlayer = false;
                playerMove.transform.GetComponent<SwingController>()?.BreakRope();
                playerMove.transform.GetComponent<Rigidbody>().velocity = Vector3.zero; 
                playerMove.horizontalInput = 0;
                playerMove.verticalInput = 0;
            }
            waitingInput = true;
        }
        else
        {
            StartCoroutine(ShowPromptForTime(3));
        }

        if (sendHandicapMessage.Length > 0)
            Handicap.ReportMessageStatic(sendHandicapMessage);
    }

    IEnumerator ShowPromptForTime(float time)
    {
        tutorialObject?.SetActive(true);
        yield return new WaitForSeconds(time);
        tutorialObject?.SetActive(false);
    }


    // Obsolete - component was repurposed
    public IEnumerator SlowdownTrigger(float delay)
    {
        IsSlowed = false;
        tutorialObject.SetActive(true);
        StartCoroutine(SlowDown(0.5f));
        yield return new WaitUntil(() => IsSlowed == true);
        Debug.Log($"Waiting {delay} seconds.");
        yield return new WaitForSecondsRealtime(delay);
        Debug.Log($"Done waiting.");
        tutorialObject.SetActive(false);
        StartCoroutine(SpeedUp(1.0f));
    }

    public IEnumerator SlowDown(float length)
    {
        UnityEngine.Debug.Log("Starting slow down.");
        float timer = 0.0f;
        while (timer < length)
        {
            Time.timeScale = Mathf.Lerp(1.0f, 0.125f, timer / length);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 0.125f;
        IsSlowed = true;
        Debug.Log("Ending slow down.");
    }

    public IEnumerator SpeedUp(float length)
    {
        UnityEngine.Debug.Log("Starting speed up.");
        float timer = 0.0f;
        while (timer < length)
        {
            Time.timeScale = Mathf.Lerp(0.125f, 1.0f, timer / length);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1.0f;

        Debug.Log("Ending speed up.");
    }
}
