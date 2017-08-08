using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorItem : Item
{
    [Header("Creation Options")]
    public GameObject[] objectsToCreate = new GameObject[0];
    public ItemReference[] itemsToCreate = new ItemReference[0];

    public GameObject[] createOnSucceed = new GameObject[0];
    public GameObject[] createOnFail = new GameObject[0];

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

        Vector3 _spawnPosition = transform.TransformPoint(positionOffset);

        foreach (GameObject objectToCreate in objectsToCreate)
            Instantiate(objectToCreate, _spawnPosition, transform.rotation);

        foreach (ItemReference itemToCreate in itemsToCreate)
            if (itemToCreate.item != null)
            {
                Instantiate(itemToCreate.item.gameObject, _spawnPosition, transform.rotation);

                foreach (GameObject objectToCreate in createOnSucceed)
                    Instantiate(objectToCreate, _spawnPosition, transform.rotation);
            }
            else
                foreach (GameObject objectToCreate in createOnFail)
                    Instantiate(objectToCreate, _spawnPosition, transform.rotation);

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
