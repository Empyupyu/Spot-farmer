using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Factory : MonoBehaviour
{
    [SerializeField] private Transform inPoint;
    [SerializeField] private Transform outPoint;
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private FactoryData FactoryData;
}
