using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WorldController : MonoBehaviour {

	public PlayerAccount playerAccount;
	public GameObject moneyPrefab;
	public GameObject categoriesParent;

	// Use this for initialization
	void Start () {
		playerAccount = new PlayerAccount ();
		Debug.Log (playerAccount.getTotalMoney ());
	}

	void createCategory(){
		Category category = new Category ("Savings Pile", 1000.0f, 0.0f, moneyPrefab, categoriesParent);
		category.addMoney (100.0f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)){
			createCategory();
		}
	}
}
