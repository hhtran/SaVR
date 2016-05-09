using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WorldController : MonoBehaviour {

	public GameObject categoriesParent;
	private Category mostRecentCategory;

	private int numberOfCategories = 0;

	Category createCategory(){
		Vector3 position = new Vector3 (numberOfCategories * 15, 0, 0);
		Category category = new Category ("Savings Pile", 1000.0f, 100.0f, categoriesParent, position, Quaternion.identity);
		numberOfCategories++;
		return category;
	}

	/* Key mappings for actions
	 * C: Add a new category/savings pile
	 * 0: Add 115 to the most recently created category
	 * 1: Add 1 to the most recently created category
	 * 2: Add 10 to the most recently created category
	 * 3: Add 100 to the most recently created category
	 * 4: Add 1000 to the most recently created category
	 */
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)){
			mostRecentCategory = createCategory();
		}
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			mostRecentCategory.addMoney (115.0f);
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			mostRecentCategory.addMoney (1.0f);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			mostRecentCategory.addMoney (10.0f);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			mostRecentCategory.addMoney (100.0f);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			mostRecentCategory.addMoney (1000.0f);
		}
	}
}
