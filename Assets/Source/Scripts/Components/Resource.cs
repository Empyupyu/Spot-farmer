using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private ResourceData resourceData;

    public void ApplySpawnForce()
    {
        var direction = Vector3.zero - transform.position;
        direction.y = 1f;

        var force = new Vector3(direction.normalized.x * resourceData.SpawnForcePower.x, direction.y * resourceData.SpawnForcePower.y, direction.normalized.z * resourceData.SpawnForcePower.z);

        rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
