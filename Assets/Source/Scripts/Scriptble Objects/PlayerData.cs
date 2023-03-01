using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Player Data")]
public class PlayerData : ScriptableObject
{
    [field : SerializeField] public float MovmentSpeed { get; private set; }
    [field : SerializeField] public float PickupRadius { get; private set; }
    [field : SerializeField] public float StartPositionRadiusToTransition { get; private set; }
    [field : SerializeField] public List<ResourceMiningSpeed> ResourceMiningSpeed { get; private set; } 
}
