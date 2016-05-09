using UnityEngine;
using System.Collections;

//Holds data about a tree
public class TreeData : MonoBehaviour
{
	public int listPos;

	void Awake()
	{
		//-1 means not in any list at all
		listPos = -1;
	}
}