using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatUnit : BasicUnit
{
    [SerializeField] private int attackValue; // Attack value
    [SerializeField] private float attackRate; // Attack rate (time between each attack)
    public GameObject blocObject, target; // Block and attack objects

    //public Animator animator;

    // POLYMORPHISM - Start method can be overridden in child classes.
    private void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
    }

    // Called when this object's collider triggers another collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object in collision is a wall, a defense unit, or an enemy
        if ((other.CompareTag("Player") || other.CompareTag("defenceUnit") || other.CompareTag("enemy")) && !other.CompareTag("projectile"))
        {
            // Check if the object in collision has a different tag than this object
            if (gameObject.tag != other.gameObject.tag)
            {
                // Perform an attack on the object in collision
                Attack(other.gameObject);
                target = other.gameObject;
                //animator.SetTrigger("Attack");

                // If this object is an enemy, activate "in combat" mode and remember the blocking object
                if (gameObject.CompareTag("enemy"))
                {
                    gameObject.GetComponent<EnemyScript>().inCombat = true;
                    blocObject = other.gameObject;
                    //animator.SetTrigger("Attack");
                }
            }

            // If the object in collision is an enemy and this object is also an enemy
            if (other.CompareTag("enemy") && gameObject.CompareTag("enemy"))
            {
                // If no blocking object is defined, remember the object in collision as a blocking object
                if (blocObject == null)
                {
                    blocObject = other.gameObject;
                    //animator.SetTrigger("Stop");
                }

                // If the colliding enemy is already in combat, prevent this movement
                if (other.GetComponent<EnemyScript>().inCombat)
                {
                    gameObject.GetComponent<EnemyScript>().canMove = false;
                    //animator.SetTrigger("Stop");
                }

                // If the colliding enemy cannot move, prevent this movement
                if (!other.GetComponent<EnemyScript>().canMove)
                {
                    gameObject.GetComponent<EnemyScript>().canMove = false;
                    //animator.SetTrigger("Stop");
                }
            }
        }

        // If the object in collision is a projectile and this object is an enemy
        if (other.CompareTag("projectile") && gameObject.CompareTag("enemy"))
        {
            // Reduce the hit points of this object based on the projectile's damage
            gameObject.GetComponent<BasicUnit>().ReceivedDamage(other.gameObject.GetComponent<Projectile>().damage, gameObject);
            // Destroy the projectile
            Destroy(other.gameObject);
        }
    }

    // Called when another collider exits the trigger collider of this object
    private void OnTriggerExit(Collider other)
    {
        // If the colliding object is an enemy, allow that enemy to move again
        if (other.gameObject.CompareTag("enemy"))
        {
            gameObject.GetComponent<EnemyScript>().canMove = true;
        }
    }

    // ENCAPSULATION - Method to perform an attack
    public void Attack(GameObject target)
    {
        // Start a coroutine to perform the attack
        StartCoroutine(AttackCor(target));
    }

    // Stop the attack
    public void StopAttack()
    {
        // Stop all running coroutines (including the attack)
        StopAllCoroutines();
    }

    // Coroutine to handle the attack
    // ABSTRACTION - This coroutine abstracts the details of attack execution.
    IEnumerator AttackCor(GameObject target)
    {
        while (true)
        {
            if (this.target != null && target != null)
            {
                target.GetComponent<BasicUnit>().ReceivedDamage(attackValue, gameObject);
            }
            yield return new WaitForSeconds(attackRate); // Wait for the next attack cycle
        }
    }
}
