using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravityVolume : MonoBehaviour
{
    [HideInInspector]
    public new Collider collider;

    void OnValidate()
    {
        if (collider == null)
        {
            collider = GetComponent<Collider>();
        }

        collider.isTrigger = true;

        collider.enabled = enabled;
    }

    // Use this for initialization
    void OnEnable()
    {
        collider.enabled = true;
    }

    void OnDisable()
    {
        collider.enabled = false;
    }

    void OnTriggerEnter(Collider _collider)
    {
        Rigidbody _rigidbody = _collider.GetComponent<Rigidbody>();

        if (_rigidbody == null)
            return;

        _rigidbody.maxAngularVelocity = 4;
        _rigidbody.angularVelocity += Random.onUnitSphere * 4;
    }

    void OnTriggerStay(Collider _collider)
    {
        Rigidbody _rigidbody = _collider.GetComponent<Rigidbody>();

        if (_rigidbody == null)
            return;

        float _relativeVertical = _rigidbody.position.y - transform.position.y;
        float _ratio = _relativeVertical / transform.localScale.y / 2;

        _rigidbody.AddForce(Vector3.up * (9.81f - _ratio * 3f) * Random.Range(0.9f, 1.1f));

        _rigidbody.velocity *= Mathf.Clamp01((1 - _ratio) + 0.3f);
        //_rigidbody.AddForceAtPosition(Vector3.up * (9.81f - _ratio * 3f) * Random.Range(0.9f, 1.1f), _rigidbody.centerOfMass);// + Random.insideUnitSphere * 0.0001f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
