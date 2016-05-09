using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public GameObject cameraObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (moveHorizontal != 0.0) {
			transform.position = transform.position + new Vector3(moveHorizontal, 0, 0);
		}
		if (moveVertical != 0.0) {
			transform.position = transform.position + new Vector3(0, 0, moveVertical);
		}
	}
}
