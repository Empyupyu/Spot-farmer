using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Stack : MonoBehaviour
{
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private Transform stackPosition;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Vector3 offsetResourceInStack;

    private List<PickUpable> pickUpables = new List<PickUpable>();

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
        pickUpables.Add(pickupable);
    }

    private void RecalculateStack()
    {
        for (int i = 0; i < pickUpables.Count; i++)
        {
            pickUpables[i].transform.localPosition = i * offsetResourceInStack;
        }
    }
}
