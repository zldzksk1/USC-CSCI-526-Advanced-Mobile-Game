using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BombCollider : MonoBehaviour
{
    private MovingObstacle movingObstacle;
    private float slowspeed;
    GameObject movingObstacleObject;
    int count = 0;
    private bool isSlowing = false;
    float decelerationRate = 0.03f;
    float accelerationRate = 0.1f;
    float targetSpeed = 1.0f;
    void OnTriggerEnter(Collider other)
    {
        movingObstacleObject = GameObject.Find(other.gameObject.name);
        movingObstacle = movingObstacleObject.GetComponent<MovingObstacle>();
        Debug.Log("SPEED:"+movingObstacle.speed);
        if (movingObstacle != null && isSlowing == false) 
        {
            isSlowing = true;
            if(count==0)
            {
                count++;
                slowspeed = movingObstacle.speed;
            }
             StartCoroutine(SlowDownObject());
        }
    }
IEnumerator SlowDownObject()
{
    while (movingObstacle.speed > targetSpeed)
    {
        //movingObstacle.speed = Mathf.Lerp(movingObstacle.speed, targetSpeed, decelerationRate * Time.deltaTime);
        movingObstacle.speed = 1;
        yield return null;
    }
}

    void OnDestroy()
    {
        if (movingObstacle != null  && isSlowing == true)
        {
            isSlowing = false;
            count = 0;
            movingObstacle.speed = slowspeed;
            StopCoroutine(SlowDownObject());
            // StartCoroutine(SpeedUpObject());
            // Debug.Log("SLOW SPEED:"+slowspeed);
        }
    }

    // IEnumerator SpeedUpObject()
    // {
    //     while (movingObstacle.speed < slowspeed)
    //     {
    //         movingObstacle.speed = Mathf.Lerp(movingObstacle.speed, slowspeed, accelerationRate * Time.deltaTime);
    //         yield return null;
    //     }
    // }
}