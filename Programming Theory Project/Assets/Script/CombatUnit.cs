using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatUnit : BasicUnit
{
    [SerializeField] private int attackValue; // Valeur de l'attaque
    [SerializeField] private float attackRate; // Taux d'attaque (combien de temps entre chaque attaque)
    public GameObject blocObject, target; // Objets de blocage et d'attaque

    //public Animator animator;

    private void Start()
    {
       //animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet en collision est un mur, une unit� de d�fense ou un ennemi
        if ((other.CompareTag("Player") || other.CompareTag("defenceUnit") || other.CompareTag("enemy")) && !other.CompareTag("projectile"))
        {
            // V�rifie si l'objet en collision a une balise diff�rente de celle de cet objet
            if (gameObject.tag != other.gameObject.tag)
            {
                // Effectue une attaque sur l'objet en collision
                Attack(other.gameObject);
                target = other.gameObject;
                //animator.SetTrigger("Attack");

                // Si cet objet est un ennemi, active le mode "en combat" et m�morise l'objet de blocage
                if (gameObject.CompareTag("enemy"))
                {
                    gameObject.GetComponent<EnemyScript>().inCombat = true;
                    blocObject = other.gameObject;
                 //   animator.SetTrigger("Attack");
                }
            }

            // Si l'objet en collision est un ennemi et cet objet est aussi un ennemi
            if (other.CompareTag("enemy") && gameObject.CompareTag("enemy"))
            {
                // Si aucun objet de blocage n'est d�fini, m�morise l'objet en collision comme objet de blocage
                if (blocObject == null)
                {
                    blocObject = other.gameObject;
                    //animator.SetTrigger("Stop");
                }

                // Si l'ennemi en collision est d�j� en combat, emp�che ce mouvement
                if (other.GetComponent<EnemyScript>().inCombat)
                {
                    gameObject.GetComponent<EnemyScript>().canMove = false;
                    //animator.SetTrigger("Stop");
                }

                // Si l'ennemi en collision ne peut pas se d�placer, emp�che ce mouvement
                if (!other.GetComponent<EnemyScript>().canMove)
                {
                    gameObject.GetComponent<EnemyScript>().canMove = false;
                    //animator.SetTrigger("Stop");
                }
            }
        }

        // Si l'objet en collision est un projectile et cet objet est un ennemi
        if (other.CompareTag("projectile") && gameObject.CompareTag("enemy"))
        {
            // R�duit les points de vie de cet objet en fonction des d�g�ts du projectile
            gameObject.GetComponent<BasicUnit>().RecievedDammage(other.gameObject.GetComponent<Projectile>().dammage, gameObject);
            // D�truit le projectile
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si l'objet en collision est un ennemi, permet � cet ennemi de se d�placer � nouveau
        if (other.gameObject.CompareTag("enemy"))
        {
            gameObject.GetComponent<EnemyScript>().canMove = true;
        }
    }

    public void Attack(GameObject target)
    {
        // Lance une coroutine pour effectuer l'attaque
        StartCoroutine(AttackCor(target));
    }

    // Arr�te l'attaque
    public void StopAttack()
    {
        // Arr�te toutes les coroutines en cours (y compris l'attaque)
        StopAllCoroutines();
    }

    // Coroutine pour g�rer l'attaque
    IEnumerator AttackCor(GameObject target)
    {
        while (true)
        {
            if (this.target != null && target != null)
            {
                target.GetComponent<BasicUnit>().RecievedDammage(attackValue, gameObject);
            }
            yield return new WaitForSeconds(attackRate); // Attends le prochain cycle d'attaque
        }
    }
}
