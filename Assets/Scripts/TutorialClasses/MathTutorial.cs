using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathTutorial : TemplateTutorial
{
    [Header("Math Tutorial")]
    public List<MathPickup> tutorialNumList;
    public Collider nextTutorialCollider;
    public GameObject grapplePoint;
    public TextMeshProUGUI randNumTex;
    protected override void OnTutorialBegin()
    {
        base.OnTutorialBegin();
        if (randNumTex)
        {
            randNumTex.gameObject.SetActive(true);
        }

        if (MathManager.instance)
        {
            MathManager.instance.d_OnCompleteEquation += OnMathCompleteEquation;
            List<int> listNum = new List<int>();
            foreach (MathPickup num in tutorialNumList)
            {
                listNum.Add(num.numToTutorial);
            }
            MathManager.instance.GenerateQuestion(listNum);
            foreach (MathPickup pickup in tutorialNumList)
            {
                MathManager.instance.availableNumbers.Remove(pickup.numToTutorial);
            }
        }
    }

    protected override void OnPromptFinished()
    {
        base.OnPromptFinished();

        playerMove.movePlayer = true;
    }

    protected override void OnTutorialComplete()
    {
        base.OnTutorialComplete();

        foreach (MathPickup pickup in tutorialNumList)
        {
            Destroy(pickup.gameObject);
        }

        if (MathManager.instance)
            MathManager.instance.d_OnCompleteEquation -= OnMathCompleteEquation;
        nextTutorialCollider.isTrigger = true;
        MeshRenderer nextRenderer = nextTutorialCollider.GetComponent<MeshRenderer>();
        if (nextRenderer)
            nextRenderer.enabled = false;
        //grapplePoint?.SetActive(true);
    }

    public void OnMathCompleteEquation()
    {
        OnTutorialComplete();
    }
}
