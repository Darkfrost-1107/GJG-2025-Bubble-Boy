using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryListener : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<int, BubbleFactory> factories = new Dictionary<int, BubbleFactory>();
    static public FactoryListener instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Subscribe(int key, BubbleFactory target){
        this.factories.Add(key, target);
    }

    public void CallFactory(int key, int delay=0){
        if(factories.ContainsKey(key)){
            factories[key].CreateBubble(delay);
        }
    }

    
}
