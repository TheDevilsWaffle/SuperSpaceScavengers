﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{
    [HideInInspector]
    public SphereCollider sphereCollider;

    public delegate void DetectionDelegate(GameObject _gameObject);
    public DetectionDelegate callOnEnter = delegate { };
    public DetectionDelegate callOnExit = delegate { };

    void OnValidate()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider _collider)
    {
        callOnEnter(_collider.gameObject);
    }

    void OnTriggerExit(Collider _collider)
    {
        callOnExit(_collider.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
