using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ObjTest : MonoBehaviour {

	public Text AuName;

	// Use this for initialization
	void Start () {

		GameObject a = Resources.Load ("import1") as GameObject;
		//GetVertices GV = a.AddComponent (typeof(GetVertices)) as GetVertices;

		GameObject CandideImported = Instantiate (a, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		////GetVertices GV = CandideImported.transform.GetChild (0).gameObject.AddComponent (typeof(GetVertices)) as GetVertices;
		//CandideImported.transform.GetChild (0).gameObject.AddComponent<GetVertices> ();
		////GV.ActionUnitNameText = AuName;


	}
	

}
