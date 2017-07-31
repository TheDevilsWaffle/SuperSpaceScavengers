using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingableItem : Item
{
    [Header("Swingable Settings")]
    public BoxCollider blade;
    public BoxCollider hilt;

    private Transform parent;
    private bool hasParent;

    public float timeBetweenSwings = 1;
    private float timeSinceSwung = 0;

    private Vector3 parentStartPosition;
    private Quaternion parentStartRotation;
    private Vector3 parentStartScale;

    private Vector3 relativeStartPosition;
    private Quaternion relativeStartRotation;

    private bool canUnstick = true;

    //// Use this for initialization
    public new void Start()
    {
        base.Start();
        timeSinceSwung = timeBetweenSwings;
    }

    public override void OnUse()
    {
        if (timeSinceSwung < timeBetweenSwings)
            return;

        timeSinceSwung = 0;

        base.OnUse();

        StartCoroutine(DamageAfterDelay());
    }
    public override void Hold(Transform _transform)
    {
        Unstick();
        base.Hold(_transform);
    }

    private IEnumerator DamageAfterDelay()
    {
        yield return new WaitForSeconds(0.03f);

        float _timer = 0;

        while (_timer < 0.1f)
        {
            _timer += Time.deltaTime;
            bool _dealtDamage = false;

            Collider[] _colliders = Physics.OverlapBox(blade.transform.position, new Vector3(0.3f, 0.5f, 0.3f), blade.transform.rotation);
            foreach (Collider _collider in _colliders)
            {
                if (_collider.transform.root == transform.root)
                    continue;

                HealthAndShields _health = _collider.GetComponent<HealthAndShields>();
                if (_health != null)
                    _health.DealDamage(10);

                if (_collider.gameObject.layer == 8)
                {
                    Vector3 _rotation = blade.transform.eulerAngles - Vector3.forward * 90;
                    Vector3 _position = blade.transform.parent.position;

                    foreach (Animate _animate in animateOnUse)
                        _animate.Stop();

                    transform.eulerAngles = _rotation;
                    transform.position += _position - blade.transform.parent.position;

                    if (heldBy == null)
                        continue;

                    Drop(heldBy.itemHolder, heldBy.dropDistance, heldBy.dropHeight);
                    Stick(_collider.transform);
                }

                _dealtDamage = true;
                break;
            }

            if (_dealtDamage)
                break;

            yield return null;
        }

    }

    // Update is called once per frame
    void OnCollisionEnter(Collision _collision)
    {
        if (_collision.contacts[0].thisCollider == blade && _collision.impulse.sqrMagnitude > 25)
        {
            Stick(_collision.collider.transform);

            if (parent == null)
                return;

            HealthAndShields _health = parent.GetComponent<HealthAndShields>();
            if (_health != null)
                _health.DealDamage(20);
        }
    }

    void Stick(Transform _stuckInObject)
    {
        if (beingPickedUp)
            return;

        parent = _stuckInObject;
        hasParent = true;

        parentStartPosition = parent.position;
        parentStartRotation = parent.rotation;
        parentStartScale = parent.lossyScale;

        relativeStartPosition = parent.InverseTransformPoint(transform.position);
        relativeStartRotation = Quaternion.Inverse(parent.transform.rotation) * transform.rotation;

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        blade.enabled = false;
        hilt.enabled = false;
        rigidbody.useGravity = false;

        rigidbody.isKinematic = true;

        //rigidbody.Sleep();
        //rigidbody.sleepThreshold = 10;

        //StartCoroutine(ControlUnstick());
    }

    private IEnumerator ControlUnstick()
    {
        canUnstick = false;
        yield return new WaitForSeconds(0.25f);
        canUnstick = true;
    }

    public void Unstick()
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForceAtPosition(Vector3.up * 1f, transform.position + transform.forward * 0.3f + transform.up * 0.03f, ForceMode.VelocityChange);
        rigidbody.angularVelocity = Random.onUnitSphere * 3;

        blade.enabled = true;
        hilt.enabled = true;
        rigidbody.useGravity = true;

        parent = null;
        hasParent = false;
    }

    void Update()
    {
        timeSinceSwung += Time.deltaTime;

        if (!hasParent)
            return;

        if (parent == null || !parent.gameObject.activeInHierarchy/* || !rigidbody.IsSleeping() && canUnstick*/)
        {
            Unstick();
        }
        else
        {
            Vector3 _relativePosition = parent.TransformPoint(relativeStartPosition);

            //transform.position = parent.TransformPoint(relativeStartPosition);
            //transform.rotation = parent.transform.rotation * relativeStartRotation;

            //if ((_relativePosition - transform.position).magnitude > 0.25f)
            //{
            //    Unstick();
            //    return;
            //}

            rigidbody.MovePosition(parent.TransformPoint(relativeStartPosition));
            rigidbody.MoveRotation(parent.transform.rotation * relativeStartRotation);
        }
        //else if (parentStartPosition != parent.position || parentStartRotation != parent.rotation || parentStartScale != parent.lossyScale)
        //{
        //    rigidbody.isKinematic = false;
        //    rigidbody.AddForceAtPosition((parent.position - parentStartPosition) * 120 * Time.deltaTime + Vector3.up * 1f, transform.position + transform.forward * 0.3f + transform.up * 0.03f, ForceMode.VelocityChange);
        //    rigidbody.angularVelocity = Random.onUnitSphere * 3;

        //    parent = null;
        //    hasParent = false;
        //}
    }
}
