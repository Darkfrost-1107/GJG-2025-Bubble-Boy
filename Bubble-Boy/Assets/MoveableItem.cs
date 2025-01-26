using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 start;
    public Vector3 begin;
    public Vector3 end;

    public float time = 2f;

    bool goingToEnd = true;

    void Start()
    {
        transform.position = start;
    }

    // Update is called once per frame
    void Update()
    {
        if(goingToEnd){
            transform.position = Vector3.MoveTowards(transform.position, end, 10 / time * Time.deltaTime);
            if(transform.position == end){
                goingToEnd = false;
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, begin, 10 /time * Time.deltaTime);
            if(transform.position == begin){
                goingToEnd = true;
            }
        }
    }
}
