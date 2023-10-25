using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnMouseDown()
    {
        LevelManager.instance.BuyUnit(this.gameObject);        
    }
}
