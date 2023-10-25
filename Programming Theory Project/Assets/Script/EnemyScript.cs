using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyScript : CombatUnit
{
    [SerializeField] private int mvtSpeed = 5;
    private Rigidbody enyRb;
    public bool canMove = true, onMvt = true, inCombat = false;
    public int coinValue;

   // private Animator animatorEny;

    void Start()
    {
        enyRb = GetComponent<Rigidbody>();
       // animatorEny = GetComponent<Animator>();

    }
        void Update()
    {
        //Déplacement de l'ennemie si il n'est pas au contact d'une unité de défence
        if ((!inCombat && canMove) || blocObject == null)
        {
            MoveFwd();
          //  animatorEny.SetBool("IsRunning", true);
        }
        else
        {
         //   animatorEny.SetBool("IsRunning", false);
        }
    }
        //Fait avancer l'enemie vers la base du joueur
    public void MoveFwd()
    {
        enyRb.transform.Translate(Vector3.forward * mvtSpeed * Time.deltaTime);
    }

}