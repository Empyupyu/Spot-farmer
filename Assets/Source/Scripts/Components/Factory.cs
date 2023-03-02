using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Factory : MonoBehaviour
{
    [SerializeField] private Transform inPoint;
    [SerializeField] private Transform outPoint;
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private FactoryData FactoryData;
    [SerializeField] private Vector3 startOffset = new Vector3(0, 1.5f, 0);

    private List<Resource> inResources = new List<Resource>();
    private List<Resource> poolResources = new List<Resource>();

    private Coroutine workingCoroutine;
    private Coroutine takingCoroutine;
    private Inventory inventory;

    private int availableResources;

    private void Awake()
    {
        collisionListener.OnTriggerStayEvent += OnStay;
        collisionListener.OnTriggerExitEvent += OnExit;
    }

    private void OnStay(Transform obj)
    {
        if (Input.touchCount > 0) return;
        if (!obj.TryGetComponent(out Inventory inv)) return;
        if (inventory != null) return;
        if (takingCoroutine != null) return;

        inventory = inv;

        var resources = inventory.GetResources();
        if (resources.DicResourceTypes.Count == 0) return;

        var type = FactoryData.InResource.GetResourceType();

        if (!resources.DicResourceTypes.ContainsKey(type)) return;

        availableResources = resources.DicResourceTypes[type];

        if (availableResources == 0) return;

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
        var type = FactoryData.InResource.GetResourceType();

        while (availableResources > 0)
        {
            if (inventory == null || Input.touchCount > 0) break;

            RemoveInventoryResource(type);

            var res = GetResource(type);

            yield return res.transform.DOMove(RandomizeStartPosition(), FactoryData.MoveDurationOnStartPosition).WaitForCompletion();
            yield return new WaitForSeconds(FactoryData.DelayBeforeTransition);
            yield return res.transform.DOMove(inPoint.position, FactoryData.DelayTransitionResourceInSpot).WaitForCompletion();

            if (workingCoroutine == null) workingCoroutine = StartCoroutine(Working());

            res.gameObject.SetActive(false);
        }

        inventory = null;
        takingCoroutine = null;
    }

    private void RemoveInventoryResource(ResourceType type)
    {
        inventory.RemoveResouce(type);
        --availableResources;
    }

    private Resource GetResource(ResourceType type)
    {
        var res = poolResources.FirstOrDefault(r => r.GetResourceType() == type && !r.gameObject.activeSelf);

        if(res == null)
        {
            res = Instantiate(FactoryData.InResource, inventory.transform.position, Random.rotation);
            res.Disable();
            poolResources.Add(res);
        }
        else
        {
            res.transform.position = inventory.transform.position;
            res.gameObject.SetActive(true);
        }

        inResources.Add(res);

        return res;
    }

    private Vector3 RandomizeStartPosition()
    {
        var radius = FactoryData.StartPositionRadiusToTransition / 2;

        var xRandomPosition = inventory.transform.position.x + Random.Range(-radius, radius);
        var yRandomPosition = startOffset.y + Random.Range(-radius, radius);
        var zRandomPosition = inventory.transform.position.z + Random.Range(-radius, radius);

        return new Vector3(xRandomPosition, yRandomPosition, zRandomPosition);
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
