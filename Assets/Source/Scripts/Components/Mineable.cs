using UnityEngine;

public class Mineable : MonoBehaviour
{
    [SerializeField] private ResourceData resourceData;

    private int currentHealth;

    private void Start()
    {
        currentHealth = resourceData.Health;
    }

    public ResourceType GetResourceType()
    {
        return resourceData.ResourceType;
    }

    public void Hit()
    {
        --currentHealth;

        CreateResource();
    }

    private void CreateResource()
    {
        for (int i = 0; i < resourceData.ResourceCountPerHit; i++)
        {
            var resource =  Instantiate(resourceData.Resource, transform.position, Quaternion.identity);

        }
    }
}
