//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : ItemReference
{
    public override Item item { get { return this; } }

    [HideInInspector]
    public new Rigidbody rigidbody = null;
    protected new Collider collider = null;

    public Vector3 heldOffset = Vector3.zero;
    public Vector3 heldRotation = Vector3.zero;

    public PlayerInventory heldBy;
    public float itemUses = float.PositiveInfinity;
    public bool dropOnAttemptStore = false;

    public bool beingPickedUp = false;

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
        if (heldBy != null)
        {
            heldBy.heldItem = null;
            heldBy = null;
        }

        gameObject.SetActive(true);
        collider.enabled = true;

        transform.parent = null;
        rigidbody.isKinematic = false;

        rigidbody.velocity = _transform.root.GetComponent<Rigidbody>().velocity * _velocityInheritance;
        rigidbody.maxAngularVelocity = 30;
        rigidbody.angularVelocity = Random.onUnitSphere * 5;
    }

    public virtual void Hold(Transform _transform)
    {
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
        beingPickedUp = true;

        Vector3 _startPosition = transform.position;
        transform.parent = _transform;
        Quaternion _startRotation = transform.localRotation;
        
        transform.position += Vector3.up * 100;
        yield return new WaitForFixedUpdate();

        collider.enabled = false;
        rigidbody.isKinematic = true;

        heldBy = _transform.root.GetComponent<PlayerInventory>();

        float _timer = 0;
        float _duration = 0.25f;

        while (_timer < _duration)
        {
            _timer += Time.deltaTime;
            float _ratio = heldBy.pickUpCurve.Evaluate(_timer / _duration);

            transform.position = Vector3.Lerp(_startPosition, _transform.TransformPoint(heldOffset), _ratio);
            transform.localRotation = Quaternion.Lerp(_startRotation, Quaternion.Euler(heldRotation), _ratio);

            yield return null;
        }

        transform.position = _transform.TransformPoint(heldOffset);
        transform.localRotation = Quaternion.Euler(heldRotation);

        beingPickedUp = false;
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
