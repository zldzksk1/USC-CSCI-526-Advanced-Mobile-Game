using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaser2 : MonoBehaviour
{
    public float speed = 0.5f;

    private Vector3 initialPos;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var target = playerObject.transform.position;
        var toPlayer = target - transform.position;
        transform.position += Time.deltaTime * speed * toPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Player"))
        {
            Debug.Log("collide player! ");
            transform.position = initialPos;
            
            Manager.Instance.RespawnPlayer();
        }
    }
}
