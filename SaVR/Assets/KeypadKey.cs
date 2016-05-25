using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class KeypadKey : MonoBehaviour {
    public string keytext = "";
    public string keyvalue = "";
    TextMesh tm;

	// Use this for initialization
	void Start () {
        tm = GetComponentsInChildren<TextMesh>(false)[0] as TextMesh;
        tm.text = keytext;
    }
	
	// Update is called once per frame
	void Update () {
        tm.text = keytext;
    }

    void OnInspectorGUI()
    {
        tm = GetComponentsInChildren<TextMesh>(false)[0] as TextMesh;
        tm.text = keytext;
    }
}
