using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateOBJ : MonoBehaviour {

    string filename = "MyFile.obj";

	// Use this for initialization
	void Start () {
        if (File.Exists(filename))
        {
            Debug.Log(filename + " already exists.");
            return;
        }

        var sr = File.CreateText(filename);
        sr.WriteLine("This is my file. jajajajaja");
        sr.WriteLine("I can write ints {0} or floats {1}, and so on.", 1, 4.2);
        sr.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
