using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroPosition : MonoBehaviour {

    public bool zero;
    private static Vector3 zeroVector;

	// Use this for initialization
	void Start () {

        zero = false;

	}
	
	// Update is called once per frame
	void Update () {
		
        

        if (zero)
        {
            this.transform.position = this.transform.position - zeroVector;
        }
        else
        {
            zeroVector = new Vector3(0.119f, 0.7739f, -0.453f) - this.transform.position;
        }
	}
}
