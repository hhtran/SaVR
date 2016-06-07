using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AssemblyCSharp {

public class Store : MonoBehaviour {

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

	public Category pointsScript;
	protected string prefabFolder = "Prefabs/";

	private static Item helicopter = new Item("helicopter", 10000.0f, "Images/helicopter", "helicopter");
	private static Item car = new Item("car", 1000.0f, "Images/car", "car");
	private static Item boat = new Item("boat", 900.0f, "Images/boat", "boat");
	public Dictionary<string, Item> itemsForSale = new Dictionary<string, Item> () {
		{"helicopter", helicopter },
		{"car", car},
		{"boat", boat}
	};


			
	// Use this for initialization
	void Start () {
				
	}

	public void buy(string itemName){
		Item itemBeingBought = itemsForSale [itemName];

		if (pointsScript.getMoneyStored () < itemBeingBought.price)
			return;

		Instantiate (Resources.Load (prefabFolder + itemBeingBought.prefabPath), new Vector3 (0f, 5f, -5f), Quaternion.identity);
		pointsScript.removeMoney (itemBeingBought.price);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

}