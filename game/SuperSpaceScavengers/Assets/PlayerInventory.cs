//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private Player player;
    public new Rigidbody rigidbody;

    public Image item1Image;
    public Image item2Image;

    public DetectionSphere itemDetection;
    [HideInInspector]
    public List<Item> availableItems = new List<Item>();

    public Transform itemHolder = null;
    public AnimationCurve pickUpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Item heldItem_;
    private Item storedItem_;

    public float pickUpDistance = 1.5f;

    public float dropDistance = 1.25f;
    public float dropHeight = 1.25f;
    public float droppedInheritedVelocity = 1;

    public float throwStrength = 8;
    public float throwHeight = 3;
    public float thrownInheritedVelocity = 0.5f;

    public LookAtObject leftArm;
    public LookAtObject rightArm;

    private Item closestItem = null;

    public Item heldItem
    {
        get { return heldItem_; }
        set { heldItem_ = value; OnItemUpdated(heldItem, item1Image); if (value != null) heldItem_.Hold(itemHolder); }
    }
    public Item storedItem
    {
        get { return storedItem_; }
        set { storedItem_ = value; OnItemUpdated(storedItem, item2Image); if (value != null) storedItem.Store(); }
    }

    private void OnItemUpdated(Item _item, Image _itemImage)
    {
        if (_item == null)
        {
            _itemImage.enabled = false;
            return;
        }
        else
            _itemImage.enabled = true;

        _itemImage.color = _item.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;// GetColor("_EmissionColor");
    }

    void OnValidate()
    {
        if (itemDetection != null)
            itemDetection.sphereCollider.radius = pickUpDistance;

        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (availableItems.Count == 0)
        {
            closestItem = null;
            return;
        }

        float _closestSqrDistance = float.PositiveInfinity;
        Item _closestItem = null;

        for (int i = 0; i < availableItems.Count; i++)
        {
            if (availableItems[i] == null)
            {
                availableItems.RemoveAt(i);
                i--;

                if (availableItems.Count == 0)
                {
                    closestItem = null;
                    return;
                }
            }

            Vector3 _vecToItem = availableItems[i].transform.position - transform.position;
            float _sqrDistance = _vecToItem.sqrMagnitude;

            if (_sqrDistance < _closestSqrDistance)
            {
                _closestSqrDistance = _sqrDistance;
                _closestItem = availableItems[i];
            }
        }

        if (_closestItem == null || _closestItem == closestItem)
            return;

        if (closestItem != null)
            closestItem.OnLost();

        _closestItem.OnDetected();

        closestItem = _closestItem;
    }

    // Use this for initialization
    void Start()
    {
        itemDetection.callOnEnter += OnDetectItem;
        itemDetection.callOnExit += OnLoseItem;

        InputEvents.ThrowItem.Subscribe(ThrowItem);
        InputEvents.PickUpDropItem.Subscribe(OnPickUpDropItem);
        InputEvents.SwitchItem.Subscribe(SwitchItem);
        InputEvents.UseItem.Subscribe(UseItem);

        heldItem = heldItem;
        storedItem = storedItem;
    }

    private void SwitchItem(InputEventInfo _inputEventInfo)
    {
        Swap();
    }
    private void ThrowItem(InputEventInfo _inputEventInfo)
    {
        if (heldItem == null || heldItem.beingPickedUp)
            return;

        Item _thrownItem = heldItem;
        heldItem.Drop(thrownInheritedVelocity);

        _thrownItem.rigidbody.velocity += transform.forward * throwStrength + transform.up * throwHeight;
        _thrownItem.rigidbody.maxAngularVelocity = 30;
        _thrownItem.rigidbody.angularVelocity = _thrownItem.transform.right * _thrownItem.rigidbody.velocity.magnitude;
        
        Swap();
    }
    protected void UseItem(InputEventInfo _inputEventInfo)
    {
        if (heldItem != null && !heldItem.beingPickedUp)
            heldItem.OnUse();
    }
    private void OnPickUpDropItem(InputEventInfo _inputEventInfo)
    {
        if (heldItem != null && heldItem.beingPickedUp)
            return;

        if (availableItems.Count > 0) //objects are in range, pick up closest
            PickUp(closestItem);
        else if (heldItem != null) //no objects are in range, drop heldItem
        {
            heldItem.Drop(droppedInheritedVelocity);
            heldItem = null;

            Swap(); //will switch to secondary (if present)
        }
    }

    public void Swap()
    {
        if (heldItem != null && heldItem.beingPickedUp)
            return;

        if (heldItem != null && heldItem.dropOnAttemptStore)
        {
            heldItem.Drop();
            heldItem = storedItem;
            storedItem = null;
            return;
        }

        Item _itemTemp = heldItem;
        heldItem = storedItem;
        storedItem = _itemTemp;
    }
    public void PickUp(Item item)
    {
        if (heldItem != null && heldItem.beingPickedUp)
            return;

        if (heldItem != null)
        {
            if (storedItem == null)
                storedItem = heldItem;
            else
            {
                heldItem.Drop(droppedInheritedVelocity);
                heldItem = null;
            }
        }

        heldItem = item;
    }

    private void OnDetectItem(GameObject _gameObject)
    {
        Item _item = _gameObject.GetComponent<Item>();

        if (_item.heldBy != null)
            return;

        availableItems.Add(_item);
    }
    private void OnLoseItem(GameObject _gameObject)
    {
        Item _item = _gameObject.GetComponent<Item>();

        if (_item.heldBy != null)
            return;

        if (_item == closestItem)
            _item.OnLost();

        availableItems.Remove(_item);
    }

    void Destroy()
    {
        InputEvents.SwitchItem.Unsubscribe(SwitchItem);
        InputEvents.ThrowItem.Unsubscribe(ThrowItem);
        InputEvents.UseItem.Unsubscribe(UseItem);
        InputEvents.PickUpDropItem.Unsubscribe(OnPickUpDropItem);
    }
}
