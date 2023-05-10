using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static long timeLine;
    public string nextScene = "";
    bool hasFinished;

    // Start is called before the first frame update
    void Start()
    {
        hasFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Manager.Instance.coinsInLevel <= 0 && !hasFinished)
        {
            Debug.Log("Level Complete!");
            hasFinished = true;
            Manager.Instance.levelCompleteText.gameObject.SetActive(true);
            //Manager.Instance.grappleText.gameObject.SetActive(false);
            Manager.Instance.UICanvas.transform.Find("Outline Crosshair").gameObject.SetActive(false);
            Manager.Instance.UICanvas.transform.Find("Outline Crosshair").Find("Inner Crosshair").gameObject.SetActive(false);
            Manager.Instance.levelCompleted = true;
            Manager.timerParse.Stop();
            timeLine = Manager.timerParse.ElapsedTicks / 10000000;
            UnityEngine.Debug.Log("HEllo stopwatch - " + Manager.timerParse.Elapsed.ToString("mm\\:ss"));
            UnityEngine.Debug.Log("HEllo stopwatch - " + timeLine.ToString());
            PostToDatabase();
            Manager.Instance.UICanvas.transform.Find("Outline Crosshair").gameObject.SetActive(false);
            Manager.Instance.UICanvas.transform.Find("Outline Crosshair").Find("Inner Crosshair").gameObject.SetActive(false);

            if (nextScene.Length > 0)
            {
                StartCoroutine(LoadSceneDelayed());
            }
        }
        else
        {
            Debug.Log("Not finished yet...");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void PostToDatabase(){
        AnalyticsObj dbObj = new AnalyticsObj();
        Proyecto26.RestClient.Post("https://teamplaceholder7-71de2-default-rtdb.firebaseio.com/.json",dbObj);
        //Proyecto26.RestClient.Post("https://placeholders-ee91c-default-rtdb.firebaseio.com/.json",dbObj);
    }
    IEnumerator LoadSceneDelayed()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
