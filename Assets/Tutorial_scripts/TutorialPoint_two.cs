using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialPoint_two : MonoBehaviour
{
    bool hasTriggered = false;

    private EventSystem eventSystem; // Reference to the EventSystem component
    private GameObject controlsUI;
    private GameObject player;
    private GameObject mathInfo;
    private GameObject mathManager;

    public TextMeshProUGUI instruction;
    public TextMeshProUGUI progress;

    private Transform pullTutorialImg;
    private Transform createTutorialImg;

    public LayerMask whatIsTargeted;

    //common var for freezing screen
    private GameObject freezeLikeImg;
    public PlayerCamera playerCamera;
    private GameObject instructionBg;

    private GameObject clickImg;
    private Vector3 startPosition;  //to Check how further they made move;

    //Mouse move checker
    private Vector3 currPosition;   //to make player cant move on mouse section;

    private bool pullCheckerDone = true;
    private int pullIndex = 0;
    string[] pullMsgs =
    {
        "Mission 1: Learn how to GRAPPLE!",
        "AIM at a SPHERE and HOLD right click.",
        "Release it anytime you want.",
    };

    private bool createCheckerDone = true;
    public bool pressedRDone = false;
    private GameObject blockKeyR;
    private int createIndex = 0;
    string[] createMsgs =
    {
        "Mission 2: Learn how to CREATE A SWING POINT!",
        "AIM at anyplace you want and PRESS R",
        "You can SWING with a Swing Point you created",
    };

    //it is number and calculate number
    public int worldSum = 0;
    private int bossNumber = 13;
    private bool coinCheckDone = true;
    private int coinIndex = 0;
    //[SerializeField] GameObject underBar;
    private GameObject bossNumberText;
    string[] coinMsgs =
    {
        "Mission 3: Make the LARGEST possible number!",
        "Given the math operator, PICKUP two numbers!",
        "Try it out, but watch out for the LASER!",
    };


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("PlayerCapsule");
        player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;
        player.gameObject.GetComponent<Throwable>().blockKeyCodeR = true;

        mathManager = GameObject.Find("MathManager")?.gameObject;
        instructionBg = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("Instruction_bg").gameObject;

        freezeLikeImg = clickImg = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("ScreenFreeze").gameObject;
        clickImg = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("Left_Click").gameObject;
        bossNumberText = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("BossNumber").gameObject;
        Manager.Instance.leftClickPrompt?.transform.GetChild(0)?.gameObject.SetActive(false);

        pullTutorialImg = instruction?.transform.Find("Pull Tutorial");
        createTutorialImg = instruction?.transform.Find("Create Tutorial");
    }

    // Update is called once per frame
    void Update()
    {
        if (!pullCheckerDone)
        {
            PullChecker();
        }

        if (!createCheckerDone)
        {
            CreateChecker();
        }

        if (!coinCheckDone)
        {
            CoinChecker();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
            return;
        hasTriggered = true;

        Tutorial_progress_two.missionProgress++;
        Tutorial_progress_two.progressIdx++;

        if (gameObject.name == "PullTutorial")
        {
            pullCheckerDone = false;
            freezeLikeImg.SetActive(true);

            player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;    //stop player move;
            currPosition = player.gameObject.GetComponent<Transform>().position;    //stop player at the current position
            DontJumpOut();

            pullTutorialImg?.gameObject.SetActive(true);
        }

        if (gameObject.name == "CreateTutorial")
        {
            createCheckerDone = false;

            Handicap.ReportMessageStatic("GrappleTutorialComplete");
            player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;    //stop player move;
            currPosition = player.gameObject.GetComponent<Transform>().position;    //stop player at the current position
            DontJumpOut();

            createTutorialImg?.gameObject.SetActive(true);
        }

        if (gameObject.name == "CoinTutorial")
        {
            coinCheckDone = false;
            freezeLikeImg.SetActive(true);

            player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;    //stop player move;
            currPosition = player.gameObject.GetComponent<Transform>().position;    //stop player at the current position
            DontJumpOut();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.name == "PullTutorial")
        {
            StartCoroutine(SuccessMsg());
            GetComponent<BoxCollider>().enabled = false;
        }

        if (gameObject.name == "CreateTutorial")
        {
            Handicap.ReportMessageStatic("UnlockBoost");
            if (pressedRDone)
            {
                StartCoroutine(SuccessMsg());
                GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                instruction.text = "You should create a GRAPPLE POINT by pressing R";
                createIndex = 3;
            }
        }

        if (gameObject.name == "CoinTutorial")
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

     void PullChecker()
    {
        if (pullIndex <= 1)
        {
            Manager.Instance.leftClickPrompt?.transform.GetChild(0)?.gameObject.SetActive(true);
            instruction.text = pullMsgs[pullIndex];
            instructionBg.SetActive(true);
            clickImg.SetActive(true);
        }

        else if (pullIndex == 2)
        {
            instruction.text = pullMsgs[pullIndex];
            instructionBg.SetActive(false);
            player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;    //allow player move;
            freezeLikeImg.SetActive(false);
            clickImg.SetActive(false);
            pullTutorialImg?.gameObject.SetActive(false);
            pullIndex++;
        }

        if (pullIndex <= 2)
        {
            DontJumpOut();
        }

        if (enterCheck())
        {
            pullIndex++;
        }
    }

    void CreateChecker()
    {
        if (createIndex <= 1)
        {
            Manager.Instance.leftClickPrompt?.transform.GetChild(0)?.gameObject.SetActive(true);
            instruction.text = createMsgs[createIndex];
            instructionBg.SetActive(true);
            freezeLikeImg.SetActive(true);
            clickImg.SetActive(true);
        }

        else if (createIndex == 2)
        {
            instruction.text = createMsgs[createIndex];
            instructionBg.SetActive(false);

            player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;    //allow player move;
            player.gameObject.GetComponent<Throwable>().blockKeyCodeR = false;      //allow creating a grapple point

            freezeLikeImg.SetActive(false);
            clickImg.SetActive(false);
            createTutorialImg?.gameObject.SetActive(false);
            DontJumpOut();

            createIndex++;
        }

        if (createIndex <= 2)
        {
            DontJumpOut();
        }

        if (enterCheck())
        {
            createIndex++;
        }
        PressedR();

    }

    void CoinChecker()
    {
        ClearGameCheck();

        if (coinIndex <= 1)
        {
            instruction.text = coinMsgs[coinIndex];
            instructionBg.SetActive(true);
            clickImg.SetActive(true);
            bossNumberText.SetActive(true);
            //underBar.SetActive(true);
        }

        else if (coinIndex == 2)
        {
            instruction.text = coinMsgs[coinIndex];
            instructionBg.SetActive(false);

            player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;    //allow player move;
            freezeLikeImg.SetActive(false);
            clickImg.SetActive(false);
            //underBar.SetActive(false);
            coinIndex++;
        }

        if (coinIndex <= 2)
        {
            DontJumpOut();
        }

        if (enterCheck())
        {
            coinIndex++;
        }

    }

    void PressedR()
    {
            if (player.gameObject.GetComponent<PlayerMovement>().movePlayer && Input.GetKeyDown(KeyCode.R))
            {
                pressedRDone = true;
            }
    }

    //Prevent a player jump out right after unfreezen
    void DontJumpOut()
    {
        player.gameObject.GetComponent<Transform>().position = currPosition;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.gameObject.GetComponent<PlayerMovement>().horizontalInput = 0;   //set input value 0 
        player.gameObject.GetComponent<PlayerMovement>().verticalInput = 0;
    }

    void ClearGameCheck()
    {
        int localSum = mathManager.transform.GetComponent<MathManager>().resultToTutorial;

        if (localSum > bossNumber)
        {
            coinCheckDone = true;
            Manager.Instance.goalObject.SetActive(true);
            bossNumberText.SetActive(false);
            SuccessMsg();

        }
    }

    IEnumerator SuccessMsg()
    {
        if (instruction) instruction.text = "Great Job!";
        if (progress) progress.text = "Move forward";
        yield return new WaitForSeconds(3);
        if (instruction) instruction.text = "";
    }

    bool enterCheck()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Do something when Enter key is pressed
            return true;
        }
        return false;
    }

}
