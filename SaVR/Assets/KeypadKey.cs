using UnityEngine;
using System.Collections;


namespace AssemblyCSharp
{
    [ExecuteInEditMode]

    public class KeypadKey : MonoBehaviour {
        public string keytext = "";
        public string keyvalue = "";
        public GameObject keypad;

        public Color unpressedColor;
        public Color pressedColor;
        TextMesh tm;

        // Use this for initialization
        void Start() {
            tm = GetComponentsInChildren<TextMesh>(false)[0] as TextMesh;
            tm.text = keytext;
        }

        // Update is called once per frame
        void Update() {
            tm.text = keytext;
        }

        void changeKeyColor(Color color)
        {
			foreach(Transform child in gameObject.transform) {
				GameObject childObject = child.gameObject;
				if (child.name == "Key Shape") {
					Material mat = childObject.GetComponent<MeshRenderer>().material;
					mat.color = color;
				}
			}

        }

        void OnInspectorGUI()
        {
            tm = GetComponentsInChildren<TextMesh>(false)[0] as TextMesh;
            tm.text = keytext;
        }

        void OnDisable()
        {
            changeKeyColor(unpressedColor);
        }

        void OnTriggerEnter(Collider other)
        {
            KeypadInput keypadInput = keypad.GetComponent<KeypadInput>() as KeypadInput;
            keypadInput.receiveInput(keyvalue);
            changeKeyColor(pressedColor);
        }

        void OnTriggerExit(Collider other)
        {
            changeKeyColor(unpressedColor);

        }
    }

}