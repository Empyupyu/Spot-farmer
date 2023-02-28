using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Resource Data")]
public class ResourceData : ScriptableObject
{
     [field : SerializeField] public int ResourceCountPerHit { get; private set; }
     [field : SerializeField] public int Health { get; private set; }
     [field : SerializeField] public float RecoveryTime { get; private set; }
     [field : SerializeField] public ResourceType ResourceType { get; private set; }
     [field : SerializeField] public Resource Resource { get; private set; }
}
