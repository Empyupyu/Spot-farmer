using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Factory : MonoBehaviour
{
    [SerializeField] private Transform inPoint;
    [SerializeField] private Transform outPoint;
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private FactoryData FactoryData;

    private void Awake()
    {
        collisionListener.OnTriggerEnterEvent += OnEnter;
        collisionListener.OnTriggerExitEvent += OnExit; ;
    }

    private void OnExit(Transform obj)
    {
        
    }

    private void OnEnter(Transform obj)
    {
        
    }
}
