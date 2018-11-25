using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    private static float mDist, distBottleHand;
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
        serialExo.dataOut = "300";
        emsOn = false;

    }
	
	// Update is called once per frame
	void Update () {

        // Get distance from markers
        mDist = MotiveDirect.markerDistance;

        mHand = GameObject.Find("Hand").transform;

        distBottleHand = Vector3.Distance(this.transform.position, mHand.transform.position);

        //if (mDist < 0.06 && mDist != 0 && distBottleHand <= 0.12) // TODO: add distance hand-bottle
        if (test)
        {
            transform.parent = mHand;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            serialExo.dataOut = "110";

            if (!emsOn)
            {
                serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("1q")); // Activate Ch1 and send intensity to max
                emsOn = true;
                //Debug.Log("Turning EMS On");
            }
        }
        else
        {
            transform.parent = null;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            serialExo.dataOut = "30";
            Debug.Log(distBottleHand);
            if (emsOn)
            {
                serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("1")); // Desactivate EMS Ch1
                emsOn = false;
                //Debug.Log("Turning EMS Off");
            }
        }
       
	}
}
