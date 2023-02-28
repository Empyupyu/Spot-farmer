using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private ResourceData resourceData;

    public void ApplySpawnForce()
    {
        rigidbody.AddForce(transform.up * resourceData.SpawnForcePower, ForceMode.Impulse);
    }
}
