using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    private static float mDist;
    private static Transform mHand;
    public bool test, debug;

    private static GameObject controller;// = GameObject.Find("Controller");
    private static SerialController serialExo;// = controller.GetComponent<SerialController>();
    private static openEMSstim serialEMS;

    private string serialData, serialDataPrev;
    private bool emsOn;

    // Use this for initialization
    void Start () {

        mDist = 0;
        test = false;

        controller = GameObject.Find("Controller");
        serialExo = controller.GetComponent<SerialController>();
        serialEMS = controller.GetComponent<openEMSstim>();
        // Detach servo
        //serialExo.dataOut = "300";]
        emsOn = false;
        debug = false;

    }
	
	// Update is called once per frame
	void Update () {

        // Get distance from markers
        mDist = MotiveDirect.markerDistance;

        mHand = GameObject.Find("Hand").transform;

        //Debug.Log(mDist);

        //if (mDist < 0.06 && mDist != 0) // TODO: add distance hand-bottle
        if(test)
        {
            transform.parent = mHand;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //serialExo.dataOut = "120";
            //var bytes = System.Text.Encoding.UTF8.GetBytes("1");
            //serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("1"));
            //serialEMS.dataOut = "1";
            //serialData = "1";

            if (!emsOn)
            {
                serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("1"));
                emsOn = true;
                Debug.Log("Turning EMS On");
            }
        }
        else
        {
            transform.parent = null;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //serialExo.dataOut = "50";

            //serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("0"));
            //serialEMS.dataOut = "0";
            //serialData = "1";
            //Debug.Log("here");

            if (emsOn)
            {
                serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("1"));
                emsOn = false;
                Debug.Log("Turning EMS Off");
            }
        }
        if (debug)
        {
            Debug.Log(serialEMS.readMessage());
        }
       
	}
}
