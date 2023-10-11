using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicUnit : MonoBehaviour
{
    [SerializeField] private int life;

    //Fonction de gestion des dégâts
    public void RecievedDammage(int dammageValue, GameObject attackingObject)
    {
        life -= dammageValue;
        if (life <= 0)
        {
            Destroy(gameObject);
            attackingObject.GetComponent<CombatUnit>().StopAttack();
            if (attackingObject.CompareTag("enemy"))
            {
                attackingObject.GetComponent<EnemyScript>().canMove = true;
            }
        }
    }
}
