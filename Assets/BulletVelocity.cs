using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour
{
    private Vector3 velocity;
    public float speed = 100;
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Camera.main.transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos += velocity * Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TempBoss" || other.gameObject.name == "FinalBoss")
        {
            Debug.Log("I hit the boss!");
            Debug.Log("result: " + -MathManager.instance.result);
            MathManager.instance.boss?.UpdateHP(-damage);
        }
    }
}
