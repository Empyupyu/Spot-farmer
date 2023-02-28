using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PickUpable : MonoBehaviour
{
    [SerializeField] private ResourceData resourceData;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Collider collider;

    private bool canPickUp;

    public bool CanPickUp()
    {
        return canPickUp;
    }

    public void PickUp(Transform parent)
    {
        transform.parent = parent;

        TransitionToPoint();
    }

    private void TransitionToPoint()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;

        transform.DOLocalRotate(Vector3.zero, .8f);
        transform.DOLocalJump(transform.parent.transform.childCount * new Vector3(0,.5f, 0), 1, 1, 1f);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(resourceData.DelayPickUpOnSpawn);

        canPickUp = true;
    }
}
