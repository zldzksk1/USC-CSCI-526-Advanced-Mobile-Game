using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AI_Boss : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate = .5f;
    [SerializeField] float rotationRate = 30.0f;
    [SerializeField] float castDistance = 100.0f;
    [SerializeField] float shootingMoveDelay = 2.0f;
    [SerializeField] LayerMask whatIsTargeted;
    [SerializeField] LayerMask whatIsInTheWay;
    [SerializeField] Color laserColor = Color.cyan;
    [SerializeField] Color detectedLaserColor = Color.red;
    [SerializeField] float knockbackForce = 10.0f;

    public static float rotationRateValue;
    private Transform playerTransform;
    private float fireTimer;
    private LineRenderer lr, lr2;
    private RaycastHit hit;
    private bool shouldMove = true;
    [HideInInspector] public bool ShouldMove 
    { 
        get { return shouldMove; } 
        set { shouldMove = value; } 
    }
    private Coroutine stopMoving;
    private Transform startPoint, startPoint2;

    public int bossHP = 100;
    private TextMeshProUGUI hpText;
    public TextMeshPro hpWorldText;
    public string nextScene = "";

    // tutorial delegate
    public delegate void BossDelegate();
    public BossDelegate d_OnTakeDamge;

    public Rigidbody mRigidBody;

    [SerializeField] private bool finalBoss = false;

    public void UpdateHP(int amount)
    {
        if (bossHP == 0) return;
        bossHP += amount;
        bossHP = Mathf.Max(bossHP, 0);
        if (hpText)
            hpText.text = "Boss HP: " + bossHP;
        hpWorldText.text = bossHP.ToString();

        if (amount < 0)
            d_OnTakeDamge?.Invoke();

        if (bossHP == 0) HandleLevelComplete();
    }
    void HandleLevelComplete()
    {
        Debug.Log("Boss Beaten");
        hpText.text = "Boss Defeated!";
        hpWorldText.text = "Defeated";
        StartCoroutine(LoadSceneDelayed());
    }
    IEnumerator LoadSceneDelayed()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("PlayerCapsule").transform;
        startPoint = transform.Find("LaserStart");
        lr = GetComponent<LineRenderer>();
        lr.material.color = laserColor;
        if(finalBoss)
        {
            startPoint2 = transform.Find("LaserStart2");
            lr2 = transform.Find("Mesh")?.GetComponent<LineRenderer>();
            lr2.material.color = laserColor;
        }
        rotationRate = 15.0f/*UnityEngine.Random.Range(10.0f, 15.0f)*/;

        // copy rotationRate into rotationRateValue 
        rotationRateValue = rotationRate;

        hpText = GameObject.Find("UI")?.transform.Find("Boss HP Text")?.GetComponent<TextMeshProUGUI>();
        hpWorldText = transform.Find("HP")?.GetComponent<TextMeshPro>();
        hpWorldText.text = bossHP.ToString();

        //mRigidBody = myGameObject.AddComponent<Rigidbody>();;
    }

    // Update is called once per frame
    private void Update()
    {
        if(shouldMove)
        {
            Quaternion lookRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationRate * Time.deltaTime);
            if(finalBoss)
            {
                RaycastHit targetHit;
                Physics.Raycast(transform.position, transform.forward, out targetHit, castDistance, whatIsInTheWay);
                Vector3 targetDirection = targetHit.point - startPoint.position;
                targetDirection.Normalize();
                Vector3 targetDirection2 = targetHit.point - startPoint2.position;
                targetDirection2.Normalize();
                if (fireTimer >= fireRate &&
                    (Physics.Raycast(startPoint.position, targetDirection, out hit, castDistance, whatIsTargeted) ||
                    Physics.Raycast(startPoint2.position, targetDirection2, out hit, castDistance, whatIsTargeted))
                    )
                {
                    fireTimer = 0.0f;
                    ShootProjectile();
                    StartCoroutine(StopMoving(shootingMoveDelay));
                }
                else if (fireTimer < fireRate)
                {
                    fireTimer += Time.deltaTime;
                }
            }
            else
            {
                if (fireTimer >= fireRate && Physics.Raycast(startPoint.position, startPoint.forward, out hit, castDistance, whatIsTargeted))
                {
                    fireTimer = 0.0f;
                    ShootProjectile();
                    StartCoroutine(StopMoving(shootingMoveDelay));
                }
                else if(fireTimer < fireRate)
                {
                    fireTimer += Time.deltaTime;
                }
            }
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce((collision.transform.position - transform.position).normalized * knockbackForce, ForceMode.Impulse);
    }

    private IEnumerator StopMoving(float delay)
    {
        shouldMove = false;
        lr.material.color = detectedLaserColor;
        if(finalBoss)
        {
            lr2.material.color = detectedLaserColor;
        }
        yield return new WaitForSeconds(delay);
        shouldMove = true;
        lr.material.color = laserColor;
        if (finalBoss)
        {
            lr2.material.color = laserColor;
        }
    }

    private void LateUpdate()
    {
        if(finalBoss)
        {
            RaycastHit targetHit;
            Physics.Raycast(transform.position, transform.forward, out targetHit, castDistance, whatIsInTheWay);
            lr.SetPosition(0, startPoint.position);
            lr.SetPosition(1, targetHit.point);
            lr2.SetPosition(0, startPoint2.position);
            lr2.SetPosition(1, targetHit.point);
        }
        else
        {
            lr.SetPosition(0, startPoint.position);
            lr.SetPosition(1, startPoint.position + (startPoint.forward * castDistance));
        }
        
    }

    private void ShootProjectile()
    {
        if(projectilePrefab != null) 
        {
            GameObject projectile = Instantiate(projectilePrefab);
            if (finalBoss)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
            }
            else
            {
                projectile.transform.position = startPoint.position;
                projectile.transform.rotation = transform.rotation;
            }
            projectile.GetComponent<Projectile>().boss = this;
        }
    }
}