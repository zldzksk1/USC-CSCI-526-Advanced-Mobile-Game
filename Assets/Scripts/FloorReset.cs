using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(AudioSource))]
public class FloorReset : MonoBehaviour
{
    [Header("Set AudioClip in AudioSource to this,\nand uncheck PlayOnAwake")]
    [Unity.Collections.ReadOnly]
    public string LaserUsesThisFile = "UL_Short_burst_2";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;

        GetComponent<AudioSource>().Play();
        Manager.Instance?.RespawnPlayer();
    }
}
