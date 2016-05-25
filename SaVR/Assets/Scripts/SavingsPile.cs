using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class SavingsPile : Category
	{
		private float goalAmount;
		private bool firstCompletionOfGoal = true;

		#region Text labels vars
		private GameObject goalTextLabel;
		#endregion

		#region Instantiation
		public SavingsPile (String categoryName, float goalAmount, float startingAmount, GameObject categoriesRootObj,  Vector3 position, Quaternion rotation) :
		base(categoryName, startingAmount, categoriesRootObj,  position, rotation)
		{
			this.goalAmount = goalAmount;
			updateAllTextLabels ();
		}
		#endregion

		#region Goal Checking/Completion

		//Checks to see if the goal has been completed. Should be called whenever the amount or goal amount is changed
		private void checkForGoalCompletion(){
			if (moneyStored >= goalAmount && firstCompletionOfGoal) {
				GameObject completionAnimation = Instantiate (Resources.Load (prefabFolder + "Dollar Rain", typeof(GameObject))) as GameObject;
				completionAnimation.transform.position = categoryParentObj.transform.position + new Vector3(0, 5, 0);
				firstCompletionOfGoal = false;
				Debug.Log ("Completed goal");
			}
		}
		#endregion

		#region Setters/Getters
		public float getGoalAmount () {
			return goalAmount;
		}


		public override void addMoney(float amount) {
			base.addMoney (amount);
			checkForGoalCompletion ();
		}

		public void changeGoalAmount (float newGoalAmount) {
			goalAmount = newGoalAmount;
			updateGoalLabel ();
			checkForGoalCompletion ();
		}

		#endregion

		#region Text Labels

		private void updateGoalLabel(){
			setTextForTextLabel (goalTextLabel, "Goal: $" + goalAmount.ToString ());
		}

		protected override void updateAllTextLabels(){
			base.updateAllTextLabels ();
			updateGoalLabel ();
		}

		protected override void createCategoryLabels(){
			base.createCategoryLabels ();
			createGoalTextLabel(goalAmount);
		}

		protected void createGoalTextLabel(float goalAmount){
			goalTextLabel = createTextLabel (new Vector3(-3, 5, -1), Quaternion.identity);
			setTextForTextLabel (goalTextLabel, "Goal: $" + goalAmount.ToString ());
		}

		#endregion

	}
}

