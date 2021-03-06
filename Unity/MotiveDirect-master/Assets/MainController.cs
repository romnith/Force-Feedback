﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public float mDist, mDistLast, distBottleHand, mDistDiff;
    private static Transform mHand;
    public bool test, debug;

    private static GameObject controller;// = GameObject.Find("Controller");
    private static SerialController serialExo;// = controller.GetComponent<SerialController>();
    private static openEMSstim serialEMS;

    private string serialData, serialDataPrev;
    private bool emsOn, grabOn, mDistErr, markerErr;

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
        grabOn = false;

    }
	
	// Update is called once per frame
	void Update () {

        // Get distance from markers
        mDist = MotiveDirect.markerDistance;
        markerErr = MotiveDirect.lostMarker;
        mDistDiff = Mathf.Abs(mDist - mDistLast);
        if (Mathf.Abs(mDist - mDistLast) > 0.01)
        {
            mDistErr = true;
        }
        else
        {
            mDistErr = false;
        }

        mHand = GameObject.Find("Hand").transform;


        distBottleHand = Vector3.Distance(this.transform.position, mHand.transform.position);

        if (debug)
        {
            Debug.Log(mDist);
        }

        if (mDist < 0.067 && mDist != 0 && distBottleHand <= 0.15) // TODO: add distance hand-bottle
                                                                   //if (test)
        {
            transform.parent = mHand;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            serialExo.dataOut = "150";
            grabOn = true;

            // Avoids sending 2nd message which would desactivate EMS
            if (!emsOn)
            {
                serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("1q")); // Activate Ch1 and send intensity to max
                emsOn = true;
                //Debug.Log("Turning EMS On");
            }
        }
        else if(mDist > 0.1 && mDist < 0.15)
        {
            transform.parent = null;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            serialExo.dataOut = "80";
            grabOn = false;

            // Avoids sending 2nd message which would activate EMS
            if (emsOn)
            {
                serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("1")); // Desactivate EMS Ch1
                emsOn = false;
                //Debug.Log("Turning EMS Off");
            }
        }
        else
        {
            if (grabOn)
            {
                transform.parent = mHand;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                serialExo.dataOut = "150";
                grabOn = true;

                // Avoids sending 2nd message which would desactivate EMS
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
                serialExo.dataOut = "80";
                grabOn = false;

                // Avoids sending 2nd message which would activate EMS
                if (emsOn)
                {
                    serialEMS.sendMessage(System.Text.Encoding.UTF8.GetBytes("1")); // Desactivate EMS Ch1
                    emsOn = false;
                    //Debug.Log("Turning EMS Off");
                }
            }
        }

        mDistLast = mDist;
       
	}
}
