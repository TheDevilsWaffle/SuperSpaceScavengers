using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorItem : Item
{
    [Header("Creation Options")]
    public GameObject[] objectsToCreate = new GameObject[0];
    public Vector3 positionOffset = Vector3.zero;
    public bool destroyOnOutOfUses;

    public float creationDelay = 0;
    public float timeBetweenUses = 1;

    private float timeSinceUsed = 0;

    public override void OnUse()
    {
        if (timeSinceUsed < timeBetweenUses)
            return;

        timeSinceUsed = 0;

        base.OnUse();
        StartCoroutine(CreateAfterTime());
    }

    private IEnumerator CreateAfterTime()
    {
        dropOnAttemptStore = true;

        yield return new WaitForSeconds(creationDelay);

        foreach (GameObject objectToCreate in objectsToCreate)
            Instantiate(objectToCreate, transform.position + positionOffset, transform.rotation);

        if (destroyOnOutOfUses && itemUses <= 0)
        {
            if (heldBy != null)
            {
                heldBy.heldItem = null;
                heldBy.Swap();
            }

            Destroy(gameObject);
        }
        else
            dropOnAttemptStore = false;
    }

    void Update()
    {
        timeSinceUsed += Time.deltaTime;
    }

    protected new void Start()
    {
        base.Start();

        timeSinceUsed = timeBetweenUses;
    }
}
