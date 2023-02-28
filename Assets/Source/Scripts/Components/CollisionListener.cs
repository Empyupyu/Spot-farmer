using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class CollisionListener : MonoBehaviour
{
    public event Action<Transform> OnTriggerEnterEvent;
    public event Action<Transform> OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other.transform);
    }

    private void OnDestroy()
    {
        OnTriggerEnterEvent = null;
        OnTriggerExitEvent = null;
    }
}
