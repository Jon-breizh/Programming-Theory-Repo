using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// INHERITANCE : this class inherits from the BasicUnit class
public class CombatUnit : BasicUnit
{
    // ENCAPSULATION - variable declaration
    [SerializeField] private int attackValue; // Attack value
    [SerializeField] private float attackRate; // Attack rate (time between each attack)
    private GameObject target; //attack objects
    public GameObject blocObject { get; private set; } // Block and attack objects
    

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
                    gameObject.GetComponent<Animator>().SetBool("IsRunning", false);
                    gameObject.GetComponent<Animator>().SetTrigger("Attack");
                    blocObject = other.gameObject;
                }
            }

            // If the object in collision is an enemy and this object is also an enemy
            if (other.CompareTag("enemy") && gameObject.CompareTag("enemy"))
            {
                // If no blocking object is defined, remember the object in collision as a blocking object
                if (blocObject == null)
                {
                    blocObject = other.gameObject;
                }

                // If the colliding enemy is already in combat, prevent this movement
                if (other.GetComponent<EnemyScript>().inCombat)
                {
                    gameObject.GetComponent<EnemyScript>().canMove = false;
                    gameObject.GetComponent<Animator>().SetBool("IsRunning", false);
                    gameObject.GetComponent<Animator>().SetTrigger("Stop");
                }

                // If the colliding enemy cannot move, prevent this movement
                if (!other.GetComponent<EnemyScript>().canMove)
                {
                    gameObject.GetComponent<EnemyScript>().canMove = false;
                    gameObject.GetComponent<Animator>().SetBool("IsRunning", false);
                    gameObject.GetComponent<Animator>().SetTrigger("Stop");
                }
            }
        }

        // If the object in collision is a projectile and this object is an enemy
        if (other.CompareTag("projectile") && gameObject.CompareTag("enemy"))
        {
            // Reduce the hit points of this object based on the projectile's damage
            gameObject.GetComponent<BasicUnit>().ReceivedDamage(other.gameObject.GetComponent<Projectile>().damage, gameObject);
            //Play the hit sound
            gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<EnemyScript>().hitSound);
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
            gameObject.GetComponent<Animator>().SetBool("IsRunning", true);
        }
    }

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
    // ABSTRACTION : This coroutine abstracts the details of attack execution.
    IEnumerator AttackCor(GameObject target)
    {
        while (true)
        {
            if (this.target != null && target != null)
            {
                target.GetComponent<BasicUnit>().ReceivedDamage(attackValue, gameObject);
                if (gameObject.CompareTag("enemy"))
                {
                    gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<EnemyScript>().attackSound);
                }
            }
            yield return new WaitForSeconds(attackRate); // Wait for the next attack cycle
        }
    }
}
