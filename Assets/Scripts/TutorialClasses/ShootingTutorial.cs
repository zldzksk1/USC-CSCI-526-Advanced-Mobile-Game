using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTutorial : TemplateTutorial
{
    [Header("Shooting Tutorial")]
    public AI_Boss boss;

    protected override void OnTutorialBegin()
    {
        base.OnTutorialBegin();

        if (boss)
        {
            boss.ShouldMove = false;
            boss.d_OnTakeDamge += OnLandedShot;
            boss.gameObject.SetActive(true);
        }
    }

    protected override void OnPromptFinished()
    {
        base.OnPromptFinished();

        playerMove.movePlayer = true;
        Handicap.ReportMessageStatic("UnlockShoot");
        if (boss)
            boss.ShouldMove = true;
    }

    protected override void OnTutorialComplete()
    {
        base.OnTutorialComplete();

        if (boss)
        {
            boss.d_OnTakeDamge -= OnLandedShot;
            boss.ShouldMove = false;
        }
    }

    protected override void OnAllComplete()
    {
        base.OnAllComplete();

        playerMove.movePlayer = true;
        boss.ShouldMove = true;
    }

    public void OnLandedShot()
    {
        OnTutorialComplete();
    }
}
