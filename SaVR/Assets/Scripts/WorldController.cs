using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WorldController : MonoBehaviour {

	public PlayerAccount playerAccount;
	public GameObject categoriesParent;
	private Category mostRecentCategory;

	private int numberOfCategories = 0;

	// Use this for initialization
	void Start () {
		playerAccount = new PlayerAccount ();
		Debug.Log (playerAccount.getTotalMoney ());
	}

	Category createCategory(){
		Vector3 position = new Vector3 (numberOfCategories * 4, 0, 0);
		Category category = new Category ("Savings Pile", 1000.0f, 100.0f, categoriesParent, position, Quaternion.identity);
		numberOfCategories++;
		return category;
	}

	// Update is called once per frame
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
