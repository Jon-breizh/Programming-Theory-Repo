using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CombatUnit
{
    [SerializeField] private int mvtSpeed = 5;
    private Rigidbody enyRb;
    public bool canMove = true, onMvt = true, inCombat = false;
    public int coinValue;

    // INHERITANCE - Extends the properties and methods of the parent class (CombatUnit).
    void Start()
    {
        enyRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement of the enemy if it is not in contact with a defense unit.
        if ((!inCombat && canMove) || blocObject == null)
        {
            MoveForward();
        }
    }

    // ABSTRACTION - A higher-level method to make the enemy move forward.
    private void MoveForward()
    {
        enyRb.transform.Translate(Vector3.forward * mvtSpeed * Time.deltaTime);
    }
}
