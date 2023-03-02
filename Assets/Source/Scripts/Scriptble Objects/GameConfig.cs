using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field : SerializeField, Expandable] public PlayerData PlayerData { get; private set; }
    [field : SerializeField, Expandable] public List<FactoryData> FactoryData { get; private set; }
    [field : SerializeField, Expandable] public List<ResourceData> ResourceData { get; private set; }
}
