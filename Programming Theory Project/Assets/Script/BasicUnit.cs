using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicUnit : MonoBehaviour
{
    [SerializeField] private int life;
    
    //Fonction de gestion des dégâts
    public void RecievedDammage(int dammageValue)
    {
        life -= dammageValue;
        if (life <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void RecievedDammage(int dammageValue, GameObject enemy)
    {
        life -= dammageValue;
        if (life <= 0)
        {
            Destroy(gameObject);
            enemy.GetComponent<EnemyScript>().inCombat = false;
            enemy.GetComponent<EnemyScript>().canMove = true;
        }
    }
}
