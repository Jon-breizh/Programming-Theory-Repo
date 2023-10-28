using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE : Extends the properties and methods of the parent class (CombatUnit).
public class DefenseUnit : CombatUnit
{    
    // ENCAPSULATION - variable declaration
    [SerializeField] private float fireRate;
    [SerializeField] private bool hasAmmo = false;
    [SerializeField] GameObject ammo;
    
    [SerializeField] private int m_costValue;
    public int costValue
    {
        get { return m_costValue; }
        set { m_costValue = value; }
    }

    [SerializeField] private Sprite m_image;
    public Sprite image
    {
        get { return m_image; }
        set { m_image = value; }
    }
    
    private AudioSource friendlyAS;

    void Start()
    {
        friendlyAS = GetComponent<AudioSource>();
        friendlyAS.volume = GameManager.Instance.effectVolumeValue;
        // If this unit has ammo, set up a repeating method to fire the ammo.
        if (hasAmmo)
        {
            InvokeRepeating("FireAmmo", 1.0f, fireRate);
        }
    }

    // ABSTRACTION - A higher-level method to handle firing ammo.
    private void FireAmmo()
    {
        // Calculate the position and rotation for the ammo and instantiate it.
        Vector3 ammoPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
        Quaternion ammoRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        Instantiate(ammo, ammoPosition, ammoRotation);
        friendlyAS.Play();
    }
}
