using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int dammage;
    [SerializeField] private int speed;
    private Rigidbody projectileRb;
    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileRb.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
