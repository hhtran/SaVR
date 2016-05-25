using UnityEngine;
using System.Collections;

namespace AssemblyCSharp {

public class Store : MonoBehaviour {

	public float points = 0f;
	public GameObject pointsPile;
	public Category pointsScript;
	protected string prefabFolder = "Prefabs/";

	// Use this for initialization
	void Start () {
	
	}

	void buy(string id){
		switch (id) {
			case "Car":
				Instantiate (Resources.Load (prefabFolder + "Car"), new Vector3 (0f, 1f, 0f), Quaternion.identity);
				pointsScript.removeMoney (1.0f);
			break;
		case "Jet":
			Instantiate (Resources.Load (prefabFolder + "AircraftJet"), new Vector3 (0f, 1f, 0f), Quaternion.identity);
			pointsScript.removeMoney (1.0f);

			break;
		default:
			break;	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)){ // Buy jet
			buy("Jet");
		} else if (Input.GetKeyDown(KeyCode.W)){
			buy("Car");
		}
	}
}

}