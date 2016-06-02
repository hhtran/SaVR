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

    public void receiveInput(string input)
    {
        wc.receiveKey(input);
        
    }

    // Update is called once per frame
    void Update() {
        
    }




}

}