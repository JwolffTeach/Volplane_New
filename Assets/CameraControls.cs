using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    [SerializeField] bool freezeCameraRotation;
    Quaternion origRotation;

	// Use this for initialization
	void Start () {
        origRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (freezeCameraRotation) {
            transform.rotation = origRotation;
        }
	}
}
