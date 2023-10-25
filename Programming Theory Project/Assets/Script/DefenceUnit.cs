using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceUnit : CombatUnit
{
    [SerializeField] private float fireRate;
    [SerializeField] private bool hasAmmo = false;
    [SerializeField] GameObject ammo;

    public int costValue;
    public Sprite image;
    // Start is called before the first frame update
    void Start()
    {
        if (hasAmmo)
        {
            InvokeRepeating("FireAmmo", 1.0f, fireRate);
        }
    }

    private void FireAmmo()
    {
        Vector3 ammoPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
        Quaternion ammoRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        Instantiate(ammo, ammoPosition, ammoRotation);
    }
}
