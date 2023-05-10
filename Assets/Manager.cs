using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static UnityEngine.Debug;


public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    public GameObject UICanvas;
    public Text levelCompleteText;
    public GameObject player;
    public GameObject spawnPoint;
    public GameObject pauseCanvas;
    public TextMeshProUGUI sensValue;
    public GameObject leftClickPrompt;
    public GameObject rightClickPrompt;
    public TextMeshProUGUI placeablePointsLeft;
    public Image elementImage;
    public Image crosshair;
    public Quaternion initialOrientation;
    public int coinsInLevel;
    public int grappleLaunchLeft = 7;
    public int deathCount;
    public float attemptTimer;
    public bool levelCompleted = false;
    public bool canStart = true;
    public PlayerCamera playerCamera;
    public float playerSens = 1.0f;

    public GameObject numberResultPrefab;

    public bool isPaused = false;
    public GameObject goalObject;

    public TextMeshProUGUI coinsLeftText;

    private System.Diagnostics.Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();
    public static System.Diagnostics.Stopwatch timerParse;

    public static System.Diagnostics.Stopwatch FirstSwingtimerParse;

    Coroutine cameraCoroutine;

    public bool firstSwing = false;

    public Dictionary<string, float> checkpointTimes = new Dictionary<string, float>();

    public Dictionary<string, int> grapplePointsAndCounts;
    public Dictionary<string, float> allCoins;
    public Dictionary<string, int> allLasers;

    public Transform lasers;
    public Transform levelGameObjs;

    public string grapplePointNames;
    public string grapplePointValues;

    public string coinNames;
    public string coinValues;

    public string laserNames;
    public string laserValues;

    public bool hasGrappledAPoint = false;
    public bool hasGrabbedCoin = false;

    public int spawnedGrapplePoints;

    public int ammoDamage = 20;
    public int ammoCount = 0;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }

        GameObject[] allNormalGrapplePoints = GameObject.FindGameObjectsWithTag("NeutralPoint");
        GameObject[] allRedGrapplePoints = GameObject.FindGameObjectsWithTag("RedPoint");
        GameObject[] allBlueGrapplePoints = GameObject.FindGameObjectsWithTag("BluePoint");

        Object[] allPickupables = Object.FindObjectsOfType(typeof(Pickupable));
        GameObject[] allUntaggedObjects = GameObject.FindGameObjectsWithTag("Untagged");

        grapplePointsAndCounts = new Dictionary<string, int>();
        allCoins = new Dictionary<string, float>();
        allLasers = new Dictionary<string, int>();

        for (int i = 0; i < allNormalGrapplePoints.Length; i++)
        {
            if (!grapplePointsAndCounts.ContainsKey(allNormalGrapplePoints[i].name))
            {
                grapplePointsAndCounts.Add(allNormalGrapplePoints[i].name, 0);
            }
        }

        for (int i = 0; i < allRedGrapplePoints.Length; i++)
        {
            if (!grapplePointsAndCounts.ContainsKey(allRedGrapplePoints[i].name))
            {
                grapplePointsAndCounts.Add(allRedGrapplePoints[i].name, 0);
            }
        }

        for (int i = 0; i < allBlueGrapplePoints.Length; i++)
        {
            if (!grapplePointsAndCounts.ContainsKey(allBlueGrapplePoints[i].name))
            {
                grapplePointsAndCounts.Add(allBlueGrapplePoints[i].name, 0);
            }
        }

        for (int i = 0; i < allPickupables.Length; i++)
        {
            if (allPickupables[i].name.Contains("Coin"))
                allCoins.Add(allPickupables[i].name, 0);
        }
        
        UnityEngine.Debug.Log("before the if statement");
        if (GameObject.Find("Level").transform.Find("Lasers"))
        {
            /*UnityEngine.Debug.Log("in the if statement");
            levelGameObjs = GameObject.Find("Level").transform;

            foreach(Transform gameObj in levelGameObjs)
            {
                if (gameObj.name.Contains("LaserWall"))
                {
                    string laserName = gameObj.name;
                    UnityEngine.Debug.Log("aobut to test if the laser is showing up");
                    laserNames += laserName + " ";
                    allLasers[laserName] = 0;
                }
            }*/
            lasers = GameObject.Find("Level").transform.Find("Lasers").transform;
            foreach(Transform laser in lasers)
            {
                UnityEngine.Debug.Log("laser name: " + laser.name);
                laserNames += laser.name + " ";
                allLasers[laser.name] = 0;
            }
        }

        spawnedGrapplePoints = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.Find("UI");
        //grappleText = UICanvas.transform.Find("Grapple Text").GetComponent<Text>();
        levelCompleteText = UICanvas.transform.Find("Level Complete Text").GetComponent<Text>();
        player = GameObject.Find("PlayerCapsule");
        spawnPoint = GameObject.Find("SpawnPoint");
        pauseCanvas = UICanvas.transform.Find("Pause Menu").gameObject;
        sensValue = pauseCanvas.transform.Find("Camera Sensitivity").Find("Sens Value").GetComponent<TextMeshProUGUI>();
        coinsInLevel = GameObject.Find("Level").transform.Find("Coins").childCount;
        elementImage = UICanvas.transform.Find("Element Image").GetComponent<Image>();
        levelCompleted = false;
        timerParse = Stopwatch.StartNew();
        FirstSwingtimerParse = Stopwatch.StartNew();
        crosshair = UICanvas.transform.Find("Outline Crosshair").Find("Inner Crosshair").GetComponent<Image>();
        leftClickPrompt = UICanvas.transform.Find("Outline Crosshair").Find("Left_Click").gameObject;
        rightClickPrompt = leftClickPrompt?.transform.Find("Right_Click")?.gameObject;
        if (UIManager.instance.currentResultText != null)
        {
            UIManager.instance.currentResultText.text += " " + ammoCount.ToString();
        }
        
        if(UICanvas.transform.Find("Grapples Left Text") != null)
        {
            placeablePointsLeft = UICanvas.transform.Find("Grapples Left Text").GetComponent<TextMeshProUGUI>();
            UnityEngine.Debug.Log("placeable limit: " + player.GetComponent<Throwable>().placeablePointLimit);
            //placeablePointsLeft.text = $"Placeable Points Left: {player.GetComponent<Throwable>().placeablePointLimit}";
        }


        goalObject = GameObject.Find("Level")?.transform.Find("EndPoint")?.gameObject;
        playerCamera = GameObject.Find("CameraHolder").transform.Find("MainCamera").GetComponent<PlayerCamera>();
        coinsLeftText = UICanvas.transform.Find("Coins Left Text").GetComponent<TextMeshProUGUI>();
        coinsLeftText.text = $"Gold Coins Left: {coinsInLevel}";
        firstSwing = false;

        if (coinsInLevel < 1) coinsLeftText.text = "";
    }

    void ResetScene()
    {
        attemptTimer = 0.0f;
        deathCount = 0;
        levelCompleted = false;
        crosshair = UICanvas.transform.Find("Outline Crosshair").Find("Inner Crosshair").GetComponent<Image>();
        initialOrientation = player.transform.rotation;
    }

    public IEnumerator StopCamera()
    {
        playerCamera.canLook = false;
        playerCamera.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(0.5f);
        playerCamera.canLook = true;
    }

    public void RespawnPlayer()
    {
        player.transform.SetPositionAndRotation(spawnPoint.transform.position, initialOrientation);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<PlayerMovement>().launchHoldTimer = 0.0f;
        if(cameraCoroutine != null)
        {
            StopCoroutine(cameraCoroutine);
        }
        cameraCoroutine = StartCoroutine(StopCamera());
        deathCount++;
        SwingController sc = player.GetComponent<SwingController>();
        sc.launchVelocity = Vector3.zero;
        sc.BreakRope();

        laserValues = "";
        foreach (KeyValuePair<string, int> laserData in allLasers)
        {
            laserValues += laserData.Value + " ";
        }

        UnityEngine.Debug.Log("laser values: " + laserValues);
    }

    public void ExitButton()
    {
        if(!isPaused)
        {
            Pause();
        }
        else
        {
            UnPause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            if (!levelCompleted)
            {
                attemptTimer += Time.deltaTime;
            }
            if (player.transform.position.y < 0.0f)
            {
                RespawnPlayer();
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(!isPaused)
                {
                    Pause();
                }
                else
                {
                    UnPause();
                }
            }
            if (hasGrappledAPoint)
            {
                foreach (KeyValuePair<string, int> grapplePointData in grapplePointsAndCounts)
                {
                    grapplePointNames = grapplePointNames + grapplePointData.Key + " ";
                    grapplePointValues = grapplePointValues + grapplePointData.Value + " ";
                }

                UnityEngine.Debug.Log("grapple point names: " + grapplePointNames);
                UnityEngine.Debug.Log("grapple point values: " + grapplePointValues);

                hasGrappledAPoint = false;
            }

            if (hasGrabbedCoin)
            {
                foreach (KeyValuePair<string, float> coinData in allCoins)
                {
                    coinNames = coinNames + coinData.Key + " ";
                    coinValues = coinValues + coinData.Value + " ";
                }

                UnityEngine.Debug.Log("coin names: " + coinNames);
                UnityEngine.Debug.Log("coin values: " + coinValues);

                hasGrabbedCoin = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E) && MathPickup.equationSolved)
        {
            if (ammoCount < 1) return;
            UnityEngine.Debug.Log("I want to shoot since I have a result");
            UIManager.instance.currentResultText.text = $"Ammo: {--ammoCount}";
            GameObject numResultCopy = Instantiate(numberResultPrefab);
            numResultCopy.transform.position = player.transform.position;
            TextScript text = numResultCopy.GetComponentInChildren<TextScript>();
            text.EnterTextHere = ammoDamage.ToString();
            if (!MathManager.instance)
                UnityEngine.Debug.LogError("no mathmanager");
            if (!MathManager.instance.boss)
                UnityEngine.Debug.LogError("mathmanager no boss");
            //MathManager.instance.boss.UpdateHP(-ammoDamage);
            if (ammoCount == 0)
                MathPickup.equationSolved = false;
        }
    }
    public void Pause()
    {
        //Time.timeScale = 0.0f;
        pauseCanvas.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnPause()
    {
        //Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdateCoin()
    {
        if (coinsInLevel < 1) return;
        coinsInLevel--;
        coinsLeftText.text = $"Gold Coins Left: {coinsInLevel}";
        if(coinsInLevel <= 0)
        {
            goalObject?.SetActive(true);
            coinsLeftText.text = "Exit ready!";
        }
    }

    /*public void UpdateLaunchText(int launchAmt)
    {
        grappleText.text = $"Launches Left: {launchAmt}";
    }*/
}
