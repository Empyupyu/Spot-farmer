using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[RequireComponent(typeof(CollisionListener))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private CollisionListener collisionListener;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private Transform stackPosition;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Vector3 offsetResourceInStack;

    private string savePath;
    private SaveResource saveResource = new SaveResource();

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/SaveData.dat";
        sphereCollider.radius = playerData.PickupRadius;
        collisionListener.OnTriggerStayEvent += OnStay;

        LoadResources();
    }

    public SaveResource GetResources()
    {
        return saveResource;
    }

    private void OnStay(Transform obj)
    {
        if (!obj.TryGetComponent<PickUpable>(out var pickupable)) return;
        if (!pickupable.CanPickUp()) return;

        pickupable.PickUp(stackPosition);

        AddResource(pickupable.GetResourceType());
    }
                   
    private void AddResource(ResourceType resource)
    {
        saveResource.AddResource(resource);
    }

    private void RemoveResouce(ResourceType resource)
    {
        saveResource.DicResourceTypes.Remove(resource);
    }

    private void OnDestroy()
    {
        saveResource.ResetSubscribes();
        SaveResources();
    }

    private void SaveResources()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        binaryFormatter.Serialize(file, saveResource);
        file.Close();
    }

    private void LoadResources()
    {
        if (!File.Exists(savePath)) return;

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(savePath, FileMode.Open);

        saveResource = (SaveResource)bf.Deserialize(file);
        file.Close();
    }

    private void ResetData()
    {
        if (File.Exists(savePath))
        {
            saveResource = new SaveResource();
            File.Delete(savePath);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
        }
    }
}
