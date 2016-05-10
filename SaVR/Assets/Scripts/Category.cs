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
		private String categoryName;
		private String prefabFolder = "Prefabs/";
		private String defaultUnit = "GoldBar";
		private String currentUnit = "GoldBar";
		private Dictionary<String, float> unitMap = new Dictionary<String, float>()
		{
			{ "GoldBar", 1.0f },
			{ "PS4", 400.0f },
			{ "Starbucks", 3.50f }
		};
		#endregion

		#region Data stored vars
		private float moneyStored;
		private float goalAmount;
		private bool firstCompletionOfGoal = true;
		#endregion

		#region Root and parent grouping objects vars
		private GameObject categoryRootObj; //The empty game object that serves as the root to hold all categories
		private GameObject categoryParentObj; //The root game object to hold game objects related to this category
		private Dictionary<int, List<GameObject>> moneySubObjects = new Dictionary<int, List<GameObject>>(); //A dictionary mapping from a place value (e.g. 100) to an array of the child money GameObjects that are of that placevalue
		#endregion

		#region Text labels vars
		private GameObject labelsParentObj; //The empty parent object to group the labels together
		private GameObject amountTextLabel;
		private GameObject categoryNameTextLabel;
		private GameObject goalTextLabel;
		#endregion

		#region Instantiation
		public Category (String categoryName, float goalAmount, float startingAmount, GameObject categoriesRootObj,  Vector3 position, Quaternion rotation)
		{
			moneyStored = startingAmount;
			this.goalAmount = goalAmount;
			this.categoryName = categoryName;

			createWorldObjects (startingAmount, categoriesRootObj, position, rotation);
			createCategoryLabels (startingAmount, goalAmount);
		}
		#endregion

		#region Goal Checking/Completion

		//Checks to see if the goal has been completed. Should be called whenever the amount or goal amount is changed
		private void checkForGoalCompletion(){
			if (moneyStored >= goalAmount && firstCompletionOfGoal) {
				GameObject completionAnimation = Instantiate (Resources.Load (prefabFolder + "Goal Complete", typeof(GameObject))) as GameObject;
				completionAnimation.transform.position = categoryParentObj.transform.position + new Vector3(0, 5, 0);
				firstCompletionOfGoal = false;
				Debug.Log ("Completed goal");
			}
		}
		#endregion

		#region	Text Labels Creation	
		private void createCategoryLabels(float startingAmount, float goalAmount){
			labelsParentObj = new GameObject ();
			labelsParentObj.name = "Labels Parent";
			labelsParentObj.transform.SetParent (categoryParentObj.transform);
			labelsParentObj.transform.position = categoryParentObj.transform.position;

			createAmountTextLabel(startingAmount);
			createGoalTextLabel(goalAmount);
			createCategoryTextLabel();
		}

		private GameObject createTextLabel(Vector3 position, Quaternion rotation){
			GameObject textPrefab = Resources.Load (prefabFolder + "Text", typeof(GameObject)) as GameObject;
			GameObject textLabel = Instantiate (textPrefab) as GameObject;

			textLabel.transform.SetParent (labelsParentObj.transform);
			textLabel.transform.position = labelsParentObj.transform.position + position;
			textLabel.transform.rotation = rotation;

			return textLabel;
		}

		private void setTextForTextLabel(GameObject textObj, String text){
			TextMesh tm = textObj.transform.GetComponent<TextMesh>();
			tm.text = text;
		}

		private void createAmountTextLabel(float startingAmount){
			amountTextLabel = createTextLabel (new Vector3(-3, 1, -1), Quaternion.identity);
			setTextForTextLabel(amountTextLabel, "Amount stored: $" + startingAmount.ToString());
		}

		private void createGoalTextLabel(float goalAmount){
			goalTextLabel = createTextLabel (new Vector3(-3, 3, -1), Quaternion.identity);
			setTextForTextLabel (goalTextLabel, "Goal: $" + goalAmount.ToString ());
		}

		private void createCategoryTextLabel(){
			categoryNameTextLabel = createTextLabel (new Vector3(-3, 5, -1), Quaternion.identity);
			setTextForTextLabel (categoryNameTextLabel, categoryName);
		}

		private void createWorldObjects(float startingAmount, GameObject categoriesRootObj, Vector3 position, Quaternion rotation) {
			categoryParentObj = new GameObject ();
			categoryParentObj.transform.position = position;
			categoryParentObj.transform.rotation = rotation;
			categoryParentObj.transform.name = categoryName;
			categoryParentObj.transform.SetParent (categoriesRootObj.transform);

			GameObject barrierPrefab = Resources.Load (prefabFolder + "Barrier", typeof(GameObject)) as GameObject;
			GameObject barrierInstance = Instantiate (barrierPrefab);
			barrierInstance.transform.position = categoryParentObj.transform.position;
			barrierInstance.transform.rotation = barrierInstance.transform.rotation;

			moneySubObjects = generateMoneyObjectsForAmount (startingAmount, categoryParentObj, currentUnit);
		}
		#endregion

		#region	Updating Labels

		private void updateAllTextLabels(){
			updateAmountLabel();
			updateGoalLabel ();
			updateCategoryLabel ();
		}

		private void updateAmountLabel() {
			setTextForTextLabel (amountTextLabel, "Amount stored: $" + moneyStored.ToString ());
		}

		private void updateGoalLabel(){
			setTextForTextLabel (goalTextLabel, "Goal: $" + goalAmount.ToString ());
		}

		private void updateCategoryLabel(){
			setTextForTextLabel (categoryNameTextLabel, categoryName);
		}
		#endregion

		#region Money Generation
		//	Takes in a float amount and returns a dictionary of lists,
		//	where the keys are integers (1, 10, 100, etc) and the lists are 
		//	lists of GameObjects of the in-world objects representing a money amount
		private Dictionary<int, List<GameObject>> generateMoneyObjectsForAmount(float amount, GameObject categoryParentObj, String unit){
			float convertedUnits = amount / unitMap [unit];

			return generateMoneyObjectsForObjectUnits (convertedUnits, categoryParentObj, unit);
		}

		// Returns the money objects ONCE THE RAW AMOUNT HAS BEEN CONVERTED TO GAME OBJECT UNITS
		private Dictionary<int, List<GameObject>> generateMoneyObjectsForObjectUnits(float units, GameObject categoryParentobj, String unit){
			int placeValue = 1;
			int amountToConvert = (int) units;

			Dictionary<int, List<GameObject>> placeValuesMap = new Dictionary<int, List<GameObject>> ();
			while (amountToConvert > 0) {
				int numberInPlaceValue = (int)amountToConvert % ( placeValue * 10 );

				int digitValue = numberInPlaceValue / placeValue;
				amountToConvert -= numberInPlaceValue;

				//Convert to objects for this place value
				List<GameObject> placeValueObjectList = createPlaceValueObjects(placeValue, digitValue, categoryParentObj, unit);
				placeValuesMap.Add (placeValue, placeValueObjectList);

				//Increment
				placeValue *= 10;
			}
			return placeValuesMap;
		}

		// Returns a list of in-world game objects for a given number of objects, under the category parent object
		private List<GameObject> createPlaceValueObjects(int placeValue, float numberOfObjs, GameObject categoryParentObject, String unit){
			List<GameObject> placeValueObjectList = new List<GameObject> ();
			GameObject prefabForPlaceValue = getPrefabForPlaceValue (placeValue, unit);

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
		private GameObject getPrefabForPlaceValue(int placeValue, String unit) {
			if ((placeValue != 1 && placeValue % 10 > 0) || placeValue > 1000)
				return new GameObject ();
			return Resources.Load (prefabFolder + unit + " " + placeValue.ToString(), typeof(GameObject)) as GameObject;
		}
		#endregion

		#region Setters/Getters

		public float getMoneyStored () {
			return moneyStored;
		}

		public float getGoalAmount () {
			return goalAmount;
		}

		public void addMoney(float amount) {
			Dictionary<int, List<GameObject>> newMoneyObjectsMap = generateMoneyObjectsForAmount (amount, categoryParentObj, currentUnit);

			foreach (int placeValue in newMoneyObjectsMap.Keys) {
				if (moneySubObjects.ContainsKey (placeValue)) {
					moneySubObjects [placeValue].AddRange (newMoneyObjectsMap [placeValue]);
				} else {
					moneySubObjects.Add (placeValue, newMoneyObjectsMap[placeValue]);
				}
			}

			moneyStored += amount;
			updateAmountLabel ();
			checkForGoalCompletion ();
		}

		public void removeMoney(float amount) {
			moneyStored -= amount;
			updateAmountLabel ();
		}

		public void changeGoalAmount (float newGoalAmount) {
			goalAmount = newGoalAmount;
			updateGoalLabel ();
			checkForGoalCompletion ();
		}
		#endregion



		#region	Convert visualization objects
		public void convertVisualizationUnit() {

		}
		#endregion
	

	}
}
