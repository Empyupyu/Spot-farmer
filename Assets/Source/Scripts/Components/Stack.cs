using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Stack : MonoBehaviour
{
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private Transform stackPosition;

    private void Awake()
    {
        collisionListener.OnTriggerEnterEvent += OnEnter;
    }

    private void OnEnter(Transform obj)
    {
        if (!obj.TryGetComponent<PickUpable>(out var pickupable)) return;
        if (!pickupable.CanPickUp()) return;

        pickupable.PickUp(stackPosition);
    }
}
