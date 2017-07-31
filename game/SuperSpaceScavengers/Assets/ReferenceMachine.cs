using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ReferenceMachine : ItemReference
{
    public ItemReference emptyReference;

    public override Item item
    {
        get
        {
            while (references.Count > 0)
            {
                int _randomPosition = Random.Range(0, references.Count);

                if (references[_randomPosition] == null)
                {
                    references.RemoveAt(_randomPosition);
                    continue;
                }

                return references[_randomPosition];
            }

            if (emptyReference == null)
                return null;

            return emptyReference.item;
        }
    }

    private List<Item> references = new List<Item>();

    void OnTriggerEnter(Collider _collider)
    {
        Item _item = _collider.GetComponent<Item>();
        if (_item == null)
            return;

        references.Add(_item);
    }

    void OnTriggerExit(Collider _collider)
    {
        Item _item = _collider.GetComponent<Item>();
        if (_item == null)
            return;

        references.Remove(_item);
    }
}
