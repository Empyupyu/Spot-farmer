using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Resource Data")]
public class ResourceData : ScriptableObject
{
     [field : SerializeField] public int ResourceCountPerHit { get; private set; }
     [field : SerializeField] public int Health { get; private set; }
     [field : SerializeField] public float RecoveryTime { get; private set; }
     [field : SerializeField] public ResourceType ResourceType { get; private set; }
     [field : SerializeField] public Resource Resource { get; private set; }
     [field : SerializeField] public float SpawnForcePower { get; private set; }
     [field : SerializeField, Header("Mining Animation Settings")] public float ShakeDuration { get; private set; }
     [field : SerializeField] public float ShakePower { get; private set; }
     [field : SerializeField] public int ShakeVibrato { get; private set; }
     [field : SerializeField] public float ShakeRandomness { get; private set; }
}
