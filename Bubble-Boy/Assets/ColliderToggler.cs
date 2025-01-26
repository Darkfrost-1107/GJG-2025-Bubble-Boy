using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class ColliderToggler : MonoBehaviour
{
    public int delay = 2000;
    Collider2D col;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponents<Collider2D>()[0];
        toogleCollider(col, false);
    }

    public async void toogleCollider(Collider2D collider, bool state){
        await UniTask.Delay(delay);
        collider.enabled = state;
        anim.SetBool("active", state);
        this.toogleCollider(collider, !state);
    }
}
