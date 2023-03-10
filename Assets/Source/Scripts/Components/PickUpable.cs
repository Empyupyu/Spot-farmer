using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PickUpable : MonoBehaviour
{
    [SerializeField] private ResourceData resourceData;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Collider collider;

    private bool canPickUp;

    public ResourceType GetResourceType()
    {
        return resourceData.ResourceType;
    }

    public bool CanPickUp()
    {
        return canPickUp;
    }

    public void PickUp(Transform parent)
    {
        transform.parent = parent;

        TransitionToLocalPoint();
    }

    public void Disable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
    }

    private void TransitionToLocalPoint()
    {
        Disable();
        transform.DOLocalRotate(Vector3.zero, .8f);
        transform.DOLocalJump(new Vector3(0,.5f, 0), 1, 1, 1f).OnComplete(() => gameObject.SetActive(false));
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(resourceData.DelayPickUpOnSpawn);

        canPickUp = true;
    }
}
