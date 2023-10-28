using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE - Extends the properties and methods of the parent class (CombatUnit).
public class EnemyScript : CombatUnit
{
    // ENCAPSULATION - variable declaration
    [SerializeField] private int m_coinValue; 
    public int coinValue
    {
        get { return m_coinValue; }
        set { m_coinValue = value; }
    }

    [SerializeField] private int mvtSpeed = 5;
    private Rigidbody enyRb;
    public bool canMove = true, onMvt = true, inCombat = false;
    public Animator animator;

    //Sound Management
    AudioSource EnemySound;
    public AudioClip hitSound, attackSound;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        enyRb = GetComponent<Rigidbody>();
        EnemySound = GetComponent<AudioSource>();
        EnemySound.volume = GameManager.Instance.effectVolumeValue;

    }

    void Update()
    {
    // Movement of the enemy if it is not in contact with a defense unit.
        if ((!inCombat && canMove) || blocObject == null)
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        enyRb.transform.Translate(Vector3.forward * mvtSpeed * Time.deltaTime);
    }
}
