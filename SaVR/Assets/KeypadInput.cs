using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace AssemblyCSharp {

public class KeypadInput : MonoBehaviour {

    public List<KeypadKey> keys;
    public GameObject WorldController;
    WorldController wc;

    // Use this for initialization
    void Start() {
        wc = WorldController.GetComponent<WorldController>();

    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidingObject = other.gameObject;
        if (collidingObject.CompareTag("Key"))
        {
            KeypadKey kk = collidingObject.GetComponent<KeypadKey>();
            string keyValue = kk.keyvalue;
            wc.receiveKey(keyValue);
        }
    }


}

}