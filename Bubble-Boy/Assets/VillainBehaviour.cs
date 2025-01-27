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
    public bool isAwake = false;
    public bool isAlive = true;
    public int MAX_HEALTH = 50;
    int health;

    public GameObject bullet;

    void Start()
    {  
        this.health = MAX_HEALTH;
       anim = GetComponent<Animator>(); 
       rb = GetComponent<Rigidbody2D>();
       this.anim.SetInteger("Health", this.health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void Shoot(){
        anim.SetBool("isCharging", false);
        // shot fireballs
        this.DisableShot();
        this.DoShot();
        await UniTask.Delay(1500);
        this.WalkToTarget();
    }

    void PrepareShot(){
        GameObject ch = transform.GetChild(0).gameObject;
        ch.SetActive(true);
    }

    void DisableShot(){
        GameObject ch = transform.GetChild(0).gameObject;
        ch.SetActive(false);
    }

    void DoShot(){
        switch(Random.Range(0, 3)){
            case 0:
                this.DoSimpleShot();
                break;
            case 1:
                this.DoMixedShot();
                break;
            case 2:
                this.DoScaleShot();
                break;
        }
    }

    async void DoSimpleShot(){
        Vector3 direction = (target.transform.position - transform.position) * 3;
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().velocity = direction;
        charge -= 1;
        await UniTask.Delay(200);
        if(charge > 0){
            charge += 1;
            this.DoSimpleShot();
            await UniTask.Delay(100);
            this.DoSimpleShot();
        }
    }

    void DoMixedShot(){
        Vector3 direction = (target.transform.position - transform.position) * 3;
        for(; this.charge > 0; this.charge--){
            GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
            GameObject b2 = Instantiate(bullet, transform.position, Quaternion.identity);
            b1.GetComponent<Rigidbody2D>().velocity = direction + new Vector3(4,0,0) * this.charge;
            b2.GetComponent<Rigidbody2D>().velocity = direction + new Vector3(-4,0,0) * this.charge;
        }
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().velocity = direction;

    }

    async void DoScaleShot(){
        Vector3 direction = (target.transform.position - transform.position) * 3;
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().velocity = direction;
        b.transform.localScale = Vector3.one * this.charge;
        await UniTask.Delay(2000);
        for(; this.charge > 0; this.charge--){
            GameObject b1 = Instantiate(bullet, b.transform.position, Quaternion.identity);
            GameObject b2 = Instantiate(bullet, b.transform.position, Quaternion.identity);
            b1.GetComponent<Rigidbody2D>().velocity = new Vector3(4 * this.charge, 8, 0);
            b2.GetComponent<Rigidbody2D>().velocity = new Vector3(-4 * this.charge, 8, 0); 
        }
        GameObject bx = Instantiate(bullet, b.transform.position, Quaternion.identity);
        bx.GetComponent<Rigidbody2D>().velocity = new Vector3(0,8,0);
        Destroy(b);
    }

    async void Charge(){
        anim.SetBool("isCharging", true);
        charge += Random.Range(1,3);
        this.PrepareShot();
        await UniTask.Delay(1000);
        float number = Random.Range(0f,5f);
        if(charge <= 5 && number > 2){
            this.Charge();
        } else {
            this.Shoot();
        }
    }

    async void Walk(Vector2 direction){
        rb.velocity = direction;
        await UniTask.Delay(500);
        float number = Random.Range(0f,5f);
        if(number > 2){
            this.Charge();
        } else {
            this.WalkToTarget();
        }
    }

    public async void WalkToTarget(){
        if(!this.isAlive){
            return;
        }
        if(this.inFloor <= 0){
            await UniTask.Delay(500);
            this.WalkToTarget();
            return;
        }
        if(target == null){
            this.Sleep();
            return;
        }
        float direction = Mathf.Clamp(target.transform.position.x - transform.position.x,-10,10);
        float jump = Mathf.Clamp(target.transform.position.y - transform.position.y,1,5);
        this.Walk(new Vector2(direction, jump));
    }

    async void Wake(){
        this.isAwake = true;
        anim.SetBool("isAwake", this.isAwake);
        await UniTask.Delay(2000);
        this.WalkToTarget();
        await UniTask.Delay(20000);
        this.Damage();
    }

    async void Sleep(){
        this.isAwake = false;
        anim.SetBool("isAwake", this.isAwake);
        this.health = MAX_HEALTH;
        this.anim.SetInteger("Health", this.health);
        await UniTask.Delay(2000);
    }

    async void Damage(){
        if(this.health <= 0 || !this.isAwake){
            return;
        }
        this.anim.SetBool("gotDamaged", true);
        this.health -= 10;
        this.anim.SetInteger("Health", this.health);
        this.anim.SetBool("isCharging", false);
        this.charge = 0;
        this.DisableShot();
        this.anim.SetBool("isDashing", false);
        await UniTask.Delay(200);
        this.anim.SetBool("gotDamaged", false);
        if(this.health <= 0){
            this.Die();
            return;
        }
        await UniTask.Delay(20000);
        this.Damage();
    }

    async void Die(){
        this.isAlive = false;
        await UniTask.Delay(1000);
        PlayerCanvas.instance.EndGame();
        Destroy(this.gameObject);
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
    void OnCollisionEnter2D(Collision2D other)
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
