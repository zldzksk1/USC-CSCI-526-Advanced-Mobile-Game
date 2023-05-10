using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraSetup : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _cameraRoot;

    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cameraRoot = GameObject.Find("PlayerCapsule").transform.Find("PlayerCameraRoot");
        _virtualCamera.Follow = _cameraRoot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
