using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animalDetector : MonoBehaviour
{

    public Transform rayCastPos;
    RaycastHit myRay;
    public GameObject crossHair;
    public GameObject pickButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(rayCastPos.position, rayCastPos.TransformDirection(Vector3.forward), out myRay, 200f))
        {
            Debug.DrawRay(rayCastPos.position, rayCastPos.TransformDirection(Vector3.forward) * myRay.distance, Color.red);
            if(myRay.collider.gameObject.tag == "Flesh")
            {
                if (myRay.collider.gameObject.transform.root.GetComponent<CharacterDamage>().hitPoints <=0)
                {
                    //  animalName.text = "[" + myRay.collider.gameObject.transform.root.name + "]";
                    crossHair.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    // animalName.text = "";
                    crossHair.GetComponent<Image>().color = Color.red;
                }
            }

            if (myRay.collider.gameObject.tag == "Usable")
            {
                pickButton.SetActive(true);
            }
            else
            {
                pickButton.SetActive(false);
            }

            }
        else
        {
            Debug.DrawRay(rayCastPos.position, rayCastPos.TransformDirection(Vector3.forward) * 100, Color.white);
           // animalName.text = "";
            //Debug.Log("Did not Hit");
        }
    }
}
