using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTutorial : TemplateTutorial
{
    [Header("Boost Tutorial")]
    public Transform targetZ;

    protected override void Update()
    {
        base.Update();

        if (!inProgress)
            return;

        Transform playerTransform = Manager.Instance?.player?.transform;
        if (playerTransform && playerTransform.position.z >= targetZ.position.z)
        {
            OnTutorialComplete();
        }
    }

    protected override void OnPromptFinished()
    {
        base.OnPromptFinished();

        playerMove.movePlayer = true;
        Handicap.ReportMessageStatic("UnlockBoost");
    }

    protected override void OnTutorialBegin()
    {
        base.OnTutorialBegin();
    }

    protected override void OnTutorialComplete()
    {
        base.OnTutorialComplete();

        Handicap.ReportMessageStatic("UnlockCreatePoint");
        Handicap.ReportMessageStatic("LockBoost");
    }
}
