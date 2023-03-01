using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Factory Data")]
public class FactoryData : ScriptableObject
{
    [field : SerializeField] public float ProductionTime { get; private set; }
    [field : SerializeField] public float DelayTransitionResourceInSpot { get; private set; }
    [field : SerializeField] public int AmoutResourcesToGenerateNew { get; private set; }
    [field : SerializeField] public int AmoutNewResourcesGenerated { get; private set; }
    [field : SerializeField] public ResourceType InResourceType { get; private set; }
    [field : SerializeField] public Resource OutResource { get; private set; }
}

