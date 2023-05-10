using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}