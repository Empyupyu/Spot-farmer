using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Miner : MonoBehaviour
{
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private PlayerData playerData;

    private Mineable mineable;
    private Coroutine mineCoroutine;

    private void Awake()
    {
        collisionListener.OnTriggerEnterEvent += OnEnter;
        collisionListener.OnTriggerExitEvent += OnExit;
    }

    private void OnEnter(Transform obj)
    {
        if (this.mineable != null) return;
        if (!obj.TryGetComponent<Mineable>(out var mineable)) return;

        this.mineable = mineable;

        mineCoroutine = StartCoroutine(Mine());
    }

    private void OnExit(Transform obj)
    {
        if (mineable == null) return;
        if (!obj.Equals(mineable.transform)) return;

        StopCoroutine(mineCoroutine);
        mineCoroutine = null;
        mineable = null;
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
}
