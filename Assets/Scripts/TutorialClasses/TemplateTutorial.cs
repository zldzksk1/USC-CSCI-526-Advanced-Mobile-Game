using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TemplateTutorial : MonoBehaviour
{
    bool hasTriggered = false;
    bool playingPrompt = false;
    bool playingComplete = false;
    protected bool inProgress = false;

    [Header("General")]
    // if shouldPauseGame set to false, tutorial will not listen to player input
    public bool shouldPauseGame;
    public PlayerMovement playerMove;
    public List<GameObject> tutorialPrompt;
    public GameObject alwaysShow;
    public KeyCode keyForPrompt = KeyCode.G;
    public bool pauseGameOnComplete;
    public GameObject completedMsg;
    int current = -1;

    protected virtual void OnTutorialBegin()
    {
        if (shouldPauseGame)
        {
            if (tutorialPrompt.Count < 1)
                return;
            current = 0;
            alwaysShow?.SetActive(true);
            tutorialPrompt[current]?.SetActive(true);
            if (playerMove)
            {
                playerMove.movePlayer = false;
                playerMove.transform.GetComponent<SwingController>()?.BreakRope();
                playerMove.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                playerMove.horizontalInput = 0;
                playerMove.verticalInput = 0;
            }
            playingPrompt = true;
        }
        else
        {
            current = 0;
            StartCoroutine(ShowPromptForTime(3));
        }
    }

    protected virtual void OnPromptFinished()
    {
        alwaysShow?.SetActive(false);
        playingPrompt = false;
        inProgress = true;
    }

    protected virtual void OnTutorialComplete()
    {
        inProgress = false;
        if (completedMsg)
        {
            if (pauseGameOnComplete)
            {
                completedMsg.SetActive(true);
                playingComplete = true;
            }
            else
                StartCoroutine(ShowCompletedMsg(3));
        }
    }

    protected virtual void OnAllComplete()
    {
        completedMsg?.SetActive(false);
        playingComplete = false;
    }

    protected virtual void Update()
    {
        if (playingPrompt)
        {
            if (Input.GetKeyDown(keyForPrompt))
            {
                tutorialPrompt[current]?.SetActive(false);
                if (++current < tutorialPrompt.Count)
                    tutorialPrompt[current]?.SetActive(true);
            }
            if (current > tutorialPrompt.Count - 1)
            {
                OnPromptFinished();
            }
        }
        else if (playingComplete)
        {
            if (Input.GetKeyDown(keyForPrompt))
            {
                OnAllComplete();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomb"))
            return;
        if (hasTriggered)
            return;
        hasTriggered = true;

        OnTutorialBegin();
    }

    IEnumerator ShowPromptForTime(float time)
    {
        tutorialPrompt[current]?.SetActive(true);
        while (current < tutorialPrompt.Count)
        {
            yield return new WaitForSeconds(time);
            tutorialPrompt[current]?.SetActive(false);
            if (++current < tutorialPrompt.Count)
                tutorialPrompt[current]?.SetActive(true);
        }
    }

    IEnumerator ShowCompletedMsg(float time)
    {
        completedMsg?.SetActive(true);
        yield return new WaitForSeconds(time);
        completedMsg?.SetActive(false);
    }
}
