using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUICounter : MonoBehaviour
{
    public ResourceType ResourceType { get; private set; }

    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image icon;

    public void InitializeCounter(ResourceType resourceType, Sprite icon)
    {
        ResourceType = resourceType;
        this.icon.sprite = icon;
    }

    public void UpdateAmount(int value)
    {
        amountText.text = value.ToString();

        amountText.transform.DORewind();
        amountText.transform.DOShakeScale(.35f, .3f, 3, 20);
    }
}
