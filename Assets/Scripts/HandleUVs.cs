using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HandleUVs : MonoBehaviour {

    Mesh mesh;
	Vector3[] vertices;
    public Vector2[] uvs;

    


    // Use this for initialization
    void Start () {

        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        uvs = mesh.uv;
       
    }
	
	// Update is called once per frame
	void Update () {

        mesh.uv = uvs;
        
	}
}
