using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class BubbleFactory : MonoBehaviour
{
    // Start is called before the first frame update
    static int totalFactories = 0;
    public int key;
    GameObject template;
    void Start()
    {
        this.key = ++totalFactories;
        FactoryListener.instance.Subscribe(this.key, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTemplate(GameObject template){
        if(this.template != null){
            Destroy(this.template);
        }
        GameObject ts = Instantiate(template, transform.position, Quaternion.identity);
        ts.SetActive(false);
        ts.name = "Template";
        this.template = ts;
    }

    public async void CreateBubble(int delay=0){
        await UniTask.Delay(delay);
        GameObject ts = Instantiate(template, transform.position, Quaternion.identity);
        ts.name = "Bubble";
        ts.SetActive(true);
    }
}
