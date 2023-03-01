using System;
using System.Collections.Generic;

[Serializable]
public class SaveResource
{
    public Dictionary<ResourceType, int> DicResourceTypes = new Dictionary<ResourceType, int>();

    public event Action<ResourceType> OnResourceTypeChange;

    public void AddResource(ResourceType resourceType)
    {
        if (!DicResourceTypes.ContainsKey(resourceType))
        {
            DicResourceTypes.Add(resourceType, 0);
        }

        ++DicResourceTypes[resourceType];

        OnResourceTypeChange?.Invoke(resourceType);
    }

    public void ResetSubscribes()
    {
        OnResourceTypeChange = null;
    }
}
