using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp {

public class ShopMenu : MonoBehaviour {

	public GameObject store;
    private Store storeScript;
	public List<GameObject> storeOptionKeys;
	public GameObject pointsLabel;

	// Use this for initialization
	void Start () {
        storeScript = store.GetComponent<Store>();

        Dictionary<string, Store.Item>.ValueCollection itemsForSale = storeScript.itemsForSale.Values;
		Dictionary<string, Store.Item>.ValueCollection.Enumerator enu = itemsForSale.GetEnumerator ();
		for (int i = 0; i < storeOptionKeys.Count; i++) {
			SpriteRenderer spriteRenderer = storeOptionKeys [i].GetComponentsInChildren<SpriteRenderer>()[0];
			enu.MoveNext ();
			Store.Item itemToLoad = enu.Current;

			KeypadKey keyScript = storeOptionKeys [i].GetComponent<KeypadKey> ();
			keyScript.keytext = itemToLoad.price.ToString();
			keyScript.keyvalue = "shop-" + itemToLoad.name;
			
			Sprite itemSprite = Resources.Load(itemToLoad.imagePath, typeof(Sprite)) as Sprite;
			spriteRenderer.sprite = itemSprite;
		}
	}
				
	// Update is called once per frame
	void Update () {
            storeScript = store.GetComponent<Store>();
            if (storeScript != null)
            {
                TextMesh pointsLabelTextMesh = pointsLabel.GetComponent<TextMesh>();
                float pointsAvailable = storeScript.pointsScript.getMoneyStored();
                pointsLabelTextMesh.text = "Points Available: " + pointsAvailable.ToString();
            }

	}
}

}