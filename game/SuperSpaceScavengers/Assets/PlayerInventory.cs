using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Player player;
    public Item item1;
    public Item item2;

    public DetectionSphere itemDetection;
    public List<Item> availableItems = new List<Item>();

    // Use this for initialization
    void Start()
    {
        itemDetection.callOnEnter += OnDetectItem;
        itemDetection.callOnExit += OnLoseItem;

        InputEvents.PickUpDropItem.Subscribe(OnPickUpDropItem);
        InputEvents.SwitchItem.Subscribe(OnSwitchItem);
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

        item1.OnDropped(transform);
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
