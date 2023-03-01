using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Stack : MonoBehaviour
{
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private Transform stackPosition;
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        sphereCollider.radius = playerData.PickupRadius;
        collisionListener.OnTriggerStayEvent += OnStay;
    }

    private void OnStay(Transform obj)
    {
        if (!obj.TryGetComponent<PickUpable>(out var pickupable)) return;
        if (!pickupable.CanPickUp()) return;

        pickupable.PickUp(stackPosition);
    }
}
