using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float rotationSpeed = 30.0f;
    private void OnMouseDown()
    {
        LevelManager.instance.BuyUnit(this.gameObject);        
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
