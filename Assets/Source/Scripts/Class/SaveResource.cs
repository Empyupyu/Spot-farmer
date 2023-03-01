using System;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable]
public class SaveResource
{
    public Dictionary<ResourceType, int> DicResourceTypes = new Dictionary<ResourceType, int>();

    public event Action<ResourceType> OnResourceTypeChange;

    public void AddResource(ResourceType resourceType)
    {
        if (!IsContainsResource(resourceType))
        {
            DicResourceTypes.Add(resourceType, 0);
        }

        ++DicResourceTypes[resourceType];

        OnResourceTypeChange?.Invoke(resourceType);
    }

    public void RemoveResource(ResourceType resourceType)
    {
        if (!IsContainsResource(resourceType)) return;

        --DicResourceTypes[resourceType];

        OnResourceTypeChange?.Invoke(resourceType);
    }

    private bool IsContainsResource(ResourceType resourceType)
    {
        return DicResourceTypes.ContainsKey(resourceType);
    } 

    public void ResetSubscribes()
    {
        OnResourceTypeChange = null;
    }
}
