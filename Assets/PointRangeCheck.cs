using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRangeCheck : MonoBehaviour
{
    public int grappableLayerNum = 11;
    public Vector3 offset;
    // Start is called before the first frame update
    private Transform cam;
    public bool shouldDetectGrapple = true;
    void Start()
    {
        if (!cam)
        {
            Debug.Log("Camera not manually assigned, auto-assigning...");
            cam = GameObject.Find("CameraHolder").transform.Find("MainCamera");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Manager.Instance.player.transform.position + offset;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == grappableLayerNum)
        {
            //Debug.Log($"{other.gameObject.name} entering trigger");
            GrapplePoint grapplePoint = other.gameObject.GetComponent<GrapplePoint>();
            grapplePoint.isInSwingRange = true;
            float dist = Vector3.Distance(other.gameObject.transform.position, cam.transform.position);
            if (dist < 25.0f)
            {
                grapplePoint.SetInSwingRangeMaterial();
            }
            else
            {
                if(shouldDetectGrapple)
                {
                    grapplePoint.isInSwingRange = false;
                    grapplePoint.SetInGrappleRangeMaterial();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == grappableLayerNum)
        {
            Debug.Log($"{other.gameObject.name} exiting trigger");
            GrapplePoint grapplePoint = other.gameObject.GetComponent<GrapplePoint>();
            grapplePoint.isInSwingRange = false;
            grapplePoint.isInGrappleRange = false;
            grapplePoint.SetDefaultMaterial();
        }
    }
}
