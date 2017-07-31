using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReference : MonoBehaviour
{
    private Item item_;
    public virtual Item item
    {
        get { return item_; }
        set { item_ = value; }
    }
}
