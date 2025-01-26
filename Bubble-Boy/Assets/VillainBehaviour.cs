using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class VillainBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    Rigidbody2D rb;
    GameObject target;

    int charge = 0;
    public int inFloor = 0;
    void Start()
    {  
       anim = GetComponent<Animator>(); 
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void Shoot(){
        anim.SetBool("isCharging", false);
        // shot fireballs
        charge = 0;
        await UniTask.Delay(1000);
        this.WalkToTarget();
    }

    async void Charge(){
        anim.SetBool("isCharging", true);
        charge += 1;
        await UniTask.Delay(1000);
        float number = Random.Range(0f,5f);
        if(charge <= 5 && number > 3){
            this.Charge();
        } else {
            this.Shoot();
        }
    }

    async void Walk(Vector2 direction){
        rb.velocity = direction;
        await UniTask.Delay(1500);
        float number = Random.Range(0f,3f);
        if(number > 2){
            this.Charge();
        } else {
            this.WalkToTarget();
        }
    }

    public async void WalkToTarget(){
        if(this.inFloor <= 0){
            await UniTask.Delay(500);
            this.WalkToTarget();
            return;
        }
        if(target == null){
            return;
        }
        Vector2 direction = target.transform.position - transform.position;
        this.Walk(direction);
    }

    async Task Wake(){
        anim.SetBool("isAwake", true);
        await UniTask.Delay(2000);
        this.WalkToTarget();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bubble"){
            if(target == null){
                this.Wake();
                target = other.gameObject;
            }
        }
    }

    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter2D(Collision other)
    {
        this.inFloor += 1;
    }


    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        this.inFloor -= 1;
    }
}
