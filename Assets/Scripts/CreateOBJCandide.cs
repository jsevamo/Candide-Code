using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateOBJCandide : MonoBehaviour {

	string filename = "OBJs/Candide.obj";
	string path = "Assets/Models/Candide/Candide2.obj";

	// Use this for initialization
	void Start () {

		StreamReader reader = new StreamReader(path); 
		//Debug.Log(reader.ReadToEnd());
		//reader.Close();


		if (File.Exists(filename))
		{
			Debug.Log(filename + " ya existe, cancelado.");
			return;
		}
		else
		{
			Debug.Log(filename + " creado correctamente.");
		}

		var sr = File.CreateText(filename);
		sr.WriteLine("# OBJ Exporter For Unity");
		sr.WriteLine("# GIM Research Group. Universidad Militar Nueva Granada.");
		sr.WriteLine("# 2018");
		sr.WriteLine (reader.ReadToEnd ());
		reader.Close();

		sr.Close();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
