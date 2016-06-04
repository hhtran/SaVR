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
		Quaternion r = transform.rotation;

		if (Input.GetKey (KeyCode.Comma)) {
			transform.rotation = new Quaternion(r.x, r.y - 0.01f, r.z, r.w);
		} else if (Input.GetKey (KeyCode.Period)) {
			transform.rotation = new Quaternion(r.x, r.y + 0.01f, r.z, r.w);
		}

		if (Input.GetKey (KeyCode.K)) {
			transform.position += new Vector3 (0f, 0.1f, 0f);
		} else if (Input.GetKey (KeyCode.L)) {
			transform.position += new Vector3 (0f, -0.1f, 0f);

		}
		if (moveHorizontal != 0.0) {
			transform.position = transform.position + transform.right * moveHorizontal;
		}
		if (moveVertical != 0.0) {
			transform.position = transform.position + transform.forward * moveVertical;
		}

	}
}
