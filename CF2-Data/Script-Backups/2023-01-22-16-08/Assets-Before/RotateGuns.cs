using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateGuns : MonoBehaviour
{   
    bool rotateleft=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateleft)
        transform.Rotate(0,25*Time.deltaTime,0,Space.World);
        else
        transform.Rotate(0,-25*Time.deltaTime,0,Space.World);

        
        
    }

    public void OnDrag()
    {
        float x =Input.GetAxis("Mouse X") * 25 *Mathf.Deg2Rad;
        if(x>0.1f)
        rotateleft=false;
        else if(x<-0.1f)
        rotateleft=true;
        transform.Rotate(Vector3.up,-x);
    }
    

}
