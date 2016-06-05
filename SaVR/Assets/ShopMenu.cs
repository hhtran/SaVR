using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp {

public class ShopMenu : MonoBehaviour {

	public Store store;
	public List<GameObject> storeOptionKeys;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < storeOptionKeys.Count; i++) {
			SpriteRenderer spriteRenderer = storeOptionKeys [i].GetComponentsInChildren<SpriteRenderer>()[0];
			Store.Item itemToLoad = store.itemsForSale [i];

			KeypadKey keyScript = storeOptionKeys [i].GetComponent<KeypadKey> ();
			keyScript.keytext = itemToLoad.price.ToString();
			keyScript.keyvalue = itemToLoad.name;
			
			Sprite itemSprite = Resources.Load(itemToLoad.imagePath, typeof(Sprite)) as Sprite;
			spriteRenderer.sprite = itemSprite;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

}