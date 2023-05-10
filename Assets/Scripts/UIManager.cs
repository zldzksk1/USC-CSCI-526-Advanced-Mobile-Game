using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public GameObject UICanvas;
    
    [HideInInspector]
    public TextMeshProUGUI randNumText;
    [HideInInspector]
    public TextMeshProUGUI resultText;
    [HideInInspector]
    public TextMeshProUGUI multiplierText;
    [HideInInspector]
    public TextMeshProUGUI pendingText;
    public TextMeshProUGUI currentResultText;
    public ProgressBar boostProgressBar;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("UIManager instance already exists!");
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if(UICanvas == null)
        {
            UICanvas = GameObject.Find("UI");
        }
        randNumText = UICanvas?.transform.Find("Random Number Text")?.gameObject.GetComponent<TextMeshProUGUI>();
        resultText = UICanvas?.transform.Find("Result Number Text")?.gameObject.GetComponent<TextMeshProUGUI>();
        multiplierText = UICanvas?.transform.Find("Multiplier Text")?.gameObject.GetComponent<TextMeshProUGUI>();
        pendingText = UICanvas?.transform.Find("Pending Multiplier Text")?.gameObject.GetComponent<TextMeshProUGUI>();
        currentResultText = UICanvas.transform.Find("Current Number Text")?.gameObject.GetComponent<TextMeshProUGUI>();
        boostProgressBar = UICanvas.transform.Find("Progress Bar")?.gameObject.GetComponent<ProgressBar>();
        //
    }

    public void DisplayResult(int res)
    {
        StartCoroutine(ShowMessage(resultText, res.ToString(), 1.0f));
    }
    
    IEnumerator ShowMessage(TextMeshProUGUI guiText, string message, float delay) {
        guiText.text = message;
        guiText.enabled = true;
        yield return new WaitForSeconds(delay);
        guiText.enabled = false;
    }

    private void Update()
    {
        if (Manager.Instance.canStart)
        {
            boostProgressBar.gameObject.SetActive(true);
        }

    }
}
