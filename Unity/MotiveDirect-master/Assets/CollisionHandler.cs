using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

    SerialController serialController = new SerialController();
    //GameObject cubeObject = GameObject.Find("Cube");


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != null)
        {
            serialController.WriteToArduino("120");
            Debug.Log("Contact was made!");
        }
        else
        {
            serialController.WriteToArduino("30");
            Debug.Log("No contact was made!");
        }
    }
}
