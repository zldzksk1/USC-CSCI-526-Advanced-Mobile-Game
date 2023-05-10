using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.5f;

    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var target = player.transform.position;
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
