using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float fireRate = 1f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float minFireRate = 1f;
    [SerializeField] float maxFireRate = 10f;

    [HideInInspector] public bool isFiring;

    private Coroutine firingCoroutine;

    private void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position,
                                                        Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);

            if (useAI)
            {
            fireRate = UnityEngine.Random.Range(minFireRate, maxFireRate);
            }

            yield return new WaitForSeconds(fireRate);
        }
    }
}
