using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatUnit : BasicUnit
{
    [SerializeField] private int attackValue; // Valeur de l'attaque
    [SerializeField] private float attackRate; // Taux d'attaque (combien de temps entre chaque attaque)
    public GameObject blocObject, attackObject; // Objets de blocage et d'attaque

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet en collision est un mur, une unité de défense ou un ennemi
        if (other.CompareTag("wall") || other.CompareTag("defenceUnit") || other.CompareTag("enemy"))
        {
            // Vérifie si l'objet en collision a une balise différente de celle de cet objet
            if (gameObject.tag != other.gameObject.tag)
            {
                // Effectue une attaque sur l'objet en collision
                Attack(other.gameObject);
                attackObject = other.gameObject;

                // Si cet objet est un ennemi, active le mode "en combat" et mémorise l'objet de blocage
                if (gameObject.CompareTag("enemy"))
                {
                    gameObject.GetComponent<EnemyScript>().inCombat = true;
                    blocObject = other.gameObject;
                }
            }

            // Si l'objet en collision est un ennemi et cet objet est aussi un ennemi
            if (other.CompareTag("enemy") && gameObject.CompareTag("enemy"))
            {
                // Si aucun objet de blocage n'est défini, mémorise l'objet en collision comme objet de blocage
                if (blocObject == null)
                {
                    blocObject = other.gameObject;
                }

                // Si l'ennemi en collision est déjà en combat, empêche ce mouvement
                if (other.GetComponent<EnemyScript>().inCombat)
                {
                    gameObject.GetComponent<EnemyScript>().canMove = false;
                }

                // Si l'ennemi en collision ne peut pas se déplacer, empêche ce mouvement
                if (!other.GetComponent<EnemyScript>().canMove)
                {
                    gameObject.GetComponent<EnemyScript>().canMove = false;
                }
            }
        }

        // Si l'objet en collision est un projectile et cet objet est un ennemi
        if (other.CompareTag("projectile") && gameObject.CompareTag("enemy"))
        {
            // Réduit les points de vie de cet objet en fonction des dégâts du projectile
            gameObject.GetComponent<BasicUnit>().RecievedDammage(other.gameObject.GetComponent<Projectile>().dammage);

            // Détruit le projectile
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si l'objet en collision est un ennemi, permet à cet ennemi de se déplacer à nouveau
        if (other.gameObject.CompareTag("enemy"))
        {
            gameObject.GetComponent<EnemyScript>().canMove = true;
        }
    }

    public void Attack(GameObject attackingObject)
    {
        // Lance une coroutine pour effectuer l'attaque
        StartCoroutine(AttackCor(attackingObject));
    }

    // Arrête l'attaque
    public void StopAttack()
    {
        // Arrête toutes les coroutines en cours (y compris l'attaque)
        StopAllCoroutines();
    }

    // Coroutine pour gérer l'attaque
    IEnumerator AttackCor(GameObject attackingObject)
    {
        while (true)
        {
            if (attackObject != null && attackingObject != null)
            {
                // Si cet objet est un ennemi, inflige des dégâts à l'objet attaqué en fonction de la valeur d'attaque
                if (gameObject.CompareTag("enemy"))
                {
                    attackingObject.GetComponent<BasicUnit>().RecievedDammage(attackValue, gameObject);
                }
                else
                {
                    // Si cet objet n'est pas un ennemi, inflige des dégâts à l'objet attaqué
                    attackingObject.GetComponent<BasicUnit>().RecievedDammage(attackValue);
                }

            }
            yield return new WaitForSeconds(attackRate); // Attends le prochain cycle d'attaque
        }
    }
}
