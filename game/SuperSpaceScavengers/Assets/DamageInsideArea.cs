using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInsideArea : MonoBehaviour
{
    public float radius = 5;
    public int tickCount = 1;
    public int damagePerTick = 10;
    public float timeBetweenTicks = 1;
    public float forcePerTick = 10;
    public float upwardForce = 0.5f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        while (tickCount > 0)
        {
            Collider[] _colliders = Physics.OverlapSphere(transform.position, radius);

            for (int j = 0; j < _colliders.Length; j++)
            {
                if (_colliders[j].transform.root == transform.root)
                    continue;

                Rigidbody _rigidbody = _colliders[j].GetComponent<Rigidbody>();
                if (_rigidbody != null)
                {
                    Vector3 _vecToTarget = (_rigidbody.position - transform.position);
                    float _distanceToTarget = _vecToTarget.magnitude;
                    if (_distanceToTarget == 0)
                        continue;
                    _vecToTarget /= _distanceToTarget;

                    _rigidbody.AddForce(new Vector3(_vecToTarget.x, upwardForce, _vecToTarget.z) * forcePerTick, ForceMode.Impulse);
                    _rigidbody.maxAngularVelocity = 30;
                    _rigidbody.angularVelocity += Random.onUnitSphere * forcePerTick;
                }

                HealthAndShields _health = _colliders[j].GetComponent<HealthAndShields>();
                if (_health != null)
                    _health.DealDamage(damagePerTick);
            }

            tickCount--;
            yield return new WaitForSeconds(timeBetweenTicks);
        }

        Destroy(gameObject);
    }
}
