using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceUIScreen : MonoBehaviour
{
    [SerializeField] private ResourceUICounter resourceUICounterPrefab;
    [SerializeField] private RectTransform countersWindow;
    [SerializeField] private List<ResourceData> resourceDatas;

    private List<ResourceUICounter> ResourceUICounters = new List<ResourceUICounter>();
    private SaveResource saveResource;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        Subscribes();
        UpdateAllCounters();
    }

    private void Subscribes()
    {
        saveResource = FindObjectOfType<Inventory>().GetResources();
        saveResource.OnResourceTypeChange += UpdateResourceCounters;
    }

    private void UpdateAllCounters()
    {
        foreach (var key in saveResource.DicResourceTypes.Keys)
        {
            UpdateResourceCounters(key);
        }
    }

    private void UpdateResourceCounters(ResourceType type)
    {
        var counter = ResourceUICounters.FirstOrDefault(c => c.ResourceType == type);
        var data = resourceDatas.FirstOrDefault(d => d.ResourceType == type);

        if (counter == null)
        {
            CreateCounter(type, data, ref counter);
        }

        var value = saveResource.DicResourceTypes[type];

        if (value <= 0)
        {
            ResourceUICounters.Remove(counter);
            Destroy(counter.gameObject);
            return;
        }

        counter.UpdateAmount(value);
    }

    private void CreateCounter(ResourceType type, ResourceData data, ref ResourceUICounter counter)
    {
        counter = Instantiate(resourceUICounterPrefab, countersWindow);
        counter.InitializeCounter(type, data.Icon);
        ResourceUICounters.Add(counter);
    }
}
