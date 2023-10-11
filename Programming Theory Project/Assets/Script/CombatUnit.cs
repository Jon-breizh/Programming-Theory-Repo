using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatUnit : BasicUnit
{
    [SerializeField] private int attackValue;
    [SerializeField] private float attackRate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall") || other.CompareTag("defenceUnit") || other.CompareTag("enemy"))
        {
            Attack(other.gameObject);
            if (gameObject.CompareTag("enemy"))
            {
                gameObject.GetComponent<EnemyScript>().canMove = false;
            }
        }
    }

    public void Attack(GameObject attackingObject)
    {
        StartCoroutine(AttackCor(attackingObject));
    }

    //Arrêt de l'attaque
    public void StopAttack()
    {
        StopAllCoroutines();
    }

    //Coroutine d'attaque
    IEnumerator AttackCor(GameObject attackingObject)
    {
        while (true)
        {
            attackingObject.GetComponent<BasicUnit>().RecievedDammage(attackValue, gameObject);
            yield return new WaitForSeconds(attackRate);
        }
    }
}
