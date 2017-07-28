using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private Player player;

    public Image item1Image;
    public Image item2Image;

    public DetectionSphere itemDetection;
    public List<Item> availableItems = new List<Item>();

    private Item item1_;
    private Item item2_;

    public float dropDistance = 1.25f;
    public float dropHeight = 1.25f;
    public float pickUpDistance = 1.5f;

    public float throwStrength = 8;
    public float throwHeight = 3;

    public Item item1
    {
        get { return item1_; }
        set { item1_ = value; OnItemUpdated(item1, item1Image); }
    }
    public Item item2
    {
        get { return item2_; }
        set { item2_ = value; OnItemUpdated(item2, item2Image); }
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

        _itemImage.color = _item.GetComponent<MeshRenderer>().material.color;// GetColor("_EmissionColor");
    }

    void OnValidate()
    {
        if (itemDetection != null)
            itemDetection.sphereCollider.radius = pickUpDistance;
    }

    // Use this for initialization
    void Start()
    {
        itemDetection.callOnEnter += OnDetectItem;
        itemDetection.callOnExit += OnLoseItem;

        InputEvents.ThrowItem.Subscribe(OnThrowItem);
        InputEvents.PickUpDropItem.Subscribe(OnPickUpDropItem);
        InputEvents.SwitchItem.Subscribe(OnSwitchItem);

        item1 = item1;
        item2 = item2;
    }

    private void OnThrowItem(InputEventInfo _inputEventInfo)
    {
        Item _thrownItem = item1;
        DropItem1(false);
        _thrownItem.GetComponent<Rigidbody>().velocity = transform.forward * throwStrength + transform.up * throwHeight;
    }

    private void OnSwitchItem(InputEventInfo _inputEventInfo)
    {
        SwitchItems();
    }

    public void SwitchItems()
    {
        Item _itemTemp = item1;
        item1 = item2;
        item2 = _itemTemp;
    }

    private void OnDetectItem(GameObject _gameObject)
    {
        _gameObject.GetComponent<Item>().OnDetected();
        availableItems.Add(_gameObject.GetComponent<Item>());
    }

    private void OnLoseItem(GameObject _gameObject)
    {
        _gameObject.GetComponent<Item>().OnLost();
        availableItems.Remove(_gameObject.GetComponent<Item>());
    }

    private void PickUp(Item item)
    {
        if (item1 != null)
        {
            if (item2 == null)
                item2 = item1;
            else
                DropItem1(true); //true because we are attempting to pick up another item
        }

        item1 = availableItems[0];
        //availableItems.Remove(item1);
        item1.OnPickedUp(transform);
    }
    private void DropItem1(bool pickingUpItem)
    {
        if (item1 == null)
            return;

        item1.OnDropped(transform, dropHeight, dropDistance);
        item1 = null;

        if (!pickingUpItem)
            SwitchItems();
    }

    private void OnPickUpDropItem(InputEventInfo _inputEventInfo)
    {
        if (availableItems.Count > 0)
            PickUp(availableItems[0]);
        else
            DropItem1(false); //false because we are not picking up another item
    }
}
