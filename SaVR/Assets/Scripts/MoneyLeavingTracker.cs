using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{

	public class MoneyLeavingTracker : MonoBehaviour {

		public Category categoryScript;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		#region Moving money in and out

		void onTriggerEnter(Collider other){
			Debug.Log("Barrier being entered");
			if (other.gameObject.layer == LayerMask.NameToLayer("Money") || other.gameObject.layer == LayerMask.NameToLayer("GrabbedMoney") )
			{
				Debug.Log("Money entering");
				Money moneyScript = other.GetComponent<Money>();
				categoryScript.moneyStored += moneyScript.value;
				categoryScript.updateAllTextLabels();
			}
		}

		void onTriggerExit(Collider other)
		{

			Debug.Log("Barrier being exited");
			if (other.gameObject.layer == LayerMask.NameToLayer("Money") || other.gameObject.layer == LayerMask.NameToLayer("GrabbedMoney") )
			{
				Debug.Log("Money leaving");
				Money moneyScript = other.GetComponent<Money>();
				categoryScript.moneyStored -= moneyScript.value;
				categoryScript.updateAllTextLabels();
			}
		}

		#endregion
	}

}
