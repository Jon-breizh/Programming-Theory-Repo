using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceObjet : MonoBehaviour
{
    [SerializeField] private int life = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecievedDammage(int value, GameObject AttackGo)
    {
        life -= value;
        if (life <= 0)
        {
            Destroy(gameObject);
            AttackGo.GetComponent<EnemyScript>().StopAttack();
            AttackGo.GetComponent<EnemyBasic01>().contact = false;
        }
    }
}
