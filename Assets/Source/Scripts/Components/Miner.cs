using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Miner : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Mineable mineable;
    private Coroutine mineCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (this.mineable != null) return;
        if (!other.TryGetComponent<Mineable>(out var mineable)) return;

        this.mineable = mineable;

        mineCoroutine = StartCoroutine(Mine());
    }

    private IEnumerator Mine()
    {
        var type = mineable.GetResourceType();
        var data = playerData.ResourceMiningSpeed.FirstOrDefault(r => r.ResourceType.Equals(type));

        while (true)
        {
            yield return new WaitForSeconds(data.Speed);

            mineable.Hit();

            yield return new WaitUntil(() => mineable.ActiveForMining());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (mineable == null) return;
        if (!other.transform.Equals(mineable.transform)) return;

        StopCoroutine(mineCoroutine);
        mineCoroutine = null;
        mineable = null;
    }
}
