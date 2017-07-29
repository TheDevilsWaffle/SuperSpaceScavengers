//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    private new Rigidbody rigidbody = null;
    private new Collider collider = null;

    public PlayerInventory heldBy;
    public float itemUses = float.PositiveInfinity;
    public bool dropOnAttemptStore = false;

    [Header("Item Animations")]
    public Animate[] animateOnDetected = new Animate[0];
    public Animate[] animateOnLost = new Animate[0];
    public Animate[] animateOnUse = new Animate[0];

    public void OnDetected()
    {
        if (rigidbody.velocity.y < 0.01f && rigidbody.velocity.y > -0.01f)
        {
            rigidbody.velocity += Vector3.up * 2.5f;
            rigidbody.angularVelocity += Random.onUnitSphere * 1.5f;
        }

        foreach (Animate _animate in animateOnLost)
            _animate.Stop();

        foreach (Animate _animate in animateOnDetected)
            _animate.Play();
    }
    public void OnLost()
    {
        foreach (Animate _animate in animateOnDetected)
            _animate.Stop();

        foreach (Animate _animate in animateOnLost)
            _animate.Play();
    }

    public virtual void OnUse()
    {
        if (itemUses <= 0)
            return;

        --itemUses;

        foreach (Animate _animate in animateOnUse)
            _animate.Play();
    }

    public void Drop(Transform _transform, float _distance, float _height, float _velocityInheritance = 1)
    {
        heldBy = null;

        gameObject.SetActive(true);
        collider.enabled = true;

        transform.parent = null;
        rigidbody.isKinematic = false;

        rigidbody.velocity = _transform.root.GetComponent<Rigidbody>().velocity * _velocityInheritance;
        rigidbody.angularVelocity = Random.onUnitSphere * 5;
    }

    public void Hold(Transform _transform)
    {
        heldBy = _transform.root.GetComponent<PlayerInventory>();
        gameObject.SetActive(true);

        //wait for one physics update so that the object is removed
        //from range of detection collider and "lost", then equip
        StartCoroutine(DelayedHold(_transform));
    }
    public void Store()
    {
        if (dropOnAttemptStore)
        {
            heldBy.storedItem = null;
            Drop(heldBy.itemHolder, heldBy.dropDistance, heldBy.dropHeight);
        }
        else
            gameObject.SetActive(false);
    }

    private IEnumerator DelayedHold(Transform _transform)
    {
        transform.position += Vector3.up * 100;
        yield return new WaitForFixedUpdate();

        collider.enabled = false;

        transform.position = _transform.position;
        transform.rotation = _transform.rotation;
        transform.parent = _transform;

        rigidbody.isKinematic = true;
    }

    // Use this for initialization
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        if (heldBy != null)
            heldBy.PickUp(this);
        else
            Drop(transform, 0, 0, 0);
    }
}
