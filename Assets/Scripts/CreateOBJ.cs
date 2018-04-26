using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateOBJ : MonoBehaviour {

    string filename = "OBJs/MiObj.obj";
    public GameObject obj;
    Vector3[] vertices;
    Vector2[] uvs;
    Mesh mesh;
    int triangleCount;
    Vector3[] normals;
    int[] triangles;

    List<Vector3> trianglesIndexList = new List<Vector3>();
    List<Vector2> uvsIndexList = new List<Vector2>();
        


	// Use this for initialization
	void Start () {

        mesh = obj.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        uvs = mesh.uv;
      
        normals = mesh.normals;

        triangleCount = mesh.triangles.Length / 3;


        triangles = mesh.triangles;
        
        int moveTriangle = 0;
        for (int i = 0; i < triangleCount; i++)
        {
            trianglesIndexList.Add(new Vector3(triangles[i+moveTriangle], triangles[i+1+moveTriangle], triangles[i+2+moveTriangle]));
            moveTriangle = moveTriangle + 2;
        }
        moveTriangle = 0;
        





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
        sr.WriteLine("o Cosito");
        for(int i = 0; i < vertices.Length; i++)
        {
            sr.WriteLine("v {0} {1} {2}", vertices[i].x, vertices[i].y, vertices[i].z);
        }
        for (int i = 0; i < uvs.Length; i++)
        {
            sr.WriteLine("vt {0} {1}", uvs[i].x, uvs[i].y);
            uvsIndexList.Add(new Vector2(uvs[i].x, uvs[i].y));
        }
        
        for(int i = 0; i < normals.Length; i++)
        {
            sr.WriteLine("vn {0} {1} {2}", normals[i].x, normals[i].y, normals[i].z);
        }

        


        int whereIsIt = 0;
        for(int j = 0; j < triangleCount; j++)
        {
            sr.Write("f ");

            for (int i = 0; i < 3; i++)
            {
                if(whereIsIt == 0)
                {
                    sr.Write(" {0}/{1}/{2} ", trianglesIndexList[j].x + 1, trianglesIndexList[j].x+1, trianglesIndexList[j].x+1);
                    whereIsIt++;
                }
                else if(whereIsIt == 1)
                {
                    sr.Write(" {0}/{1}/{2} ", trianglesIndexList[j].y + 1, trianglesIndexList[j].y + 1, trianglesIndexList[j].y + 1);
                    whereIsIt++;
                }
                else if(whereIsIt == 2)
                {
                    sr.Write(" {0}/{1}/{2} ", trianglesIndexList[j].z + 1, trianglesIndexList[j].z + 1, trianglesIndexList[j].z + 1);
                    whereIsIt++;
                }
            }

            whereIsIt = 0;
            sr.WriteLine(" ");
        }



        sr.Close();
    }
	
}
