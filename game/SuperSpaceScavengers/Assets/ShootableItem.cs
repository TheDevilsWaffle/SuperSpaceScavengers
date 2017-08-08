using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooting))]
public class ShootableItem : Item
{
    [HideInInspector]
    public Shooting shooting;

    void OnValidate()
    {
        if (shooting == null)
            shooting = GetComponent<Shooting>();
    }

    public override void OnUse()
    {
        if (itemUses <= 0)
            return;

        if (shooting.timeSinceShot < shooting.timeBetweenShots)
            return;

        base.OnUse();
        shooting.Fire();
    }
}
