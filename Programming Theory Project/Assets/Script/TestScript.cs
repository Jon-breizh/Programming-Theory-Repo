using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Click effectué");
    }

    private void OnMouseOver()
    {
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f ,1.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject findIt = GameObject.Find("Panel01");
        findIt.SetActive(false);
        Debug.Log(findIt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
