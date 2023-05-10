using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public GameObject newspherePrefab;
    public GameObject spherePrefab;
    public float spawnDistance = 20f;
    public float despawnTime = 5.0f;
    public int placeablePointLimit = 5;
    public bool canPlace = true;
    public bool placingPoint;

    public bool blockKeyCodeR = true;  //Using it on tutorialPoint_two to block user creating a grapple point during screen freez 

    private void Awake()
    {
        blockKeyCodeR = false;
    }
    private void Start()
    {
        if(Manager.Instance.placeablePointsLeft != null)
        {
            Manager.Instance.placeablePointsLeft.text = $"Placeable Points Left: {placeablePointLimit}";
        }
        canPlace = true;
        StartCoroutine(StartText());
    }

    IEnumerator StartText()
    {
        yield return new WaitForSeconds(1.0f);
        if (Manager.Instance.placeablePointsLeft != null)
        {
            Manager.Instance.placeablePointsLeft.text = $"Placeable Points Left: {placeablePointLimit}";
        }
    }

    void Update()
    {
        if (!placingPoint && /*placeablePointLimit > 0*/ canPlace && Input.GetKeyDown(KeyCode.R) && !blockKeyCodeR)
        {
            //Debug.Log($"PlaceablePointLimit: {placeablePointLimit}");
            placingPoint = true;
            // Spawn the sphere spawnDistance units away from the mouse pointer
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward * spawnDistance;
            GameObject newSphere = Instantiate(newspherePrefab, spawnPos, Quaternion.identity);

            Manager.Instance.spawnedGrapplePoints++;
            UnityEngine.Debug.Log("number of grapple points: " + Manager.Instance.spawnedGrapplePoints);

            DecreasePlaceableCount();

            // Keep updating the sphere's position to follow the mouse pointer
            StartCoroutine(FollowMouse(newSphere));
        }

        if (placingPoint && Input.GetKeyUp(KeyCode.R))
        {
            canPlace = false;
            placingPoint = false;
            // Get the camera's position and forward direction
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;

            // Calculate the spawn position based on the camera's position and forward direction
            Vector3 spawnPos = cameraPos + cameraForward * spawnDistance;

            // Spawn the sphere at the calculated position
            GameObject newSphere = Instantiate(spherePrefab, spawnPos, Quaternion.identity);
            StartCoroutine(WaitToPlaceAgain());
            StartCoroutine(Despawn(newSphere, despawnTime));
        }
    }

    IEnumerator WaitToPlaceAgain()
    {
        yield return new WaitForSeconds(3.0f);
        canPlace = true;
    }

    IEnumerator Despawn(GameObject gameObject, float timer)
    {
        yield return new WaitForSeconds(timer);
        var sc = GetComponent<SwingController>();
        if(sc.IsSwinging && gameObject == sc.selectedGrapple)
        {
            sc.BreakRope();
        }
        Destroy(gameObject);
    }

    void DecreasePlaceableCount()
    {
        if (placeablePointLimit < 1) return;
        placeablePointLimit--;
        if (Manager.Instance.placeablePointsLeft)
        {
            Manager.Instance.placeablePointsLeft.text = $"Placeable Points Left: {placeablePointLimit}";
        }
    }

    IEnumerator FollowMouse(GameObject sphere)
    {
        while (Input.GetKey(KeyCode.R))
        {
            // Update the sphere's position to follow the mouse pointer
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward * spawnDistance;
            sphere.transform.position = newPos;
        
            yield return null;
        }
    
        // Destroy the sphere when the R key is released
        Destroy(sphere);
    }
}