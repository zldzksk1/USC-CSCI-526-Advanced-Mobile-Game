using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Stopwatch = System.Diagnostics.Stopwatch;

public class SwingController : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip, cam, player;
    public LayerMask whatIsGrappleable;

    [Header("Swinging")]
    private float maxSwingDistance = 25.0f;
    private float maxGrappleDistance = 40.0f;
    private Vector3 swingPoint;
    private SpringJoint joint;
    private Rigidbody rb;
    public float swingRopeDistance = 15.0f;

    [SerializeField] private float grappleForce = 15.0f;

    private PlayerMovement pm;
    private bool isSwinging;
    Vector3 currentGrapplePosition;
    public string element;
    private bool canSwing;
    private bool canGrapple;
    private bool canBoost;
    [HideInInspector]
    public bool isPulling = false;
    RaycastHit hit;

    // public accessor
    public bool IsSwinging { get { return isSwinging; } }

    [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius = 2.0f;
    public Transform predictionPoint;

    public bool justLaunched;
    public Vector3 launchVelocity;
    public bool isMovingGrapple;
    public Transform movingGrappleTransform;
    private Transform pullPointTransform;
    [SerializeField] private Color lrSwingColor = Color.gray;
    [SerializeField] private Color lrPullColor = Color.yellow;

    private Stopwatch firstSwingStopwatch;
    public static long timeTakenForFirstSwing;
    private bool firstSwingComplete = false;
    private GrapplePoint hoveredGrapple;
    public GrapplePoint selectedGrapple;

    public static long giveanyname;

    [HideInInspector] public float boostTimer = 0.75f; // Unused

    public static int numSwings = 0;

    public float grappleStartTime;

    public float currentTime;

    public string grappleName;

    public static string grappleItemTimes;

    public static string grappleItemNames;

    public static string grappleItemIndex;

    public static string grappleItemLocations;

    public float maxBoostTime = 1.0f;

    public float boostDelay = 0.125f;

    private float boostDelayTimer;
    private float boostTimeTimer;

    public float boostForce = 3.0f;

    private UnityEngine.UI.Image leftClickImg;
    private UnityEngine.UI.Image rightClickImg;
    public bool isBoostEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        if(!cam)
        {
            Debug.Log("Camera not manually assigned, auto-assigning...");
            cam = GameObject.Find("CameraHolder").transform.Find("MainCamera");
        }
        canBoost = true;
        firstSwingStopwatch = Stopwatch.StartNew();
        lr.material.color = lrSwingColor;
        grappleStartTime = Time.time;
        boostDelayTimer = boostDelay;
        boostTimeTimer = maxBoostTime;

        leftClickImg = Manager.Instance?.leftClickPrompt?.GetComponent<UnityEngine.UI.Image>();
        rightClickImg = Manager.Instance?.rightClickPrompt?.GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftClickImg)
            leftClickImg.enabled = false;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable)
            && (hit.collider.tag.Equals("NeutralPoint") || hit.collider.tag.Equals(element)))
        {
            float playerDist = Vector3.Distance(hit.collider.transform.position, transform.position);
            if (rightClickImg)
            {
                if(playerDist >= 12.5f)
                {
                    rightClickImg.enabled = true;
                }
                else
                {
                    rightClickImg.enabled = false;
                }
            }
            float camDist = Vector3.Distance(hit.collider.transform.position, cam.position);
            if(camDist < maxSwingDistance)
            {
                if (leftClickImg)
                    leftClickImg.enabled = true;
                canSwing = true;
            }
            canGrapple = true;
            Manager.Instance.crosshair.color = Color.green;
            Manager.Instance.leftClickPrompt?.SetActive(true);
            if(hoveredGrapple != null) 
            {
                hoveredGrapple.UnhoveredGrapple();
            }
            hoveredGrapple = hit.transform.GetComponent<GrapplePoint>();
            hoveredGrapple.HoveredGrapple();
        }
        else if (Physics.SphereCast(cam.position, predictionSphereCastRadius, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable) 
            && (hit.collider.tag.Equals("NeutralPoint") || hit.collider.tag.Equals(element))) 
        {
            float playerDist = Vector3.Distance(hit.collider.transform.position, transform.position);
            if (rightClickImg)
            {
                if (playerDist >= 12.5f)
                {
                    rightClickImg.enabled = true;
                }
                else
                {
                    rightClickImg.enabled = false;
                }
            }
            float camDist = Vector3.Distance(hit.collider.transform.position, cam.position);
            if (camDist < maxSwingDistance)
            {
                if (leftClickImg)
                    leftClickImg.enabled = true;
                canSwing = true;
            }
            canGrapple = true;
            Manager.Instance.crosshair.color = Color.green;
            Manager.Instance.leftClickPrompt?.SetActive(true);

            if (hoveredGrapple != null)
            {
                hoveredGrapple.UnhoveredGrapple();
            }
            hoveredGrapple = hit.transform.GetComponent<GrapplePoint>();
            hoveredGrapple.HoveredGrapple();
        }
        else
        {
            canSwing = false;
            Manager.Instance.crosshair.color = Color.white;
            Manager.Instance.leftClickPrompt.SetActive(false);
            if (hoveredGrapple != null)
            {
                hoveredGrapple.UnhoveredGrapple();
                hoveredGrapple = null;
            }
        }
        if (!Manager.Instance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !Handicap.HasHandicapStatic(KeyCode.Mouse0))
            {   
                StartSwing();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0) && !Handicap.HasHandicapStatic(KeyCode.Mouse0))
            {
                StopSwing();
                isMovingGrapple = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && !Handicap.HasHandicapStatic(KeyCode.Mouse1))
            {
                StartPull();
            }
            if (Input.GetKeyUp(KeyCode.Mouse1) && !Handicap.HasHandicapStatic(KeyCode.Mouse1))
            {
                StopPull();
            }
            /*if (Input.GetKeyDown(KeyCode.Space) && !Handicap.HasHandicapStatic(KeyCode.Space) 
                && !isSwinging && !pm.grounded)
            {
                GrappleLaunch();
            }*/
            if(isMovingGrapple && joint && movingGrappleTransform) // temporary fix for null reference errors
            {
                joint.connectedAnchor = movingGrappleTransform.position;
            }
        }
        if (isSwinging && !firstSwingComplete)
        {
            firstSwingComplete = true;
            firstSwingStopwatch.Stop();
            Manager.FirstSwingtimerParse.Stop();
            timeTakenForFirstSwing = Manager.FirstSwingtimerParse.ElapsedTicks / 10000000;
            UnityEngine.Debug.Log("HEllo stopwatch - " + Manager.FirstSwingtimerParse.Elapsed.ToString("mm\\:ss"));
            UnityEngine.Debug.Log("HEllo stopwatch - " + timeTakenForFirstSwing.ToString());
            // firstSwingTimeTaken = firstSwingStopwatch.ElapsedMilliseconds;
            Debug.Log("Time taken for first swing: " + timeTakenForFirstSwing + "ms");
        }
    }

    private void FixedUpdate()
    {
        if(!Manager.Instance.isPaused)
        {
            if(Input.GetKey(KeyCode.Mouse0) && !Handicap.HasHandicapStatic(KeyCode.Mouse0)
                && isSwinging && !isMovingGrapple)
            {
                Vector3 directionToPoint = swingPoint - transform.position;
                float distanceFromPoint = Vector3.Distance(swingPoint, transform.position);
                if(distanceFromPoint > swingRopeDistance)
                {
                    //Debug.Log("DistanceFromPoint: " + distanceFromPoint);
                    rb.AddForce(directionToPoint.normalized * 1500.0f * Time.deltaTime);
                    joint.maxDistance = distanceFromPoint * 0.8f;
                    joint.minDistance = distanceFromPoint * 0.25f;
                }
            }
            if (isBoostEnabled && Input.GetKey(KeyCode.Space) && !Handicap.HasHandicapStatic(KeyCode.Space))
            {
                if (boostDelayTimer < 0.0f)
                {
                    if (boostTimeTimer > 0.0f)
                    {
                        //rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, 0.0f, float.MaxValue), rb.velocity.z);
                        rb.AddForce(transform.up * boostForce * Time.fixedDeltaTime, ForceMode.Impulse);
                        boostTimeTimer -= Time.fixedDeltaTime;
                        boostTimeTimer = Mathf.Clamp(boostTimeTimer, 0.0f, maxBoostTime);
                        UIManager.instance?.boostProgressBar?.SetProgressPercentage(boostTimeTimer / maxBoostTime);
                    }
                }
                else
                {
                    boostDelayTimer -= Time.deltaTime;
                }
            }
            else if (!Input.GetKey(KeyCode.Space))
            {
                boostDelayTimer = boostDelay;

                boostTimeTimer += Time.fixedDeltaTime;
                boostTimeTimer = Mathf.Clamp(boostTimeTimer, 0.0f, maxBoostTime);
                UIManager.instance?.boostProgressBar?.SetProgressPercentage(boostTimeTimer / maxBoostTime);

            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == whatIsGrappleable)
        {
            other.gameObject.GetComponent<GrapplePoint>().SetInRangeMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == whatIsGrappleable)
        {
            other.gameObject.GetComponent<GrapplePoint>().SetDefaultMaterial();
        }
    }*/

    void StartSwing()
    {
        if(canSwing && !isPulling)
        {
            if(justLaunched)
            {
                justLaunched = false;
                //rb.AddForce(-launchVelocity, ForceMode.Impulse);
            }

            UnityEngine.Debug.Log("point grappled: " + hit.collider.name);
            if (Manager.Instance.grapplePointsAndCounts.ContainsKey(hit.collider.name))
            {
                Manager.Instance.grapplePointsAndCounts[hit.collider.name] += 1;
                Manager.Instance.hasGrappledAPoint = true;
                Manager.Instance.grapplePointNames = "";
                Manager.Instance.grapplePointValues = "";
            }

            isSwinging = true;
            pm.movementState = PlayerMovement.MovementState.SWING;
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            selectedGrapple = hit.transform.GetComponent<GrapplePoint>();

            MovingObstacle movingObstacle = hit.transform.GetComponent<MovingObstacle>();

            if (movingObstacle != null)
            {
                if(!movingObstacle.isAround && !movingObstacle.isHorizontal && !movingObstacle.isVertical)
                {
                    isMovingGrapple = false;
                }
                else
                {
                    isMovingGrapple = true;
                    movingGrappleTransform = hit.transform;
                }
            }
            else
            {
                isMovingGrapple = false;
            }

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.5f;
            joint.minDistance = distanceFromPoint * 0.25f;

            if(!Manager.Instance.firstSwing)
            {
                joint.spring = 0.0f;
                joint.damper = 7.0f;
                joint.massScale = 500.5f;
                Manager.Instance.firstSwing = true;
            }
            else
            {
                joint.spring = 4.5f;
                joint.damper = 7.0f;
                joint.massScale = 4.5f;
            }

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;

            BreakTimer timer = hit.collider.gameObject.GetComponent<BreakTimer>();
            if (timer != null) timer.StartTicking(this);
            if (!firstSwingComplete)
            {
                firstSwingStopwatch.Stop();
                firstSwingComplete = true;
                Manager.FirstSwingtimerParse.Stop();
                timeTakenForFirstSwing = Manager.FirstSwingtimerParse.ElapsedTicks / 10000000;
                Debug.Log("1Time taken for first swing: " + firstSwingStopwatch.ElapsedMilliseconds + "ms");
                Debug.Log("2Time taken for first swing: " + timeTakenForFirstSwing + "ms");
            }
            if (Input.GetMouseButtonDown(0))
            {
                numSwings++;
                Debug.Log("Swing " + numSwings);

                currentTime = Time.time - grappleStartTime;
                grappleName = hit.collider.name;
                Debug.Log("Grapple Name: " + grappleName);
                // print grapple point location
                Debug.Log("Grapple Point Location: " + hit.collider.transform.position);

                grappleItemIndex = grappleItemIndex + "," + numSwings.ToString();
                grappleItemNames = grappleItemNames + "," + grappleName;
                grappleItemTimes = grappleItemTimes + "," + currentTime.ToString();
                grappleItemLocations = grappleItemLocations + hit.collider.transform.position.ToString();
                

                Debug.Log("Grapple Item Counter: " + grappleItemIndex);
                Debug.Log("Grapple Item Names: " + grappleItemNames);
                Debug.Log("Grapple Item Times: " + grappleItemTimes);
                Debug.Log("Grapple Item Locations: " + grappleItemLocations);
                


                

            }
        }
    }

    public void BreakRope()
    {
        StopSwing();
        isMovingGrapple = false;
    }

    void StopSwing()
    {
        pm.movementState = PlayerMovement.MovementState.WALK;
        isSwinging = false;
        lr.positionCount = 0;
        selectedGrapple = null;
        Destroy(joint);
    }

    void StartPull()
    {
        if (canGrapple && !isPulling && !IsSwinging)
        {
            PullPoint pull;
            if (hit.collider.gameObject.TryGetComponent<PullPoint>(out pull))
            {
                pm.movementState = PlayerMovement.MovementState.GRAPPLE;
                isPulling = true;
                pullPointTransform = pull.transform;
                lr.positionCount = 2;
                lrSwingColor = lr.material.color;
                lr.material.color = Color.yellow;
                pull.StartPulling(this);
            }
        }
    }

    void StopPull()
    {
        if (!isPulling) return;
        pm.movementState = PlayerMovement.MovementState.WALK;
        isPulling = false;
        lr.positionCount = 0;
        lr.material.color = lrSwingColor;
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8.0f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, joint.connectedAnchor);
    }
    void DrawPullLine()
    {
        if (!isPulling) return;
        if (lr.positionCount < 2) return;
        if (!gunTip || !pullPointTransform) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, pullPointTransform.position);
    }

    private void GrappleLaunch()
    {
        if(canBoost)
        {
            Vector3 launchForce = /*(hit.point - transform.position).normalized*/ cam.transform.forward * grappleForce;
            rb.AddForce(launchForce, ForceMode.Impulse);
            canBoost = false;
            //Manager.Instance.grappleText.text = "Launch COOLDOWN";
            StartCoroutine(BoostTimer());
        }
    }

    private IEnumerator BoostTimer()
    {
        yield return new WaitForSeconds(boostTimer);
        canBoost = true;
        //Manager.Instance.grappleText.text = "Launch READY";
    }

    private void LateUpdate()
    {
        DrawRope();
        DrawPullLine();
    }

}
