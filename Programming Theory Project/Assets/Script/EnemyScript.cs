using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class EnemyScript : MonoBehaviour
{

    //Fait avancer l'enemie vers la base du joueur
    public void MoveFwd(float speed, Rigidbody rb)
    {
        rb.transform.Translate(Vector3.forward * -speed * Time.deltaTime);
    }

    //script d'attaque de l'ennemie sur une unité de défence
    public void Attack(GameObject attackGo, int attackValue, float rate)
    {
        StartCoroutine(AttackNum(attackGo, attackValue, rate));
    }

    //Arrêt de l'attaque
    public void StopAttack()
    {
        StopAllCoroutines();
    }

    //Coroutine d'attaque
    IEnumerator AttackNum(GameObject attackGo, int attackValue, float rate)
    {
        while (true)
        {
            attackGo.GetComponent<DefenceObjet>().RecievedDammage(attackValue, gameObject);
            yield return new WaitForSeconds(rate);
        }
    }
}
