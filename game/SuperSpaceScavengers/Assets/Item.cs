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

    public Transform holdPoint;

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

    public void Drop(float _velocityInheritance = 1)
    {
        gameObject.SetActive(true);
        collider.enabled = true;

        transform.parent = null;
        rigidbody.isKinematic = false;

        if (heldBy != null)
        {
            heldBy.leftArm.positionObject = null;
            heldBy.rightArm.positionObject = null;

            heldBy.leftArm.transform.localPosition = heldBy.leftArm.restingPosition;
            heldBy.rightArm.transform.localPosition = heldBy.rightArm.restingPosition;

            if (heldBy.rigidbody != null)
                rigidbody.velocity = heldBy.rigidbody.velocity * _velocityInheritance;

            heldBy.heldItem = null;
            heldBy = null;
        }

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
        heldBy.leftArm.positionObject = null;
        heldBy.rightArm.positionObject = null;

        heldBy.leftArm.transform.localPosition = heldBy.leftArm.restingPosition;
        heldBy.rightArm.transform.localPosition = heldBy.rightArm.restingPosition;

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

        heldBy.leftArm.startPosition = heldBy.leftArm.transform.position;
        heldBy.rightArm.startPosition = heldBy.rightArm.transform.position;

        float _timer = 0;
        float _duration = 0.25f;

        while (_timer < _duration)
        {
            _timer += Time.deltaTime;
            float _ratio = heldBy.pickUpCurve.Evaluate(_timer / _duration);

            if (holdPoint.position.y < 50)
            {
                heldBy.leftArm.transform.position = Vector3.Lerp(heldBy.leftArm.startPosition, holdPoint.TransformPoint(heldBy.leftArm.positionOffset), _ratio);
                heldBy.rightArm.transform.position = Vector3.Lerp(heldBy.rightArm.startPosition, holdPoint.TransformPoint(heldBy.rightArm.positionOffset), _ratio);
            }

            transform.position = Vector3.Lerp(_startPosition, _transform.TransformPoint(heldOffset), _ratio);
            transform.localRotation = Quaternion.Lerp(_startRotation, Quaternion.Euler(heldRotation), _ratio);

            yield return null;
        }

        heldBy.leftArm.positionObject = holdPoint;
        heldBy.rightArm.positionObject = holdPoint;

        transform.position = _transform.TransformPoint(heldOffset);
        transform.localRotation = Quaternion.Euler(heldRotation);

        beingPickedUp = false;
    }

    // Use this for initialization
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        if (holdPoint == null)
            holdPoint = transform.GetChild(0);

        if (heldBy != null)
            heldBy.PickUp(this);
        else
            Drop(0);
    }
}
