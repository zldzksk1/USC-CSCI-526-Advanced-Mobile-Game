using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPoint : MonoBehaviour
{
    private bool isPulling = false;
    private float pullTimer = 0f;
    private const float PULL_TIME = .5f;
    private const float BUFFER = .2f;
    private const float END_BOOST = 9f;
    private SwingController controller;
    private Transform playerTransform;
    private Vector3 startPos;

    public void StartPulling(SwingController swing)
    {
        if (isPulling) return;
        
        controller = swing;
        Rigidbody rigid;
        if (!controller.gameObject.TryGetComponent<Rigidbody>(out rigid))
        {
            Debug.LogWarning("Player doesn't have a Rigidbody Component");
        }
        rigid.velocity = Vector3.zero;
        playerTransform = controller.transform;
        startPos = playerTransform.position;
        pullTimer = 0f;
        isPulling = true;
    }

    public void StopPulling()
    {
        if (!isPulling) return;

        Vector3 boost = (transform.position - startPos).normalized;
        Rigidbody rigid;
        if (!controller.gameObject.TryGetComponent<Rigidbody>(out rigid))
        {
            Debug.LogWarning("Player doesn't have a Rigidbody Component");
        }
        rigid.AddForce(boost * END_BOOST, ForceMode.Impulse);
        isPulling = false;
    }
    
    private void FixedUpdate()
    {
        if (isPulling)
        {
            if (!controller.isPulling)
            {
                StopPulling();
                return;
            }
            float playerToPullPoint = Vector3.Distance(playerTransform.position, transform.position);
            if (playerToPullPoint < 12.5f)
            {
                StopPulling();
                controller.BreakRope();
                return;
            }
            pullTimer = Mathf.Min(pullTimer + Time.fixedDeltaTime, PULL_TIME);
            playerTransform.position =
                Vector3.Lerp(startPos, transform.position, pullTimer / (PULL_TIME + BUFFER));
            if (pullTimer == PULL_TIME)
            {
                StopPulling();
                controller.BreakRope();
            }
        }
    }
}
