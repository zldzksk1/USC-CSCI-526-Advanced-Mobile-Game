using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialPoint : MonoBehaviour
{
    private EventSystem eventSystem; // Reference to the EventSystem component
    private GameObject controlsUI;
    private GameObject player;
    private GameObject mathInfo;
    private GameObject mathManager;

    public TextMeshProUGUI instruction;
    public TextMeshProUGUI progress;

    private Transform lookTutorialImg;
    private Transform jumpTutorialImg;
    private Transform swingTutorialImg;

    public LayerMask whatIsTargeted;

    private static float startTime = 0f;

    //common var for freezing screen
    private GameObject freezeLikeImg;
    private GameObject instructionBg;
    private GameObject tutorialImg;
    public PlayerCamera playerCamera;


    //Tutorial_progress tutorial_Progress;

    //Welcome msg and WASD Tutorial vars
    private bool WelcomeMsgDone = false;
    private GameObject clickImg;
    private bool wasdCheck = false;
    private Vector3 startPosition;  //to Check how further they made move;
    private int welcomIndex = 0;
    string[] welcomeMsgs =
    {   
        "Mission 1: Move with keys - W A S D.",
        "Try it out!",
    };

    //Mouse move checker
    private bool mouseCheckDone = true;
    private Vector3 currPosition;   //to make player cant move on mouse section;
    [SerializeField] private GameObject targetObject;
    public float pointerTimeThreshold = 5f;
    private float pointerTime;
    private bool isPointing;
    public int mouseIndex = 0;
    string[] mouseMsgs =
    {
        "Mission2: AIM at the RED SPHERE for 3 seconds.",
        "Try it out by moving the MOUSE!"
    };

    //jump checker
    private bool jumpCheckDone = true;
    private int jumpIndex = 0;
    string[] jumpMsgs =
    {
        "Mission 3: Learn how to JUMP!",
        "To leap over gaps, you want to JUMP!",
        "To JUMP press W + SPACE BAR",
    };


    //swing checker
    private bool swingCheckerDone = true;
    private int swingIndex = 0;
    string[] swingMsgs =
    {
        "Mission 4: Learn how to SWING!",
        "AIM at a SPHERE and HOLD left click.",
        "Release it after you gain enough momentum.",
    };

    //private bool pullCheckerDone = true;
    //private int pullIndex = 0;
    //string[] pullMsgs =
    //{
    //    "Mission 5: Learn how to GRAPPLE!",
    //    "AIM at a SPHERE and HOLD right click.",
    //    "Release it anytime you want.",
    //};

    //it is number and calculate number
    //public int worldSum = 0;
    //private int bossNumber = 13;
    //private bool coinCheckDone = true;
    //private int coinIndex = 0;
    //[SerializeField] GameObject underBar;
    //private GameObject bossNumberText;
    //string[] coinMsgs =
    //{
    //    "Mission 6: Make a number GREATER than the target number!",
    //    "Given the math operator, PICKUP two numbers!",
    //    "Try it out, but watch out for the LASER!",
    //};


    // Start is called before the first frame update
    void Start()
    {
        //tutorial_Progress = FindObjectOfType<Tutorial_progress>();

        player = GameObject.Find("PlayerCapsule");
        player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;
        player.gameObject.GetComponent<Throwable>().blockKeyCodeR = true;      //block creating a grapple point

        //mathManager = GameObject.Find("MathManager").gameObject;
        
        freezeLikeImg = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("ScreenFreeze").gameObject;
        instructionBg = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("Instruction_bg").gameObject;
        clickImg = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("Left_Click").gameObject;
        tutorialImg = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("Tutorial_Instruction").gameObject;

        //bossNumberText = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("BossNumber").gameObject;
        //luanchBar = GameObject.Find("UI").transform.Find("DetailedTutorial").transform.Find("LaunchBar").gameObject; Do not require anymore
        //clickImg.SetActive(false);
        Manager.Instance.leftClickPrompt?.transform.GetChild(0)?.gameObject.SetActive(false);

        lookTutorialImg = instruction?.transform.Find("Look Tutorial");
        jumpTutorialImg = instruction?.transform.Find("Jump Tutorial");
        swingTutorialImg = instruction?.transform.Find("Swing Tutorial");
    }

    // Update is called once per frame
    void Update()
    {
        if (!WelcomeMsgDone)
        {
            WelcomMsg();
        }

        if (!mouseCheckDone)
        {
            MouseMoveChecker();
        }

        if (!jumpCheckDone)
        {
            JumpChecker();
        }

        if (!swingCheckerDone)
        {
            SwingChecker();
        }

        //if (!pullCheckerDone)
        //{
        //    //SwingChecker();
        //    PullChecker();
        //}

        //if (!coinCheckDone)
        //{
        //    CoinChecker();
        //}

    }

    void OnTriggerEnter(Collider other)
    {

        if (gameObject.name == "MoveTutorial")
        {
            return;
        }

        Tutorial_progress.missionProgress++;
        Tutorial_progress.progressIdx++;

        if (gameObject.name == "MouseTutorial")
        {
            mouseCheckDone = false;   //triger mouseCheck function;

            targetObject.SetActive(true); //aiming object spwan


            //freezing screen
            freezeLikeImg.SetActive(true);
            player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;    //stop player move;
            currPosition = player.gameObject.GetComponent<Transform>().position;    //stop player at the current position
            DontJumpOut();

            lookTutorialImg?.gameObject.SetActive(true);
        }

        if (gameObject.name == "JumpTutorial")
        {
            jumpCheckDone = false;
            freezeLikeImg.SetActive(true);
            Handicap.ReportMessageStatic("UnlockSpace");

            player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;    //stop player move;

            currPosition = player.gameObject.GetComponent<Transform>().position;    //stop player at the current position
            DontJumpOut();

            jumpTutorialImg?.gameObject.SetActive(true);
        }

        if (gameObject.name == "SwingTutorial")
        {
            swingCheckerDone = false;
            freezeLikeImg.SetActive(true);

            player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;    //stop player move;
            currPosition = player.gameObject.GetComponent<Transform>().position;    //stop player at the current position
            DontJumpOut();

            swingTutorialImg?.gameObject.SetActive(true);
        }

        //if (gameObject.name == "PullTutorial")
        //{
        //    pullCheckerDone = false;
        //    freezeLikeImg.SetActive(true);

        //    player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;    //stop player move;
        //    currPosition = player.gameObject.GetComponent<Transform>().position;    //stop player at the current position
        //    DontJumpOut();
        //}

        //if (gameObject.name == "CoinTutorial")
        //{
        //    coinCheckDone = false;
        //    freezeLikeImg.SetActive(true);

        //    player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;    //stop player move;
        //    currPosition = player.gameObject.GetComponent<Transform>().position;    //stop player at the current position
        //    DontJumpOut();
        //}

    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.name == "MoveTutorial")
        {
            GetComponent<BoxCollider>().enabled = false;
        }

        if (gameObject.name == "MouseTutorial")
        {
            GetComponent<BoxCollider>().enabled = false;
        }

        if (gameObject.name == "JumpTutorial")
        {
            StartCoroutine(SuccessMsg());
            GetComponent<BoxCollider>().enabled = false;
            jumpIndex++;
        }

        if (gameObject.name == "SwingTutorial")
        {
            StartCoroutine(SuccessMsg());
            Manager.Instance.goalObject.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
        }

        //if (gameObject.name == "PullTutorial")
        //{
        //    StartCoroutine(SuccessMsg());
        //    GetComponent<BoxCollider>().enabled = false;
        //}

        //if (gameObject.name == "CoinTutorial")
        //{
        //    GetComponent<BoxCollider>().enabled = false;
        //}
    }

    void WelcomMsg()
    {
        if (Manager.Instance.canStart)
        {
            if (welcomIndex == 0)
            {
                instruction.text = welcomeMsgs[welcomIndex];
                clickImg.SetActive(true);
                freezeLikeImg.SetActive(true);
                instructionBg.SetActive(true);
            }

            else if (welcomIndex == 1)
            {
                instruction.text = welcomeMsgs[welcomIndex];
                player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;
                startPosition = player.GetComponent<Transform>().position;
                clickImg.SetActive(false);
                freezeLikeImg.SetActive(false);
                wasdCheck = true;
                welcomIndex++;
            }

             if (wasdCheck)
            {
                instructionBg.SetActive(false);
                WasdMoveChecker();
            }

            if (enterCheck())
            {
                welcomIndex++;
            }

        }
    }

    void WasdMoveChecker()
    {
        float distanceHorizontal = Mathf.Abs(player.GetComponent<Transform>().position.x - startPosition.x);
        float distanceVertical = Mathf.Abs(player.GetComponent<Transform>().position.z - startPosition.z);

        if (distanceHorizontal >= 5 || distanceVertical >= 5)
        {
            StartCoroutine(SuccessMsg());
            WelcomeMsgDone = true;
        }
    }

    void MouseMoveChecker()
    {
        switch(mouseIndex)
        {
            case 0:
            {
                instruction.text = mouseMsgs[mouseIndex];
                DontJumpOut();
                freezeLikeImg.SetActive(true);
                clickImg.SetActive(true);
                instructionBg.SetActive(true);
                player.gameObject.GetComponent<PlayerMovement>().movePlayer = false;
                break;
            }
            case 1:
            {
                instruction.text = mouseMsgs[mouseIndex];
                clickImg.SetActive(false);
                freezeLikeImg.SetActive(false);
                instructionBg.SetActive(false);
                lookTutorialImg?.gameObject.SetActive(false);

                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Cast a ray from the mouse position into the world
                // Check if the ray intersects with the target object
                //Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (Camera.main.transform.forward * 500.0f), Color.blue);
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500.0f, whatIsTargeted))
                {
                        Debug.Log(hit.collider.gameObject);
                    if (hit.collider.gameObject == targetObject)
                    {
                        // Do something if the target object is being pointed to
                        Debug.Log("Pointer is pointing to the target object!");
                        startTime += Time.deltaTime;
                        Manager.Instance.crosshair.color = Color.green;

                        instruction.text = ((int)startTime + 1).ToString();

                        if (startTime > pointerTimeThreshold)
                        {
                            // User has moved their mouse
                            Debug.Log("User has moved their mouse");
                            mouseCheckDone = true;
                            player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;
                            targetObject.SetActive(false);
                            StartCoroutine(SuccessMsg());

                            lookTutorialImg?.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    Debug.Log("MISSING");
                    isPointing = false;
                    startTime = 0f;
                }
                break;
            }
        }

        if (enterCheck())
        {
            mouseIndex++;
            if(mouseIndex > 1)
            {
                mouseIndex = 1;
            }
        }
    }

    void JumpChecker()
    {
        if (jumpIndex <= 1)
        {
            instruction.text = jumpMsgs[jumpIndex];
            instructionBg.SetActive(true);
            clickImg.SetActive(true);
        }

        else if (jumpIndex == 2)
        {
            instruction.text = jumpMsgs[jumpIndex];
            clickImg.SetActive(true);
            freezeLikeImg.SetActive(false);
            clickImg.SetActive(false);
            jumpIndex++;

        }

        if (jumpIndex >= 3)
        {
            player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;    //allow player move;
            instructionBg.SetActive(false);
            jumpCheckDone = true;

            jumpTutorialImg?.gameObject.SetActive(false);

        }

        if (jumpIndex <= 3)
        {
            DontJumpOut();
        }

        if (enterCheck())
        {
            jumpIndex++;
        }
    }

    void SwingChecker()
    {
        if (swingIndex <= 1)
        {
            instruction.text = swingMsgs[swingIndex];
            clickImg.SetActive(true);
            instructionBg.SetActive(true);
        }

        else if (swingIndex == 2)
        {
            instruction.text = swingMsgs[swingIndex];

            player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;    //allow player move;
            freezeLikeImg.SetActive(false);
            clickImg.SetActive(false);
            swingIndex++;

            swingTutorialImg?.gameObject.SetActive(false);
        }

        if (swingIndex <= 2)
        {
            instructionBg.SetActive(false);
            DontJumpOut();
        }

        if (enterCheck())
        {
            swingIndex++;
        }
    }

    //void PullChecker()
    //{
    //    if (pullIndex <= 1)
    //    {
    //        Manager.Instance.leftClickPrompt?.transform.GetChild(0)?.gameObject.SetActive(true);
    //        instruction.text = pullMsgs[pullIndex];
    //        clickImg.SetActive(true);
    //        instructionBg.SetActive(true);
    //    }

    //    else if (pullIndex == 2)
    //    {
    //        instruction.text = pullMsgs[pullIndex];

    //        player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;    //allow player move;
    //        freezeLikeImg.SetActive(false);
    //        clickImg.SetActive(false);
    //        pullIndex++;
    //    }

    //    if (pullIndex <= 2)
    //    {
    //        DontJumpOut();
    //        instructionBg.SetActive(false);
    //    }

    //    if (enterCheck())
    //    {
    //        pullIndex++;
    //    }
    //}

    //void CoinChecker()
    //{
    //    ClearGameCheck();

    //    if (coinIndex <= 1)
    //    {
    //        instruction.text = coinMsgs[coinIndex];
    //        clickImg.SetActive(true);
    //        bossNumberText.SetActive(true);
    //        underBar.SetActive(true);
    //    }

    //    else if (coinIndex == 2)
    //    {
    //        instruction.text = coinMsgs[coinIndex];
    //        player.gameObject.GetComponent<PlayerMovement>().movePlayer = true;    //allow player move;
    //        freezeLikeImg.SetActive(false);
    //        clickImg.SetActive(false);
    //        underBar.SetActive(false);
    //        coinIndex++;
    //    }

    //    if (coinIndex <= 2)
    //    {
    //        DontJumpOut();

    //    }

    //    if (enterCheck())
    //    {
    //        coinIndex++;
    //    }

    //}

    //Prevent a player jump out right after unfreezen
    void DontJumpOut()
    {
        player.gameObject.GetComponent<Transform>().position = currPosition;
        player.gameObject.GetComponent<PlayerMovement>().horizontalInput = 0;   //set input value 0 
        player.gameObject.GetComponent<PlayerMovement>().verticalInput = 0;
    }

    //void ClearGameCheck()
    //{
    //    int localSum = mathManager.transform.GetComponent<MathManager>().resultToTutorial;

    //    if (localSum > bossNumber)
    //    {
    //        coinCheckDone = true;
    //        Manager.Instance.goalObject.SetActive(true);
    //        bossNumberText.SetActive(false);
    //        SuccessMsg();

    //    }
    //}

    IEnumerator SuccessMsg()
    {
        progress.text = "Move forward";
        instruction.text = "Great Job!";
        yield return new WaitForSeconds(3);
        instruction.text = "";
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
