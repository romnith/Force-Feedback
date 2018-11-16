using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    private static float mDist;
    private static Transform mHand;
    public bool test;

    private static GameObject controller;// = GameObject.Find("Controller");
    private static SerialController serialExo;// = controller.GetComponent<SerialController>();

    // Use this for initialization
    void Start () {

        mDist = 0;
        test = false;

        controller = GameObject.Find("Controller");
        serialExo = controller.GetComponent<SerialController>();
        // Detach servo
        serialExo.dataOut = "300";
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
            serialExo.dataOut = "120";
        }
        else
        {
            transform.parent = null;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            serialExo.dataOut = "50";
        }
	}
}
