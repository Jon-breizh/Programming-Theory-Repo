using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyScript : CombatUnit
{
    [SerializeField] private int mvtSpeed = 5;
    private Rigidbody enyRb;
    public bool canMove = true;
    void Start()
    {
        enyRb = GetComponent<Rigidbody>();
    }
        void Update()
    {
        //D�placement de l'ennemie si il n'est pas au contact d'une unit� de d�fence
        if (canMove)
        {
            MoveFwd();
        }
    }
        //Fait avancer l'enemie vers la base du joueur
    public void MoveFwd()
    {
        enyRb.transform.Translate(Vector3.forward * -mvtSpeed * Time.deltaTime);
    }
}