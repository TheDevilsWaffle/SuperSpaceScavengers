using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(HealthAndShields))]
public class DamageOnImpact : MonoBehaviour
{
    [HideInInspector]
    public HealthAndShields health;
    
    public Vector2 impulseThresholdRange = Vector2.one * 5;
    public int baseDamage = 10;
    public int damagePerImpulseUnit = 0;

    void OnValidate()
    {
        health = GetComponent<HealthAndShields>();
    }

    void OnCollisionEnter(Collision _collision)
    {
        float _impulseThreshold = Random.Range(impulseThresholdRange.x, impulseThresholdRange.y);

        float _impulseTotal = _collision.impulse.magnitude;
        float _additionalImpulse = _impulseTotal - _impulseThreshold;

        if (_impulseTotal > _impulseThreshold)
            health.DealDamage(baseDamage + (int)_additionalImpulse * damagePerImpulseUnit);
    }
}
