using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrow : MonoBehaviour
{
    public GameObject spherePrefab; // the sphere prefab to be thrown
    public float throwForce = 10f; // the force at which the sphere is thrown
    public float destroyTime = 5f; // the time it takes for the sphere to disappear
    private Camera playerCamera; // the player's camera
    public GameObject newspherePrefab;
    public float spawnDistance = 20f;
    private void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T) && !Handicap.HasHandicapStatic(KeyCode.T) && spherePrefab)
        {
            GameObject sphere = Instantiate(spherePrefab, playerCamera.transform.position + playerCamera.transform.forward * spawnDistance, playerCamera.transform.rotation); // instantiate the sphere at the crosshair
            //Rigidbody rb = sphere.GetComponent<Rigidbody>(); // get the sphere's rigidbody component
            //rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse); // throw the sphere forward
            Destroy(sphere, destroyTime); // destroy the sphere after 5 seconds
        }

        
        if (Input.GetKeyDown(KeyCode.T) && newspherePrefab)
        {
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward * spawnDistance;
            GameObject newSphere = Instantiate(newspherePrefab, spawnPos, Quaternion.identity);
            StartCoroutine(FollowMouse(newSphere));
        }

       
    }
     IEnumerator FollowMouse(GameObject sphere)
    {
        while (Input.GetKey(KeyCode.T))
        {
            // Update the sphere's position to follow the mouse pointer
            Vector3 newPos = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
            sphere.transform.position = newPos;
        
            yield return null;
        }
    
        // Destroy the sphere when the R key is released
        Destroy(sphere);
    }
}