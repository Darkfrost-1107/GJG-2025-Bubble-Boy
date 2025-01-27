using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class LifeTime : MonoBehaviour
{
    // Start is called before the first frame update
    public int lifeTime = 3500;
    
    void Start()
    {
        if(gameObject.activeSelf){
            DestroyAfter(lifeTime);
        }
    }

    async void DestroyAfter(int time){
        await UniTask.Delay(time);
        Destroy(this.gameObject);
    }
}
