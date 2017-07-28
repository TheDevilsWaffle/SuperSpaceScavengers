using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    private new Rigidbody rigidbody = null;

    private bool detected = false;
    public Animate[] animateOnDetected = new Animate[0];
    public Animate[] animateOnLost = new Animate[0];

    public Animate[] animateOnPickedUp = new Animate[0];
    public Animate[] animateOnDropped = new Animate[0];

    public void OnDetected()
    {
        detected = true;

        foreach (Animate _animate in animateOnLost)
            _animate.Stop();

        foreach (Animate _animate in animateOnDetected)
            _animate.Play();
    }

    public void OnLost()
    {
        detected = false;

        foreach (Animate _animate in animateOnDetected)
            _animate.Stop();

        foreach (Animate _animate in animateOnLost)
            _animate.Play();
    }

    public void OnPickedUp(Transform _transform)
    {
        transform.position = _transform.position + Vector3.up * 100;
        StartCoroutine(DelayedDisable());
    }

    private IEnumerator DelayedDisable()
    {
        yield return null;
        yield return null;

        gameObject.SetActive(false);
    }

    public void OnDropped(Transform _transform, float _distance, float _height)
    {
        gameObject.SetActive(true);
        rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;

        transform.position = _transform.position + _transform.forward * _distance + Vector3.up * _height;
    }

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        OnDropped(transform, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
