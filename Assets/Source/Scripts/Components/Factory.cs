using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Factory : MonoBehaviour
{
    [SerializeField] private Transform inPoint;
    [SerializeField] private Transform outPoint;
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private FactoryData FactoryData;

    private List<Resource> inventoryResources = new List<Resource>();
    private List<Resource> inResources = new List<Resource>();
    private List<Resource> outResources = new List<Resource>();

    private Coroutine takingCoroutine;
    private Coroutine workingCoroutine;
    private Inventory inventory;

    private void Awake()
    {
        collisionListener.OnTriggerEnterEvent += OnEnter;
        collisionListener.OnTriggerExitEvent += OnExit; ;
    }

    private void OnEnter(Transform obj)
    {
        if (!obj.TryGetComponent(out Inventory inv)) return;
        if (inventory != null) return;

        inventory = inv;

        var resources = inventory.GetResources();

        if (resources.DicResourceTypes.Count == 0) return;

        inventoryResources = inventory.TakeResource(FactoryData.InResourceType);

        if (inventoryResources == null) return;

        takingCoroutine = StartCoroutine(TransitionIn());
    }

    private void OnExit(Transform obj)
    {
        if (inventory == null) return;
        if(!inventory.transform.Equals(obj.transform)) return;

        inventory = null;
    }

    private IEnumerator TransitionIn()
    {
        while (inventoryResources.Count > 0)
        {
            var res = inventoryResources[0];
            inventoryResources.RemoveAt(0);

            res.transform.parent = null;
            res.gameObject.SetActive(true);

            inventory.RemoveResouce(FactoryData.InResourceType);

            yield return res.transform.DOMove(inPoint.position, FactoryData.DelayTransitionResourceInSpot).WaitForCompletion();

            inResources.Add(res);

            if (workingCoroutine == null)
            {
                workingCoroutine = StartCoroutine(Working());
            }

            if (inventory == null) break;
        }

        takingCoroutine = null;
    }

    private IEnumerator Working()
    {
        while (inResources.Count >= FactoryData.AmoutResourcesToGenerateNew)
        {
            yield return new WaitForSeconds(FactoryData.ProductionTime);

            for (int i = 0; i < FactoryData.AmoutNewResourcesGenerated; i++)
            {
                var resource = Instantiate(FactoryData.OutResource, inPoint.transform.position, Random.rotation);

                resource.SetJumpPosition(outPoint.transform.position, .5f);

                yield return new WaitForSeconds(.5f);

                RemoveInResources();

                outResources.Add(resource);
            }
        }

        workingCoroutine = null;
    }

    private void RemoveInResources()
    {
        for (int j = 0; j < FactoryData.AmoutResourcesToGenerateNew; j++)
        {
            inResources.Remove(inResources[0]);
        }
    }
}
