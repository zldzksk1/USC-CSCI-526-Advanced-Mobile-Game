using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public float knockbackForce = 3.0f;

    [SerializeField] private Material outlineMat;
    [SerializeField] private Material inSwingRangeMat;
    [SerializeField] private Material inGrappleRangeMat;
    private Renderer _renderer;
    private Material originalMat;
    private Transform playerTransform;
    private Transform pointTransform;
    public float inRangeDist = 15.0f;
    private float inRangeDistSq;
    public bool isHovered;
    public bool isInSwingRange;
    public bool isInGrappleRange;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        originalMat = _renderer.material;

        {
            PullPoint pullPoint;
            if (!gameObject.TryGetComponent<PullPoint>(out pullPoint))
            {
                gameObject.AddComponent<PullPoint>();
            }
        }
        pointTransform = transform;
        //playerTransform = Manager.Instance.player.transform;
        inRangeDistSq = inRangeDist * inRangeDist;
    }

    // Update is called once per frame
    void Update()
    {
        /*if((Manager.Instance.player.transform.position - pointTransform.position).sqrMagnitude <= inRangeDistSq)
        {
            SetInRangeMaterial();
        }
        else
        {
            if(!isHovered)
            {
                SetDefaultMaterial();
            }
        }*/
    }

    public void HoveredGrapple()
    {
        isHovered = true;
        if (_renderer == null)
        {
            Debug.LogWarning("NULL RENDERER");
        }
        else
        {
            if (isInSwingRange)
            {
                SetInSwingRangeMaterial();
            }else if(isInGrappleRange)
            {
                SetInGrappleRangeMaterial();
            }
            //_renderer.material = outlineMat;
            //_renderer.material.SetColor("_BaseColor", Color.green);
        }
    }

    public void SetInSwingRangeMaterial()
    {
        isInSwingRange = true;
        //Debug.Log("Setting material in range");
        if (_renderer == null)
        {
            Debug.LogWarning("NULL RENDERER");
        }
        else
        {
            _renderer.material = inSwingRangeMat;
            _renderer.material.SetColor("_BaseColor", originalMat.GetColor("_BaseColor"));
        }
    }

    public void SetInGrappleRangeMaterial()
    {
        isInGrappleRange = true;
        Debug.Log("Setting material in range");
        if (_renderer == null)
        {
            Debug.LogWarning("NULL RENDERER");
        }
        else
        {
            _renderer.material = inGrappleRangeMat;
            _renderer.material.SetColor("_BaseColor", originalMat.GetColor("_BaseColor"));
        }
    }

    public void SetDefaultMaterial()
    {
        _renderer.material = originalMat;
    }

    public void UnhoveredGrapple()
    {
        isHovered = false;
        if(isInGrappleRange)
        {
            SetInGrappleRangeMaterial();
        }
        else if(isInSwingRange)
        {
            SetInSwingRangeMaterial();
        }
        else
        {
            SetDefaultMaterial();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce((collision.transform.position - transform.position).normalized * knockbackForce, ForceMode.Impulse);
    }
}
