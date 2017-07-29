using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public new Collider collider;
    [HideInInspector]
    public new Rigidbody rigidbody;

    public Transform target;
    public GameObject createOnDestroy;
    public float createOnDestroySizeRatio = 0.5f;

    private int damage = 1;

    private float distance = -1;
    private float speed = -1;
    private float maxTimeAlive = -1;

    private float seekingStrength;

    private float timeAlive = 0;
    private GameObject createdBy = null;

    void OnValidate()
    {
        if (collider == null)
            collider = GetComponent<Collider>();

        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();
    }

    public Projectile FireNew(GameObject _createdBy, Vector3 _position, Quaternion _rotation, float _projectileSpeed, float _shotDistance, float _size, int _damage, Material _materialOverride,
        float _horizontalSpread = 0, float _verticalSpread = 0, Vector3 _inheritedVelocity = default(Vector3), float _seekingStrength = 0, Transform _target = null) //optional parameters
    {
        GameObject _newProjectileObject = Instantiate(gameObject, _position, _rotation);

        if (_materialOverride != null)
            _newProjectileObject.GetComponent<MeshRenderer>().material = _materialOverride;

        float _randomAngle = Mathf.Deg2Rad * Random.Range(-_horizontalSpread, _horizontalSpread);
        Vector3 _direction = Vector3.RotateTowards(_rotation * Vector3.forward, Mathf.Sign(_randomAngle) * (_rotation * Vector3.right), Mathf.Abs(_randomAngle), 0);

        _randomAngle = Mathf.Deg2Rad * Random.Range(-_verticalSpread, _verticalSpread);
        _direction = Vector3.RotateTowards(_direction, Mathf.Sign(_randomAngle) * (_rotation * Vector3.up), Mathf.Abs(_randomAngle), 0);

        _newProjectileObject.GetComponent<Rigidbody>().velocity = _direction * _projectileSpeed + _inheritedVelocity;
        _newProjectileObject.transform.localScale = Vector3.one * _size;

        Projectile _newProjectile = _newProjectileObject.GetComponent<Projectile>();
        _newProjectile.Update();

        _newProjectile.createdBy = _createdBy;

        _newProjectile.distance = _shotDistance;
        _newProjectile.speed = _projectileSpeed;
        _newProjectile.maxTimeAlive = _newProjectile.distance / _newProjectile.speed;

        _newProjectile.damage = _damage;
        _newProjectile.seekingStrength = _seekingStrength;
        _newProjectile.target = _target;

        _newProjectileObject.GetComponentInChildren<MeshRenderer>().enabled = true;

        _newProjectile.target = target;

        return _newProjectile;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + rigidbody.velocity);

        timeAlive += Time.deltaTime;

        if (maxTimeAlive < 0)
            return;

        if (timeAlive > maxTimeAlive)
            DestroySelf();
    }

    void FixedUpdate()
    {
        SeekTarget();
    }

    void SeekTarget()
    {
        if (target == null)
            return;

        Vector3 _vecToTarget = target.position - transform.position;
        rigidbody.velocity = Vector3.RotateTowards(rigidbody.velocity, _vecToTarget, seekingStrength * Time.fixedDeltaTime, 0);
    }

    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.GetComponent<Projectile>() != null || _collider.gameObject == createdBy)
            return;

        HealthAndShields _healthAndShields = _collider.GetComponent<HealthAndShields>();

        if (_healthAndShields != null)
            _healthAndShields.DealDamage(damage);

        DestroySelf();
    }

    void DestroySelf()
    {
        if (createOnDestroy != null)
        {
            GameObject _createdObject = Instantiate(createOnDestroy, transform.position - rigidbody.velocity * Time.fixedDeltaTime, transform.rotation);
            _createdObject.transform.localScale = transform.localScale * createOnDestroySizeRatio;
        }

        Destroy(gameObject);
    }
}
