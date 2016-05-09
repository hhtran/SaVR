using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Category : MonoBehaviour
	{
		private float moneyStored;
		private float goalAmount;
		public GameObject moneyPrefab;
		public GameObject categoryObj;

		public Category (GameObject moneyPrefabObj, GameObject categoriesParentObj, String categoryName)
		{
			categoryObj = new GameObject ();
			categoryObj.transform.name = categoryName;
			moneyStored = 0.0f;
			goalAmount = 1000.0f;
			moneyPrefab = moneyPrefabObj;

			categoryObj.transform.SetParent (categoriesParentObj.transform);
			GameObject moneyPrefabInstance = Instantiate (moneyPrefab, categoryObj.transform.position , Quaternion.identity) as GameObject;
			moneyPrefabInstance.transform.SetParent (categoryObj.transform);

		}

		public float getMoneyStored () {
			return moneyStored;
		}

		public float getGoalAmount () {
			return goalAmount;
		}

		public void addMoney(float amount) {
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

