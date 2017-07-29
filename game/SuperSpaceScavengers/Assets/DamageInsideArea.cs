using System;
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
                HealthAndShields _health = _colliders[j].GetComponent<HealthAndShields>();
                if (_health != null)
                    _health.DealDamage(damagePerTick);

                Rigidbody _rigidbody = _colliders[j].GetComponent<Rigidbody>();
                if (_rigidbody != null)
                    _rigidbody.AddForce((_rigidbody.position - transform.position).normalized * forcePerTick, ForceMode.Impulse);
            }

            tickCount--;
            yield return new WaitForSeconds(timeBetweenTicks);
        }

        Destroy(gameObject);
    }
}
