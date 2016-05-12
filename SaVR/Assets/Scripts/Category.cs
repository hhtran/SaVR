using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{

	/*
	 * A category is equivalent to a "savings pile". Each category has a name, a goal amount,
	 * and the amount currently stored in the category. 
	 */

	public class Category : MonoBehaviour
	{
		#region String Parameter vars
		protected String categoryName;
		protected String prefabFolder = "Prefabs/";
		protected String defaultUnit = "GoldBar";
		protected String currentUnit = "GoldBar";
		protected Dictionary<String, float> unitMap = new Dictionary<String, float>()
		{
			{ "GoldBar", 1.0f },
			{ "PS4", 400.0f },
			{ "Starbucks", 3.50f }
		};
		#endregion

		#region Data stored vars
		protected float moneyStored;
		protected float unconvertedUnits = 0.0f;
		#endregion

		#region Root and parent grouping objects vars
		protected GameObject categoryRootObj; //The empty game object that serves as the root to hold all categories
		protected GameObject categoryParentObj; //The root game object to hold game objects related to this category
		protected GameObject moneyObjectParentObj; //The root game object to hold the money object units
		protected Dictionary<int, List<GameObject>> moneySubObjects = new Dictionary<int, List<GameObject>>(); //A dictionary mapping from a place value (e.g. 100) to an array of the child money GameObjects that are of that placevalue
		#endregion

		#region Text labels vars
		protected GameObject labelsParentObj; //The empty parent object to group the labels together
		protected GameObject amountTextLabel;
		protected GameObject categoryNameTextLabel;
		#endregion

		#region Instantiation
		public Category (String categoryName, float startingAmount, GameObject categoriesRootObj,  Vector3 position, Quaternion rotation)
		{
			moneyStored = startingAmount;
			this.categoryName = categoryName;

			createWorldObjects (startingAmount, categoriesRootObj, position, rotation);
			createCategoryLabels ();
		}

		protected void createWorldObjects(float startingAmount, GameObject categoriesRootObj, Vector3 position, Quaternion rotation) {
			categoryParentObj = new GameObject ();
			categoryParentObj.transform.position = position;
			categoryParentObj.transform.rotation = rotation;
			categoryParentObj.transform.name = categoryName;
			categoryParentObj.transform.SetParent (categoriesRootObj.transform);

			moneyObjectParentObj = new GameObject ();
			moneyObjectParentObj.transform.position = categoryParentObj.transform.position;
			moneyObjectParentObj.transform.rotation = categoryParentObj.transform.rotation;
			moneyObjectParentObj.transform.SetParent (categoryParentObj.transform);
			moneyObjectParentObj.name = "Money Parent";

			GameObject barrierPrefab = Resources.Load (prefabFolder + "Barrier", typeof(GameObject)) as GameObject;
			GameObject barrierInstance = Instantiate (barrierPrefab);
			barrierInstance.transform.position = categoryParentObj.transform.position;
			barrierInstance.transform.rotation = barrierInstance.transform.rotation;

			moneySubObjects = generateMoneyObjectsForAmount (startingAmount, currentUnit);
		}
		#endregion



		#region	Text Labels
		protected virtual void createCategoryLabels(){
			labelsParentObj = new GameObject ();
			labelsParentObj.name = "Labels Parent";
			labelsParentObj.transform.SetParent (categoryParentObj.transform);
			labelsParentObj.transform.position = categoryParentObj.transform.position;

			createCategoryTextLabel(new Vector3(-3, 9, -1), Quaternion.identity);
			createAmountTextLabel(new Vector3(-3, 7, -1), Quaternion.identity);
		}

		protected GameObject createTextLabel(Vector3 position, Quaternion rotation){
			GameObject textPrefab = Resources.Load (prefabFolder + "Text", typeof(GameObject)) as GameObject;
			GameObject textLabel = Instantiate (textPrefab) as GameObject;

			textLabel.transform.SetParent (labelsParentObj.transform);
			textLabel.transform.position = labelsParentObj.transform.position + position;
			textLabel.transform.rotation = rotation;

			return textLabel;
		}

		protected void setTextForTextLabel(GameObject textObj, String text){
			TextMesh tm = textObj.transform.GetComponent<TextMesh>();
			tm.text = text;
		}

		protected void createAmountTextLabel(Vector3 position, Quaternion rotation){
			amountTextLabel = createTextLabel (position, rotation);
			setTextForTextLabel(amountTextLabel, "Amount stored: $" + moneyStored.ToString());
		}
			
		protected void createCategoryTextLabel(Vector3 position, Quaternion rotation){
			categoryNameTextLabel = createTextLabel (position, rotation);
			setTextForTextLabel (categoryNameTextLabel, categoryName);
		}

		protected virtual void updateAllTextLabels(){
			updateAmountLabel();
			updateCategoryLabel ();
		}

		protected void updateAmountLabel() {
			setTextForTextLabel (amountTextLabel, "Amount stored: $" + moneyStored.ToString ());
		}

		protected void updateCategoryLabel(){
			setTextForTextLabel (categoryNameTextLabel, categoryName);
		}
		#endregion

		#region Money Generation

		//	Takes in a float amount and returns a dictionary of lists,
		//	where the keys are integers (1, 10, 100, etc) and the lists are 
		//	lists of GameObjects of the in-world objects representing a money amount
		protected Dictionary<int, List<GameObject>> generateMoneyObjectsForAmount(float amount, String unit){
			float convertedUnits = amount / unitMap [unit];
			unconvertedUnits += convertedUnits - (int)convertedUnits;
			Debug.Log (convertedUnits);
			Debug.Log (unconvertedUnits);
			if (unconvertedUnits >= 1.0f) {
				Debug.Log ("Trigger");
				convertedUnits += (int)unconvertedUnits;
				unconvertedUnits = unconvertedUnits - (int)unconvertedUnits;
			}

			return generateMoneyObjectsForObjectUnits (convertedUnits, unit);
		}

		// Returns the money objects ONCE THE RAW AMOUNT HAS BEEN CONVERTED TO GAME OBJECT UNITS
		protected Dictionary<int, List<GameObject>> generateMoneyObjectsForObjectUnits(float units, String unit){
			int placeValue = 1;
			int amountToConvert = (int) units;

			Dictionary<int, List<GameObject>> placeValuesMap = new Dictionary<int, List<GameObject>> ();
			while (amountToConvert > 0) {
				int numberInPlaceValue = (int)amountToConvert % ( placeValue * 10 );

				int digitValue = numberInPlaceValue / placeValue;
				amountToConvert -= numberInPlaceValue;

				//Convert to objects for this place value
				List<GameObject> placeValueObjectList = createPlaceValueObjects(placeValue, digitValue, unit);
				placeValuesMap.Add (placeValue, placeValueObjectList);

				//Increment
				placeValue *= 10;
			}
			return placeValuesMap;
		}

		// Returns a list of in-world game objects for a given number of objects, under the category parent object
		protected List<GameObject> createPlaceValueObjects(int placeValue, float numberOfObjs, String unit){
			List<GameObject> placeValueObjectList = new List<GameObject> ();
			GameObject prefabForPlaceValue = getPrefabForPlaceValue (placeValue, unit);

			for (int i = 0; i < numberOfObjs; i++) {
				Vector3 moneyObjPosition = moneyObjectParentObj.transform.position;
				moneyObjPosition.y += i + 5;
				GameObject moneyPrefabInstance = Instantiate (prefabForPlaceValue, moneyObjPosition, Quaternion.identity) as GameObject;
				moneyPrefabInstance.transform.SetParent (moneyObjectParentObj.transform);

				placeValueObjectList.Add (moneyPrefabInstance);
			}
			return placeValueObjectList;
		}

		// Retrieves the prefab for the money object. Note that the prefab folder must be inside the resources folder
		protected GameObject getPrefabForPlaceValue(int placeValue, String unit) {
			if ((placeValue != 1 && placeValue % 10 > 0) || placeValue > 1000)
				return new GameObject ();
			return Resources.Load (prefabFolder + unit + " " + placeValue.ToString(), typeof(GameObject)) as GameObject;
		}
		#endregion

		#region Setters/Getters

		public float getMoneyStored () {
			return moneyStored;
		}



		public virtual void addMoney(float amount) {
			Dictionary<int, List<GameObject>> newMoneyObjectsMap = generateMoneyObjectsForAmount (amount, currentUnit);

			foreach (int placeValue in newMoneyObjectsMap.Keys) {
				if (moneySubObjects.ContainsKey (placeValue)) {
					moneySubObjects [placeValue].AddRange (newMoneyObjectsMap [placeValue]);
				} else {
					moneySubObjects.Add (placeValue, newMoneyObjectsMap[placeValue]);
				}
			}

			moneyStored += amount;
			updateAmountLabel ();
		}

		public void removeMoney(float amount) {
			if (amount > moneyStored) {
				animateMoneyRemoval (moneyStored);
				moneyStored = 0;
			} else {
				moneyStored -= amount;
				animateMoneyRemoval (amount);
			}

			updateAmountLabel ();
		}

		protected void animateMoneyRemoval(float amount){

		}
			
		#endregion



		#region	Convert visualization objects
		public void convertVisualizationUnit(String unit) {
			unconvertedUnits = 0.0f;
			currentUnit = unit;
			destroyMoneyObjects ();
			moneySubObjects = generateMoneyObjectsForAmount (moneyStored, currentUnit);
		}

		protected void destroyMoneyObjects(){
			foreach (Transform child in moneyObjectParentObj.transform) {
				GameObject.Destroy(child.gameObject);
			}
		}
		#endregion
	

	}
}
