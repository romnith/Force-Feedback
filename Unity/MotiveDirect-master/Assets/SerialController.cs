using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

// Servo motor stable values: 29 - 142

public class SerialController : MonoBehaviour {

    //public string comPort = "COM3";
    private static string incomingMsg;
    public bool test;
    public string dataOut, dataLastOut;

    SerialPort stream = new SerialPort("COM17", 9600);


    // Use this for initialization
    void Start () {
        
        stream.ReadTimeout = 50;
        stream.Open();
        //dataOut = "300";
        //dataLastOut = null;
        //WriteToArduino("300");
    }
	
	// Update is called once per frame
	void Update () {

        if (stream.IsOpen)
        {
            try
            {
                incomingMsg = stream.ReadLine();
                // Ignore heartbeat message
                if (incomingMsg != "0")
                {
                    Debug.Log(incomingMsg);
                }

                // Testing values
                if (test)
                {
                    dataOut = "135";
                }
                else
                {
                    dataOut = "50";
                }

                // Avoid sending same value too many times
                if (dataOut != dataLastOut)
                {
                    Debug.Log(dataOut + " VS " + dataLastOut);
                    dataLastOut = dataOut;
                    WriteToArduino(dataLastOut);
                }
            }
            catch (System.Exception)
            {
                // Heartbeat message, does nothing
                WriteToArduino("0");
            }
        }
        
	}

    public void WriteToArduino(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name != null)
    //    {
    //        WriteToArduino("300"); // This works
    //        Debug.Log("Contact was made!");
    //    }
    //    else
    //    {
    //        WriteToArduino("300"); // This value doesn't work
    //        Debug.Log("No contact was made!");
    //    }
    //}
}
