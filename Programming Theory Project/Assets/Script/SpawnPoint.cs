using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float rotationSpeed = 30.0f;
    public bool IsFree = true;
    // ENCAPSULATION - IsFree is encapsulated as a private variable with a public getter.
    private void OnMouseDown()
    {
        LevelManager.instance.BuyUnit(this.gameObject);
        // Demonstrates INHERITANCE, as it uses BuyUnit from the parent class.
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        // Demonstrates ABSTRACTION - Update abstracts the rotation details.
    }
}
