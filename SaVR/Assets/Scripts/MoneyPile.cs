using UnityEngine;
using System.Collections;

public class MoneyPile : MonoBehaviour {
	public GameObject goldBarPrefab;
	public int batchSize = 100;
	public int batchDelay = 10; //In milliseconds
	public int barsToCreate = 100;
	// Use this for initialization
	void Start () {
	
	}

	void createBars(int barsToCreate){
		int barsCreated = 0;
		Debug.Log ("Creating bars");

		while (barsCreated < barsToCreate){
			Vector3 spawnLocation = new Vector3 (transform.position.x, transform.position.y + barsCreated, transform.position.z);
			Instantiate(goldBarPrefab, spawnLocation, Quaternion.identity);
			barsCreated++;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.X)){
			createBars(barsToCreate);
		}
	}
}
