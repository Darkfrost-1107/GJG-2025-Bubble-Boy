using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploteDoor : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject dr;
    void Start()
    {
        dr = GameObject.FindWithTag("Door");
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bubble"){
            Destroy(dr);
            Destroy(gameObject);
        }
    }
}
