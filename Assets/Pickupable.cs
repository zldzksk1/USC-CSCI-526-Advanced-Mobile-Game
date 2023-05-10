using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{

    [SerializeField] private float spinRate = 250.0f;
    [SerializeField] private bool shouldReset = false;
    private const float timeToReset = 5f;

    // Utility
    private Renderer renderer;
    private Collider collider;

    public static string coinName;

    public static string swingCount;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Renderer>(out renderer))
        {
            renderer = GetComponentInChildren<Renderer>();
            if (renderer == null)
            {
                Debug.LogError(gameObject.name + " has no Renderer Component.");
            }
        }
        if (!TryGetComponent<Collider>(out collider))
        {
            Debug.LogError(gameObject.name + " has no Collider Component.");
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Transform tag: {transform.tag}");
        Debug.Log($"Other tag: {other.tag}");
        if (transform.tag.Equals("BluePickup"))
        {
            Debug.Log("Setting to blue");
            Manager.Instance.player.GetComponent<SwingController>().element = "BluePoint";
            Manager.Instance.elementImage.color = new Color(0.0f, 0.0f, 1.0f, 0.65f);
            //Manager.Instance.coinsInLevel--;
        }
        else if (transform.tag.Equals("RedPickup"))
        {
            Debug.Log("Setting to red");
            Manager.Instance.player.GetComponent<SwingController>().element = "RedPoint";
            Manager.Instance.elementImage.color = new Color(1.0f, 0.0f, 0.0f, 0.65f);
            //Manager.Instance.coinsInLevel--;
        }
        else
        {
            Manager.Instance.allCoins[transform.name] = Manager.Instance.attemptTimer;
            Manager.Instance.hasGrabbedCoin = true;
            Manager.Instance.coinNames = "";
            Manager.Instance.coinValues = "";
            Debug.Log("Grabbed coin");
            Debug.Log("Coin Name" + gameObject.name + "Total swing count: " + SwingController.numSwings);
            coinName = coinName + "," + gameObject.name;
            swingCount = swingCount + "," + SwingController.numSwings;
            Manager.Instance.UpdateCoin();
        }
        //Destroy(gameObject);
        if (shouldReset)
        {
            StartCoroutine(DelayedReset());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (spinRate * Time.deltaTime), transform.eulerAngles.z);
    }

    IEnumerator DelayedReset()
    {
        renderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(timeToReset);
        renderer.enabled = true;
        collider.enabled = true;
    }
}
