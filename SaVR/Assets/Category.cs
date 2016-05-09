using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Category : MonoBehaviour
	{
		private String prefabFolder = "Prefabs/";
		private float moneyStored;
		private float goalAmount;
		private GameObject categoryRootObj; //The empty game object that serves as the root to hold all categories
		private GameObject categoryParentObj; //The root game object to hold game objects related to this category
		private Dictionary<int, List<GameObject>> moneySubObjects = new Dictionary<int, List<GameObject>>(); //A dictionary mapping from a place value (e.g. 100) to an array of the child money GameObjects that are of that placevalue

		public Category (String categoryName, float goalAmount, float startingAmount, GameObject categoriesRootObj,  Vector3 position, Quaternion rotation)
		{
			moneyStored = startingAmount;
			this.goalAmount = goalAmount;

			createWorldObjects (categoryName, startingAmount, categoriesRootObj, position, rotation);
		}

		private void createWorldObjects(String categoryName, float startingAmount, GameObject categoriesRootObj, Vector3 position, Quaternion rotation) {
			categoryParentObj = new GameObject ();
			categoryParentObj.transform.position = position;
			categoryParentObj.transform.rotation = rotation;
			categoryParentObj.transform.name = categoryName;
			categoryParentObj.transform.SetParent (categoriesRootObj.transform);

			GameObject barrierPrefab = Resources.Load (prefabFolder + "Barrier", typeof(GameObject)) as GameObject;
			GameObject barrierInstance = Instantiate (barrierPrefab);
			barrierInstance.transform.position = categoryParentObj.transform.position;
			barrierInstance.transform.rotation = barrierInstance.transform.rotation;

			moneySubObjects = convertAmountToPlaceValues (startingAmount, categoryParentObj);
		}
			
		//	Takes in a float amount and returns a dictionary of lists,
		//	where the keys are integers (1, 10, 100, etc) and the lists are 
		//	lists of GameObjects of the in-world objects representing a money amount
		private Dictionary<int, List<GameObject>> convertAmountToPlaceValues(float amount, GameObject categoryParentObj){
			int placeValue = 1;
			int amountToConvert = (int) amount;

			Dictionary<int, List<GameObject>> placeValuesMap = new Dictionary<int, List<GameObject>> ();

			while (amountToConvert > 0) {
				int numberInPlaceValue = (int)amountToConvert % ( placeValue * 10 );

				int digitValue = numberInPlaceValue / placeValue;
				amountToConvert -= numberInPlaceValue;

				//Convert to objects for this place value
				List<GameObject> placeValueObjectList = createPlaceValueObjects(placeValue, digitValue, categoryParentObj);
				placeValuesMap.Add (placeValue, placeValueObjectList);

				Debug.Log ("Amount to convert: " + amountToConvert.ToString());
				//Increment
				placeValue *= 10;
			}
			return placeValuesMap;
		}

		// Returns a list of in-world game objects for a given number of objects, under the category parent object
		private List<GameObject> createPlaceValueObjects(int placeValue, float numberOfObjs, GameObject categoryParentObject){
			Debug.Log ("Creating place value objects: " + placeValue.ToString() + " place value: " + numberOfObjs.ToString());
			List<GameObject> placeValueObjectList = new List<GameObject> ();
			GameObject prefabForPlaceValue = getPrefabForPlaceValue (placeValue);

			for (int i = 0; i < numberOfObjs; i++) {
				Vector3 moneyObjPosition = categoryParentObject.transform.position;
				moneyObjPosition.y += i + 1;
				GameObject moneyPrefabInstance = Instantiate (prefabForPlaceValue, moneyObjPosition, Quaternion.identity) as GameObject;
				moneyPrefabInstance.transform.SetParent (categoryParentObj.transform);

				placeValueObjectList.Add (moneyPrefabInstance);
			}
			return placeValueObjectList;
		}

		// Retrieves the prefab for the money object. Note that the prefab folder must be inside the resources folder
		private GameObject getPrefabForPlaceValue(int placeValue) {
			if (placeValue != 1 && placeValue % 10 > 0)
				return new GameObject ();
			switch (placeValue) {
			case 1:
				return Resources.Load (prefabFolder + "Gold Bar", typeof(GameObject)) as GameObject;
			default:
				if (placeValue >= 1000)
					return new GameObject ();
				return Resources.Load (prefabFolder + "GoldBar " + placeValue.ToString(), typeof(GameObject)) as GameObject;
			}
		}


		public float getMoneyStored () {
			return moneyStored;
		}

		public float getGoalAmount () {
			return goalAmount;
		}

		public void addMoney(float amount) {
			Dictionary<int, List<GameObject>> newMoneyObjectsMap = convertAmountToPlaceValues (amount, categoryParentObj);
			foreach (int placeValue in newMoneyObjectsMap.Keys) {
				if (moneySubObjects.ContainsKey (placeValue)) {
					moneySubObjects [placeValue].AddRange (newMoneyObjectsMap [placeValue]);
				} else {
					moneySubObjects.Add (placeValue, newMoneyObjectsMap[placeValue]);
				}
			}

			moneyStored += amount;
		}

		public void removeMoney(float amount) {
			moneyStored -= amount;
		}

		public void changeGoalAmount (float newGoalAmount) {
			goalAmount = newGoalAmount;
		}

	}
}

