/*
 * TODO: FIX THE BUG WITH THE VERTEX NOT MOVING
 * */

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

/**
* This is a class that is used to manage the vertices of any given mesh.
* How it works:
* 1. First it gets all of the vertices of the mesh and stores them in the vertices array.
* 2. It connects with the ParseXML class to get the vertex order and ID of the vertices in the XML.
* 3. It saves the Vertex that come from ParseXML to the "VerticesWithIndex" list.
* 4. It saves the ID corresponding to each vertex to the "indexList" list.
* 5. It searches through the "indexList" for a coincidence on the ID of the vertices in the "VerticesWithIndex" list.
* 6. If it finds a coincidence, "searchID" is changed for the index it finds and moves the vertex in vertices[searchID] with MoveVertexTo().
* 
*
*/

public class UnityVertexMatch
{
    //int CadideID;
    List<int> UnityVertex;
};


public class GetVertices : MonoBehaviour {



    int choose = 0;

    //public Text textoPrueba;

    [Range(0, 1)]
    /**
    * This is a variable that the user or the computer can control. It's the key for the 
    * parametrization of the line used to move a vertex from point A to point B.
    * It's used here: L = (1-t)*A + t*B
    * Where: 
    * - A is the starting point on 3D space.
    * - B is the ending point on 3D space.
    * 
    * When t = 0, L = A, which means the vertex remains at it's original point.
    * When t = 1, L = B, which means the vertex moves to it's ending point.
    * t can also take any other value in between 0 and 1 to move the vertex to that corresponding position.
    */
    public float t;

    /**
    * This is a variable the user or the computer can control. It let's us choose which 
    * vertex we want to move with the parametrization.
    */
    public int AuID;


    Vector3 destination = new Vector3(0f, 0f, 0);

    /**
    * This is the array of vertices that unity saves with it's built in GetComponent<MeshFilter>().mesh.vertices function.
    * Unfortunately, Unity saves the vertices in a very different order that what it's required by the project, so 
    * this only serves us to make a comparison with VerticesWithIndex later to find matches and get the corresponding
    * index.
    */
    [HideInInspector]
    public Vector3[] vertices;


    Mesh mesh;
    ArrayList ogVertices = new ArrayList();

    Vector3 foundVector;
    bool hasFound;

    /**
    * This is a test.
    */
    [HideInInspector]
    public int searchID = -1;

    /**
    * This is a test.
    */
    [HideInInspector]
    public List<Vector4> VerticesWithIndex = new List<Vector4>();
    /**
    * This is a test.
    */
    [HideInInspector]
    public List<int> indexList = new List<int>();



    //public List<int> VertexIDList = new List<int>();
    //public List<Vector3> VertexDestinationList = new List<Vector3>();


    public List<ActionUnit> ActionUnitList = new List<ActionUnit>();


    //------------------------------------------------

    int indiceEncontrado = -1;

    List<int> indexToMove = new List<int>();


    // Use this for initialization
    void Start() {

        hasFound = false;
        AuID = 0;

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



        Vector3 test = new Vector3(-100f, 150f, 70f);

         Debug.Log(vertices[1]);


         for(int i = 0; i < vertices.Length; i++)
         {
             if(vertices[i].x == test.x && vertices[i].y == test.y && vertices[i].z == test.z)
             {
                indexToMove.Add(i);
             }
         }

         Debug.Log(indexToMove.Count);


        /* GameObject gameController = GameObject.Find("GameController");
         ParseXML parseXML = gameController.GetComponent<ParseXML>();


         for (int i = 0; i < ogVertices.Count; i++)
         {
             Vector4 v1 = (Vector4)ogVertices[i];


             for (int j = 0; j < ogVertices.Count; j++)
             {
                 Vector4 v2 = parseXML.candideVertexList[j];

                 if (System.Math.Round(v1.x, 2) == System.Math.Round(v2.x, 2) && System.Math.Round(v1.y, 2) == System.Math.Round(v2.y, 2) && System.Math.Round(v1.z, 2) == System.Math.Round(v2.z, 2))
                 {
                     v1.w = v2.w;
                     break;
                 }


             }


             ogVertices[i] = v1;
             indexList.Add(Mathf.RoundToInt(v1.w));

         }

         for (int i = 0; i < ogVertices.Count; i++)
         {
             Vector4 v;
             v = (Vector4)ogVertices[i];
             VerticesWithIndex.Add(v);
         }


         indexList.ToArray();

         ActionUnitList = parseXML.auList;
         //Debug.Log(ActionUnitList[0].Count());*/

    }

    // Update is called once per frame
    void Update() {

        //searchID = System.Array.IndexOf(indexList.ToArray(), VertexID);
        //vertices[searchID] = MoveVertexTo((Vector4)ogVertices[searchID], destination, t);

        //Debug.Log(ActionUnitList.Count);


        /*for(int j = 0; j < ActionUnitList[AuID].Count(); j++)
        {
            searchID = System.Array.IndexOf(indexList.ToArray(), ActionUnitList[AuID].GetId(j));
            vertices[searchID] = MoveVertexTo((Vector4)ogVertices[searchID], ActionUnitList[AuID].GetDestination(j), t);
            //Debug.Log(searchID);
        }*/

        //vertices[125] = MoveVertexTo((Vector4)ogVertices[125], new Vector3(0,0,0), t);

        for(int i = 0; i < indexToMove.Count; i++)
        {
            vertices[indexToMove[i]] = MoveVertexTo((Vector4)ogVertices[indexToMove[i]], new Vector3(0, 0, 0), t);
        }


        mesh.vertices = vertices;
        mesh.RecalculateBounds();

    }

    Vector4 copyCoordinate(Vector4 v)
    {
        Vector4 copy;
        copy = v;
        return copy;
    }

    /**
    * This is a test.
    */
    public Vector3 MoveVertexTo(Vector3 original, Vector3 goalPosition, float t)
    {
        
        Vector3 inital = original;


        inital.x = (1 - t) * inital.x + (t * goalPosition.x);
        inital.y = (1 - t) * inital.y + (t * goalPosition.y);
        inital.z = (1 - t) * inital.z + (t * goalPosition.z);

        return inital;

    }


    public void ReadString()
    {
        string path = "Assets/ArchivosTexto/test.txt";
        StreamReader reader = new StreamReader(path);

        //Saving all the text 
        string allAUs = reader.ReadToEnd();

        //Spliting all the text by how many AUs there are.
        Debug.Log("Cuantas unidades de acción hay? " + allAUs.Split('/').Length);
        //Debug.Log("La unidad de acción 2 es: " + allAUs.Split('/')[0]);


        /*string[] lines = System.IO.File.ReadAllLines(path);
        Debug.Log(lines[0].Split(',')[1]);*/

        reader.Close();
    }



}
