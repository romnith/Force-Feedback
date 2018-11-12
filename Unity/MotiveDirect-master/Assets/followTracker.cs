using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTracker : MonoBehaviour {

    public bool calibrate;
    public string trackedObj;
    private static Vector3 mTrackedObj, calibration, initialPos;

    // Use this for initialization
    void Start () {

        initialPos = new Vector3(0.119f, 0.7739f, -0.453f);

        calibrate = false;
        //mTrackedObj = GameObject.Find(trackedObj).transform;
        mTrackedObj = GameObject.Find(trackedObj).transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        mTrackedObj = GameObject.Find(trackedObj).transform.position;
        this.transform.position = mTrackedObj;

        //Debug.Log(mTrackedObj.position);

        if (calibrate)
        {
            //transform.parent = mTrackedObj - calibration;
            //this.transform.position = new Vector3(mTrackedObj.position.x - calibration.x,
            //                                        mTrackedObj.position.y - calibration.y,
            //                                        mTrackedObj.position.z - calibration.z);
            this.transform.position = mTrackedObj - calibration ;
        }
        else
        {
            calibration = mTrackedObj - initialPos;
        }
	}
}
