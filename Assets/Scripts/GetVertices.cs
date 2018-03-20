/*
 * TODO: Give an ID to each vertex.
 * */

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class Vertices
{
    public Vector3[] vertices;
    public ArrayList ogVertices = new ArrayList();

    public Vertices(Vector3[] newVertices, ArrayList newOgVertices)
    {
        vertices = newVertices;
        ogVertices = newOgVertices;
    }
}

public class GetVertices : MonoBehaviour {

    //let's move (-0.5, -0.5, 0) to (-0.5, 0.5, 0)
    [Range(0, 1)]
    public float carne;
    public int VertexID;
    int LastVertexID;


    Vector3 destination = new Vector3(0f, 0f, 0);
    Vector3[] vertices;


    Mesh mesh;
    ArrayList ogVertices = new ArrayList();

    Vector3 foundVector;
    int searchID;
    bool hasFound;

    //------------------------------------------------

   
    // Use this for initialization
    void Start () {

        hasFound = false;
        VertexID = 0;
        LastVertexID = VertexID;

        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        foundVector = new Vector3(0, 0, 0);
        


        for (int i = 0; i < vertices.Length; i++)
        {
            //Vector4 v = vertices[i];
            float x = vertices[i].x;
            float y = vertices[i].y;
            float z = vertices[i].z;
            float w = 0;

            Vector4 v;
            v.x = x;
            v.y = y;
            v.z = z;
            v.w = w;

            ogVertices.Add(copyCoordinate(v));
            
        }

        GameObject gameController = GameObject.Find("GameController");
        ParseXML parseXML = gameController.GetComponent<ParseXML>();
        

        for (int i = 0; i < ogVertices.Count; i++)
        {
            Vector4 v1 = (Vector4)ogVertices[i];


            for (int j = 0; j < ogVertices.Count; j++)
            {
                Vector4 v2 = parseXML.candideVertexList[j];

                if(System.Math.Round(v1.x, 2) == System.Math.Round(v2.x, 2) && System.Math.Round(v1.y, 2) == System.Math.Round(v2.y, 2) && System.Math.Round(v1.z, 2) == System.Math.Round(v2.z, 2))
                {
                    v1.w = v2.w;
                    break;
                }


            }

            ogVertices[i] = v1;

        }



    }

    // Update is called once per frame
    void Update() {

        


        if (hasFound == false)
        {
            for (int i = 0; i < ogVertices.Count; i++)
            {
                Vector4 v;
                v = (Vector4)ogVertices[i];

                if (VertexID == v.w)
                {
                    foundVector = v;
                    searchID = System.Array.IndexOf(vertices, foundVector);
                    hasFound = true;
                    LastVertexID = VertexID;

                    break;
                }


            }
        }


        if(LastVertexID!=VertexID)
        {
            hasFound = false;   
        }

        
        vertices[searchID] = MoveVertexTo((Vector4)ogVertices[searchID], destination, carne);

        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        //Debug.Log(searchID);
        Debug.Log(vertices[0]);

        //Shit
        
        

    }

    Vector4 copyCoordinate(Vector4 v)
    {
        Vector4 copy;
        copy = v;
        return copy;
    }


    Vector3 MoveVertexTo(Vector3 original, Vector3 goalPosition, float t)
    {
        /*
         * This is a test.
         * */
        Vector3 inital = original;


        inital.x = (1 - t) * inital.x + (t * goalPosition.x);
        inital.y = (1 - t) * inital.y + (t * goalPosition.y);
        inital.z = (1 - t) * inital.z + (t * goalPosition.z);

        return inital;

    }


    
}
