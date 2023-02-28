using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Mineable mineable;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Mineable>(out var mineable)) return;

        this.mineable = mineable;

        StartCoroutine(Mine());
    }

    private void Mining()
    {
        
    }

    private IEnumerator Mine()
    {
        var type = mineable.GetResourceType();
        var data = playerData.ResourceMiningSpeed.FirstOrDefault(r => r.ResourceType.Equals(type));

        yield return new WaitForSeconds(data.Speed);

        mineable.
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
