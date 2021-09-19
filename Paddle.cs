using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private Rigidbody rg;
    
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float mouse = new Vector3(Input.mousePosition.x, 0, 50).x;
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mouse, -17, 0));
        
        rg.MovePosition(pos);
    }
}
