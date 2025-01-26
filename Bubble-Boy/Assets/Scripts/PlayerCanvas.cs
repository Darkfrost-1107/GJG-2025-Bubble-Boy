using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    static public PlayerCanvas instance;
    public Slider powerSlider;
    public Slider speedSlider;
    void Start()
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

    public void SetPower(float max, float current){
        float value = Mathf.Clamp(current * 100 / max, 0, 100);
        powerSlider.value = value;
    }

    public void SetSpeed(float max, float current){
        float value = Mathf.Clamp(current * 100 / max, 0, 100);
        speedSlider.value = value;
    }
}
