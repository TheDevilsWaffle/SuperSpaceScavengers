using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainGlobalRotation : MonoBehaviour
{
    public Vector3 rotation = Vector3.zero;
        [HideInInspector]
    public Quaternion quaternionRotation = Quaternion.identity;

    void OnValidate()
    {
        quaternionRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //if(transform)
        transform.rotation = quaternionRotation;
    }
}
