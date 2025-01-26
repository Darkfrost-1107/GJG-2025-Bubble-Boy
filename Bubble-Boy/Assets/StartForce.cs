using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForce : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 direction;
    Rigidbody2D rb; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
