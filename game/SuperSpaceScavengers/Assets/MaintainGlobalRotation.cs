using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainGlobalRotation : MonoBehaviour
{
    public Vector3 rotation = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = rotation;
    }
}
