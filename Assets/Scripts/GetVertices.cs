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
    public int CandideID;
    public List<int> UnityVertex = null;

    public UnityVertexMatch(){
        CandideID = -1;
        UnityVertex = new List<int>();
    }

    public int Count()
    {
        return UnityVertex.Count;
    }

    public int getID(int a)
    {
        return UnityVertex[a];
    }

    
}


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
    //ArrayList ogVertices = new ArrayList();
    List<Vector3> ogVertices = new List<Vector3>();
    List<int> ogVerticesID = new List<int>();


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



    public List<ActionUnit> ActionUnitList = new List<ActionUnit>();


    //------------------------------------------------



    List<UnityVertexMatch> matchList = new List<UnityVertexMatch>();


    // Use this for initialization
    void Start() {


        AuID = 0;

        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        


        for (int i = 0; i < vertices.Length; i++)
        {
            //Vector4 v = vertices[i];
            float x = vertices[i].x;
            float y = vertices[i].y;
            float z = vertices[i].z;

            Vector3 v;
            v.x = x;
            v.y = y;
            v.z = z;
            

            ogVertices.Add(copyCoordinate(v));

        }

        

        GameObject gameController = GameObject.Find("GameController");
        ParseXML parseXML = new ParseXML();

        



        parseXML.ParceFile();


        ActionUnitList = parseXML.auList;



        for(int j = 0; j < parseXML.candideVertexList.Count; j++)
        {

            matchList.Add(new UnityVertexMatch());
            float _x = parseXML.candideVertexList[j].x;
            float _y = parseXML.candideVertexList[j].y;
            float _z = parseXML.candideVertexList[j].z;

            Vector3 testing = new Vector3(_x, _y, _z);


            for (int i = 0; i < vertices.Length; i++)
            {
                if (testing.x == vertices[i].x && testing.y == vertices[i].y && testing.z == vertices[i].z)
                {
                    
                    matchList[j].UnityVertex.Add(i);
                }
            }
        }

        




    }

    // Update is called once per frame
    void Update() {


       Debug.Log(matchList.Count);


        for (int i = 0; i < ActionUnitList[AuID].Count(); i++)
        {
            for(int j = 0; j < matchList[i].Count(); j++)
            {

                vertices[matchList[i].getID(j)] = MoveVertexTo((Vector4)ogVertices[matchList[i].getID(j)], ActionUnitList[AuID].GetDestination(i), t);

            }
            
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


}
