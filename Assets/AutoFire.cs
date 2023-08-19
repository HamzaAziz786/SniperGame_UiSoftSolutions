using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFire : MonoBehaviour
{
    public Transform rayCastPos;
    public static bool isAutoFire=false;
    RaycastHit myRay;
    float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    
    if(isAutoFire)
    {
        if (Physics.Raycast(rayCastPos.position, rayCastPos.TransformDirection(Vector3.forward), out myRay, 200f))
        {    
           
           if(myRay.collider.transform.root.tag=="Flesh") // For Getting RFPS AI Health
           {
               health = myRay.collider.gameObject.transform.root.GetComponent<CharacterDamage>().hitPoints;
               if(health>0)
               InputControl.instance.fireHold=true;
               else
               InputControl.instance.fireHold=false;
           }
           else
           InputControl.instance.fireHold = false; 
        }
    }
    }
}
