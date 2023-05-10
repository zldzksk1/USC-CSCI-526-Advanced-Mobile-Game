using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_progress : MonoBehaviour
{
    public TextMeshProUGUI progress;
    public static int missionProgress = 1;
    public static int progressIdx = 0;
    private int currIndex = -1;

    string[] missionTitle =
    {
        "Move with keys - W A S D",
        "AIM at the RED SPHERE for 3 seconds",
        "Learn how to JUMP!",
        "Learn how to SWING!",
    };

    // Start is called before the first frame update
    void Start()
    {
        progress.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (progressIdx > missionTitle.Length - 1) return;

        if (currIndex != progressIdx)
        {
            progress.text = "Mission: " + missionTitle[progressIdx] + " (" + missionProgress + "/4)";
            currIndex = progressIdx;
        }
    }
}
