using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUnit : CombatUnit
{
    [SerializeField] private float fireRate;
    [SerializeField] private bool hasAmmo = false;
    [SerializeField] GameObject ammo;

    public int costValue;
    public Sprite image;

    // ABSTRACTION - The Start method is called before the first frame update, abstracting the Unity framework details.
    void Start()
    {
        // If this unit has ammo, set up a repeating method to fire the ammo.
        if (hasAmmo)
        {
            InvokeRepeating("FireAmmo", 1.0f, fireRate);
        }
    }

    // ENCAPSULATION - Getter and setter methods for costValue and image can be implemented here.

    // ABSTRACTION - A higher-level method to handle firing ammo.
    private void FireAmmo()
    {
        // Calculate the position and rotation for the ammo and instantiate it.
        Vector3 ammoPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
        Quaternion ammoRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        Instantiate(ammo, ammoPosition, ammoRotation);
    }
}
