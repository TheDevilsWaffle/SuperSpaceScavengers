using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
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
        gameObject.SetActive(false);
    }

    public void OnDropped(Transform _transform)
    {
        gameObject.SetActive(true);
        transform.position = _transform.position + Vector3.right;
    }

    // Use this for initialization
    void Start()
    {
        OnDropped(transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
