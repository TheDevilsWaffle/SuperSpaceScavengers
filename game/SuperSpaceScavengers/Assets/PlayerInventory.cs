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

    }
    private void Drop(Item item)
    {

    }

    private void OnPickUpDropItem(InputEventInfo _inputEventInfo)
    {
        if (item1 == null && availableItems.Count > 0)
        {
            item1 = availableItems[0];
            availableItems.Remove(item1);
            item1.OnPickedUp(transform);
        }
        else //item 1 not null
        {
            if (availableItems.Count > 0 && item2 == null) //item is avalable and we don't have anything in the second slot
            {
                item2 = item1;
                item1 = availableItems[0];
                availableItems.Remove(item1);
                item1.OnPickedUp(transform);
            }
            else
            {
                if (item1 != null)
                {
                    item1.OnDropped(transform);
                    item1 = null;
                }

                if (availableItems.Count > 0)
                {
                    item1 = availableItems[0];
                    availableItems.Remove(item1);
                    item1.OnPickedUp(transform);
                }
                else
                {
                    item1 = item2;
                    item2 = null;
                }
            }
        }

        //pick up available item
        //drop item1
        //drop item1 and pick up availableItem
        //pick up and put away item1

        //if (availableItems.Count == 0 && item1 != null)
        //    item1.OnDropped();

        //    item2 = item1;
        //}

        //if(availableItems.Count == 0)


        ////item1.OnDropped();

        //item1 = availableItems[0];
        //item1.OnPickedUp();
    }
}
