using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBasic01 : EnemyScript
{
    [SerializeField] private int mvtSpeed = 5;
    [SerializeField] private int attaqueValue = 1;
    [SerializeField] private float attaqueRate = 0.5f;
    private Rigidbody eny01Rb;
    public bool contact = false;
    void Start()
    {
        eny01Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Déplacement de l'ennemie si il n'est pas au contact d'une unité de défence
        if (!contact)
        {
            MoveFwd(mvtSpeed, eny01Rb);
        }
    }

    // réaction en cas de contact avec une unité de défence
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("wall") || other.CompareTag("defenceUnit"))
        {
            contact = true;
            Attack(other.gameObject, attaqueValue, attaqueRate);
        }
    }
}
