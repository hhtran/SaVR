using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WorldController : MonoBehaviour {

	public GameObject savingsPilesParent;
	private Category mostRecentSavingsPile;
	private bool atLeastOneSavingsPileExists = false;

	private int numberOfSavingsPiles = 0;

	Category createSavingsPile(){
		atLeastOneSavingsPileExists = true;
		Vector3 position = new Vector3 (numberOfSavingsPiles * 15, 0, 0);
		SavingsPile savingsPile = new SavingsPile ("Savings Pile", 1000.0f, 100.0f, savingsPilesParent, position, Quaternion.identity);
		numberOfSavingsPiles++;
		return savingsPile;
	}

	/* Key mappings for actions
	 * WASD or arrow keys: Move the camera around
	 * C: Add a new category/savings pile
	 * X: Convert the money in the most recently created category into PS4 units
	 * Z: Convert money to Starbucks cups
	 * 0: Add 115 to the most recently created category
	 * 1: Add 1 to the most recently created category
	 * 2: Add 10 to the most recently created category
	 * 3: Add 100 to the most recently created category
	 * 4: Add 1000 to the most recently created category
	 */
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)){
			mostRecentSavingsPile = createSavingsPile();
		}

		if (atLeastOneSavingsPileExists) {
			if (Input.GetKeyDown (KeyCode.Alpha0)) {
				mostRecentSavingsPile.addMoney (115.0f);
			} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
				mostRecentSavingsPile.addMoney (1.0f);
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				mostRecentSavingsPile.addMoney (10.0f);
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				mostRecentSavingsPile.addMoney (100.0f);
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				mostRecentSavingsPile.addMoney (1000.0f);
			} else if (Input.GetKeyDown (KeyCode.X)) {
				mostRecentSavingsPile.convertVisualizationUnit ("PS4");
			} else if (Input.GetKeyDown (KeyCode.Z)) {
				mostRecentSavingsPile.convertVisualizationUnit ("Starbucks");
			}
		}

	}
}
