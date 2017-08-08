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
    public Material projectileMaterialOverride;

    public int projectilesPerBurst = 1;
    public float timeBetweenBursts = 0.01f;

    public float timeSinceShot = 0;
    private bool firing = false;

    protected Vector3 aimDirection;

    private void OnValidate()
    {
        timeBetweenShots = 1 / shotsPerSecond;

        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();
    }

    public void Fire()
    {
        if (timeSinceShot < timeBetweenShots)
            return;

        timeSinceShot = 0;
        StartCoroutine(ShootProjectiles());
    }

    private IEnumerator ShootProjectiles()
    {
        Vector3 _inheritedVelocity = Vector3.zero;

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 _spawnPosition = transform.GetChild(0).position + transform.right * projectileOffset.x + transform.up * projectileOffset.y + transform.forward * projectileOffset.z;
            if (rigidbody != null)
                _inheritedVelocity = rigidbody.velocity * velocityInheritanceRatio;

            projectile.FireNew(gameObject, _spawnPosition, transform.rotation, projectileSpeed, shotDistance, projectileSize,
                projectileDamage, projectileMaterialOverride, horizontalSpread, verticalSpread, _inheritedVelocity, seekingStrength);

            if (i % projectilesPerBurst == 0)
                yield return new WaitForSeconds(timeBetweenBursts);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        timeSinceShot += Time.deltaTime;
    }
}
