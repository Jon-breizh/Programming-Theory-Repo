using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // ENCAPSULATION - variable declaration

    [SerializeField] private int m_damage;
    public int damage 
    {
        get {return m_damage; } 
        set { m_damage = value; } 
    }
    [SerializeField] private int speed;
    private Rigidbody projectileRb;

    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        projectileRb.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
