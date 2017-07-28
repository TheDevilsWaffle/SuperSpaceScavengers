using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [HideInInspector]
    public new Rigidbody rigidbody = null;

    public Vector3 projectileOffset = Vector3.forward;

    public float shotsPerSecond = 1;
    //[ReadOnly]
    public float timeBetweenShots;

    public int projectileDamage = 10;
    public int projectileCount = 10;

    public float shotDistance = 10;
    public float projectileSpeed = 50;

    public float projectileSize = 0.3f;

    public float horizontalSpread = 10;
    public float verticalSpread = 5;

    [Range(0, 1)]
    public float velocityInheritanceRatio;
    public float seekingStrength;

    public Projectile projectile;

    protected float timeSinceShot = 0;
    protected bool firing = false;

    protected Vector3 aimDirection;

    void OnValidate()
    {
        timeBetweenShots = 1 / shotsPerSecond;
    }

    // Use this for initialization
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    protected void Fire()
    {
        if (timeSinceShot < timeBetweenShots)
            return;

        timeSinceShot = 0;
        StartCoroutine(ShootProjectiles());
    }

    private IEnumerator ShootProjectiles()
    {
        Vector3 _spawnPosition = transform.GetChild(0).position + transform.right * projectileOffset.x + transform.up * projectileOffset.y + transform.forward * projectileOffset.z;
        Vector3 _inheritedVelocity = Vector3.zero;

        if (rigidbody != null)
            _inheritedVelocity = rigidbody.velocity * velocityInheritanceRatio;

        for (int i = 0; i < projectileCount; i++)
        {
            projectile.FireNew(gameObject, _spawnPosition, transform.rotation, projectileSpeed, shotDistance, projectileSize, projectileDamage, horizontalSpread, verticalSpread, _inheritedVelocity, seekingStrength);

            if (i % 3 == 0)
                yield return null;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        timeSinceShot += Time.deltaTime;
    }
}
