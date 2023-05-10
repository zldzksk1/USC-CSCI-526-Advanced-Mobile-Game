using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberEater : MonoBehaviour
{
    [SerializeField] GameObject numbersParent;
    private MathPickup[] numbersChild;
    GameObject nearestObject = null;
    float maxDistance = Mathf.Infinity;
    Vector3 closestPosition;

    private Rigidbody enemyRb;

    // Start is called before the first frame update
    private void Awake()
    {
        numbersChild = numbersParent.GetComponentsInChildren<MathPickup>(true);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestObject();
        Vector3 lookDirection = (closestPosition - transform.position).normalized;
        transform.Translate(lookDirection * 30.0f * Time.deltaTime);
    }

    void FindNearestObject()
    {
        if (numbersChild.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            closestPosition = Vector3.zero;

            foreach (MathPickup obj in numbersChild)
            {
                if (true)//obj.GetComponent<MathPickup>().CanPickup == true)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);

                    if (distance < closestDistance && distance < maxDistance)
                    {
                        closestDistance = distance;
                        closestPosition = obj.transform.position;
                    }
                }
            }

           // Debug.Log("Nearest object position: " + closestPosition);
        }
        else
        {
           // Debug.Log("No objects with tag found.");
        }
    }
}
