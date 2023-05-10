using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_progress_two : MonoBehaviour
{
    public TextMeshProUGUI progress;
    public static int missionProgress = 1;
    public static int progressIdx = 0;
    private int currIndex = 0;

    string[] missionTitle =
    {
        "",
        "Learn how to GRAPPLE!",
        "Learn how to CREATE A SWING POINT!",
        "Make the largest possible number",
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
            progress.text = "Mission: " + missionTitle[progressIdx] + " (" + progressIdx + "/3)";
            currIndex = progressIdx;
        }
    }
}
