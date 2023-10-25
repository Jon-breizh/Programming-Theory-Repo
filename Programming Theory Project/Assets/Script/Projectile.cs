using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    [SerializeField] private int speed;
    private Rigidbody projectileRb;

    // ABSTRACTION - A higher-level method to make the projectile move forward.
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        projectileRb.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
