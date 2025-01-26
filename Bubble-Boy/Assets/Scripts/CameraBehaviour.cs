using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraBehaviour instance;
    public float CHUNK_BIT_SIZE = 10;
    public float SCROLL_SPEED = 10;
    Rigidbody2D rb;
    Transform target;
    Transform bg;
    Transform fg;
    void Start()
    {
        Debug.Log("Ancho de Pantalla :" + Screen.width);
        Debug.Log("Alto de Pantalla :" + Screen.height);
        rb = GetComponent<Rigidbody2D>();
       

        if (instance == null)
        {
            instance = this;
            bg = GameObject.FindWithTag("Background").transform;
            fg = GameObject.FindWithTag("Frontground").transform; 
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            return;
        }

        Vector3 delta = target.position - transform.position;

        if(delta.x > Screen.width * (2/3) / CHUNK_BIT_SIZE){
            rb.AddForce(Vector3.right * SCROLL_SPEED * delta.x * delta.x);
        }

        if(delta.x < Screen.width * (-2/3) / CHUNK_BIT_SIZE){
            rb.AddForce(Vector2.left * SCROLL_SPEED * delta.x * delta.x); 
        }

        if(delta.y > Screen.height * (2/3) / CHUNK_BIT_SIZE){
            rb.AddForce(Vector2.up * SCROLL_SPEED * delta.y * delta.y);
        }

        if(delta.y < Screen.height * (-2/3) / CHUNK_BIT_SIZE){
            rb.AddForce(Vector2.down * SCROLL_SPEED * delta.y * delta.y);	
        }

        bg.position = Vector3.right * transform.position.x * 0.7f + Vector3.forward * 16f;
        fg.position = Vector3.right * transform.position.x * -0.3f + Vector3.forward * -16f;;
    }
}
