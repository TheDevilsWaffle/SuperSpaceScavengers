using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndShields : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    private int delayedHealth = 100;

    [Header("Display Settings")]
    public bool displayHealthBar = true;
    public bool dontDisplayAtFull = true;
    public float healthDelay = 0.3f;
    public float delayedLerpSpeed = 2f;
    //public ResourceMeter healthBarPrefab = null;
    //public Vector3 offset = new Vector3(0, 0, 3);
    public ResourceMeter healthBar = null;

    public int fixedFrameDelay = 1;

    public GameObject[] createOnKilled;

    public delegate void HealthDelegate(int _health);
    private HealthDelegate healthChangeDelegate = delegate { };

    public void Start()
    {
        //if (healthBarPrefab == null || displayHealthBar == false)
        //{
        //    DealDamage(0);
        //    return;
        //}

        //GameObject _healthBarObject = null;
        //_healthBarObject = Instantiate(healthBarPrefab.gameObject, transform.position, transform.rotation);

        //healthBar = _healthBarObject.GetComponent<ResourceMeter>();

        DealDamage(0);
    }

    public void SubscribeToChanges(HealthDelegate _functionToCall)
    {
        healthChangeDelegate += _functionToCall;
    }

    public void OnHealthUpdated()
    {
        if (healthBar != null)
        {
            healthBar.mainFill.fillAmount = (float)health / maxHealth;

            if (dontDisplayAtFull && healthBar.mainFill.fillAmount == 1)
                healthBar.enabled = false;
            else
                healthBar.enabled = true;
        }

        healthChangeDelegate(health);

        if (health <= 0)
            Kill();
    }

    public void SetHealth(int _newHealth)
    {
        StartCoroutine(DelayedDamage(healthDelay, _newHealth - health));
        health = _newHealth;
        OnHealthUpdated();
    }
    public void DealDamage(int _damage)
    {
        health -= _damage;
        OnHealthUpdated();

        StartCoroutine(DelayedDamage(healthDelay, _damage));
    }

    private IEnumerator DelayedDamage(float _delay, int _damage)
    {
        yield return new WaitForSeconds(_delay);
        delayedHealth -= _damage;

        //if (healthBar != null && healthBar.delayedFill != null)
        //    healthBar.delayedFill.fillAmount = (float)delayedHealth / maxHealth;
    }

    void Kill()
    {
        StartCoroutine(DelayedKill());
    }

    private IEnumerator DelayedKill()
    {
        while (fixedFrameDelay > 0)
        {
            fixedFrameDelay--;
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);

        foreach (GameObject _gameObject in createOnKilled)
            Instantiate(_gameObject, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            DealDamage(10);

        if (healthBar != null && healthBar.delayedFill != null)
            healthBar.delayedFill.fillAmount = Mathf.MoveTowards(healthBar.delayedFill.fillAmount, (float)delayedHealth / maxHealth, Time.deltaTime * delayedLerpSpeed);
    }

    void OnDestroy()
    {
        if (healthBar != null)
            Destroy(healthBar.gameObject);
    }
}
