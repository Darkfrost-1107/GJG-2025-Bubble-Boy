using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Bubble"){
            BubbleBehaviour bb = other.gameObject.GetComponent<BubbleBehaviour>();
            bb.Pop();
        }
    }
}
