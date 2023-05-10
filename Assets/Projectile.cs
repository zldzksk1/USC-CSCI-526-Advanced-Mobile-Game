using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float lifeTime = 5.0f;
    public AI_Boss boss;

    public static int AI_Boss_Hits = 0;

    public static string AI_Boss_Hits_String_Count;

    public static string AI_Boss_Hits_String_Rotation_Speed_Values;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * (moveSpeed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Do stuff here to penalize player
            boss?.UpdateHP(20);
            Destroy(gameObject);

            AI_Boss_Hits++;

            Debug.Log("AI Boss Hit : " + AI_Boss_Hits);
            Debug.Log("AI Boss Rotation Value : " + AI_Boss.rotationRateValue);

            AI_Boss_Hits_String_Count = AI_Boss_Hits_String_Count + "," + AI_Boss_Hits.ToString();
            AI_Boss_Hits_String_Rotation_Speed_Values = AI_Boss_Hits_String_Rotation_Speed_Values + "," + AI_Boss.rotationRateValue.ToString();
        }
    }
}
