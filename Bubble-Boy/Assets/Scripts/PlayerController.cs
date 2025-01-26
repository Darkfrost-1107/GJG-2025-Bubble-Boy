using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    BubbleBehaviour bubbleBehaviour;
    int checkpointKey = 0;
    void Start()
    {
        bubbleBehaviour = GetComponent<BubbleBehaviour>();
        CameraBehaviour.instance.SetTarget(transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(0, 0);
        if(Input.GetKey(KeyCode.A)){
            direction += Vector2.left;
        } 
        if(Input.GetKey(KeyCode.D)){
            direction += Vector2.right;	
        }
        if(Input.GetKey(KeyCode.W)){
            direction += Vector2.up;
        }
        if(Input.GetKey(KeyCode.S)){
            direction += Vector2.down;
        }
        if(direction.magnitude > 0){
            bubbleBehaviour.move(direction);
        }
        PlayerCanvas.instance.SetPower(bubbleBehaviour.MAX_FORCE_ALLOWED, bubbleBehaviour.getPower());
        PlayerCanvas.instance.SetSpeed(bubbleBehaviour.MAX_SPEED_ALLOWED, bubbleBehaviour.getSpeed());

    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "CheckPoint"){
            BubbleFactory bf = collision.GetComponent<BubbleFactory>();
            bf.SetTemplate(gameObject);
            this.checkpointKey = bf.key;
            Debug.Log("CheckPointKey: " + checkpointKey);
            return;
        }
    }

  
    void OnDestroy()
    {
        if(gameObject.activeSelf){
            return;
        }
        FactoryListener.instance.CallFactory(checkpointKey, 1000);
    }
}
