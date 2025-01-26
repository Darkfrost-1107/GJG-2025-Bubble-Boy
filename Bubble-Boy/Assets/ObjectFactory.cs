using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class ObjectFactory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject template;
    public int delay;
    void Start()
    {
        this.CreateObject(delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create(){
        if(template == null){
            return;
        }
        Instantiate(template, transform.position, Quaternion.identity);
    }

    public async void CreateObject(int delay){
        await UniTask.Delay(delay);
        this.Create();
        this.CreateObject(delay);
    }
}
