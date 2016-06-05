using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AssemblyCSharp {

public class Store : MonoBehaviour {

	public GameObject pointsPile;
	public Category pointsScript;
	protected string prefabFolder = "Prefabs/";
		public List<Item> itemsForSale = new List<Item> { new Item ("Helicopter", 10000.0f, "Images/helicopter", "helicopter"), new Item ("Car", 1000.0f, "Images/car", "car"), new Item ("Boat", 900.0f, "Images/boat", "boat") };

	public struct Item
	{
		public string name;
		public float price;
		public string imagePath;
		public string prefabPath;

		public Item(string name, float price, string imagePath, string prefabPath){
			this.name = name;
			this.price = price;
			this.imagePath = imagePath;
			this.prefabPath = prefabPath;
		}
	}
			
	// Use this for initialization
	void Start () {
				
	}

	void buy(Item item){
		if (pointsScript.getMoneyStored () < item.price)
			return;

		Instantiate (Resources.Load (prefabFolder + item.prefabPath), new Vector3 (0f, 1f, 0f), Quaternion.identity);
		pointsScript.removeMoney (item.price);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

}