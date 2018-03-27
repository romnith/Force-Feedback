using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;



public class SerialController : MonoBehaviour {
    SerialPort stream = new SerialPort("COM4", 9600);

    // Use this for initialization
    void Start () {
        //Debug.Log("Hi!");
        stream.ReadTimeout = 50;
        stream.Open();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WriteToArduino(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != null)
        {
            WriteToArduino("300"); // This works
            Debug.Log("Contact was made!");
        }
        else
        {
            WriteToArduino("300"); // This value doesn't work
            Debug.Log("No contact was made!");
        }
    }
}
