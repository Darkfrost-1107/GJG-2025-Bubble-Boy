using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float speed = 5;
    public float MAX_FORCE_ALLOWED = 3f;
    public float MAX_SPEED_ALLOWED = 0.1f;
    public int MAX_HEAT_ALLOWED = 5;


    public int heat = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pop(){
        Debug.Log("Bubble popped");
        Destroy(gameObject);
    }

    public void Heat(){
        this.heat += 1;
        if(this.heat >= MAX_HEAT_ALLOWED){
            Pop();
        }
    }

    public void Freeze(){
        this.heat -= 1;
    }
    public void move(Vector2 direction){
        rb.AddForce(direction * speed * Time.deltaTime);
        // Debug.Log("Fuerza en la BB :" + rb.totalForce.magnitude);
        // Debug.Log("Velocidad en la BB :" + rb.velocity.magnitude);

        if(rb.totalForce.magnitude > MAX_FORCE_ALLOWED){
            Pop();
        }
    }

    public float getPower(){
        return rb.totalForce.magnitude;
    }

    public float getSpeed(){
        return rb.velocity.magnitude;
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.isTrigger){
            Debug.Log("Triggered");
            return;
        }
        Debug.Log("Collided");
        if(rb.velocity.magnitude > MAX_SPEED_ALLOWED){
            Pop();
        }
    }
}
