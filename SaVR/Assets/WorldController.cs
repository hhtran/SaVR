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
		if (Input.GetKeyDown (KeyCode.A)) {
			mostRecentCategory.addMoney (115.0f);
		}
	}
}
