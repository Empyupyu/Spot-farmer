using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Mineable : MonoBehaviour
{
    [SerializeField] private ResourceData resourceData;
    [SerializeField] private Transform objectGraphics;
    [SerializeField] private Vector3 spawnResourcePositionOffset = new Vector3(0, 2f, 0);

    private int currentHealth;
    private float originScale;

    private void Start()
    {
        originScale = objectGraphics.localScale.x;
        SetStartHealth();
    }

    public ResourceType GetResourceType()
    {
        return resourceData.ResourceType;
    }

    public void Hit()
    {
        if (!ActiveForMining()) return;

        --currentHealth;

        AnimationAfterHit();
        CreateResource();

        if (ActiveForMining()) return;

        StartCoroutine(Recovery());
    }

    private void AnimationAfterHit()
    {
        objectGraphics.DORewind();

        objectGraphics.DOShakeScale(resourceData.ShakeDuration, resourceData.ShakePower,
            resourceData.ShakeVibrato, resourceData.ShakeRandomness);
    }

    private void SetStartHealth()
    {
        currentHealth = resourceData.Health;
    }

    public bool ActiveForMining()
    {
        return currentHealth > 0;
    }

    private IEnumerator Recovery()
    {
        objectGraphics.DORewind();
        objectGraphics.DOScale(originScale * resourceData.DisableScale, resourceData.ScaleDisableDuration);

        yield return new WaitForSeconds(resourceData.RecoveryTime);

        objectGraphics.DOScale(originScale, resourceData.ScaleToOriginDuration).OnComplete(() => SetStartHealth());
    }

    private void CreateResource()
    {
        for (int i = 0; i < resourceData.ResourceCountPerHit; i++)
        {
            var resource =  Instantiate(resourceData.Resource, transform.position + spawnResourcePositionOffset, Random.rotation);
            resource.ApplySpawnForce();
        }
    }
}
