using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Player Data")]
public class PlayerData : ScriptableObject
{
     [field : SerializeField] public float MovmentSpeed { get; private set; } 
}
