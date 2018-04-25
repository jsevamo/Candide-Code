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
    [SerializeField]
    int AuID;

    public string AuName;

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

    public Text ActionUnitNameText;

    List<UnityVertexMatch> matchList = new List<UnityVertexMatch>();
    ParseXML parseXML = new ParseXML();

    //-----------------RayCast-------------------------------

    RaycastHit hit;





    // Use this for initialization
    void Start() {

        t = 0;
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


        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.back));
        //Debug.Log(ray);

       


        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider);

            if(hit.collider != null)
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green, 20, true);
                Debug.Log("It hits");
            }
                
            else
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 20, true);
                Debug.Log("It doesnt hit");
            }
                
        }




        //fixTransform();

    }

    // Update is called once per frame
    void Update() {

        


        for (int i = 0; i < ActionUnitList[AuID].Count(); i++)
        {
            for(int j = 0; j < matchList[ActionUnitList[AuID].GetId(i)].Count(); j++)
            {

                vertices[matchList[ActionUnitList[AuID].GetId(i)].getID(j)] =
                    MoveVertexTo(ogVertices[matchList[ActionUnitList[AuID].GetId(i)].getID(j)], ActionUnitList[AuID].GetDestination(i), t);

            }
            
        }

        ActionUnitNameText.text = parseXML.ActionUnitNames[AuID];

        /**
         * Important, use this to change from local to world to each vertex
         * 
         * Vector3 worldPt = transform.TransformPoint(vertices[0]);
            Debug.Log(worldPt);

            To move a vertex back, for raycast:

        vertices[0] = MoveVertexTo(ogVertices[0], new Vector3(ogVertices[0].x, ogVertices[0].y, ogVertices[0].z - 200), t);
         * */

        /*Vector3 worldPt = transform.TransformPoint(vertices[0]);
        Debug.Log(worldPt);*/

        Vector3 forward = transform.TransformDirection(Vector3.back);
        Debug.DrawRay(transform.position, forward, Color.green, 20, true);

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

    public void fixTransform()
    {
        Vector3 origin = new Vector3(0, 0, 0);

        Matrix4x4 candideMatrix;
        candideMatrix = transform.localToWorldMatrix;


        Matrix4x4 OriginalVertexMatrix = new Matrix4x4();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < vertices.Length; j++)
            {
                if (i == 0)
                    OriginalVertexMatrix.SetColumn(i, new Vector4(1, 0, 0, 0));
                else if (i == 1)
                    OriginalVertexMatrix.SetColumn(i, new Vector4(0, 1, 0, 0));
                else if (i == 2)
                    OriginalVertexMatrix.SetColumn(i, new Vector4(0, 0, 1, 0));
                else if (i == 3)
                    OriginalVertexMatrix.SetColumn(i, new Vector4(vertices[j].x, vertices[j].y, vertices[j].z, t));
            }
        }

        Vector4[] Matrix;
        Matrix = new Vector4[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Matrix[i] = new Vector4(vertices[0].x, vertices[0].y, vertices[0].z, 0);
        }

        //Debug.Log(Matrix[0]);

        Matrix4x4 Translation = new Matrix4x4();

        Translation.SetRow(0, new Vector4(1, 0, 0, transform.position.x));
        Translation.SetRow(1, new Vector4(0, 1, 0, transform.position.y));
        Translation.SetRow(2, new Vector4(0, 0, 1, transform.position.z));
        Translation.SetRow(3, new Vector4(0, 0, 0, 1));

        Debug.Log(Translation.MultiplyPoint(Matrix[0]));

        for(int i = 0; i < Matrix.Length; i++)
        {
            Translation.MultiplyPoint(Matrix[i]);
        }

        Debug.Log(Translation);


        Matrix4x4 Scale = new Matrix4x4();

        Scale.SetRow(0, new Vector4(transform.localScale.x, 0, 0, 0));
        Scale.SetRow(1, new Vector4(0, transform.localScale.y, 0, 0));
        Scale.SetRow(2, new Vector4(0, 0, transform.localScale.z, 0));
        Scale.SetRow(3, new Vector4(0, 0, 0, 1));

        Debug.Log(Scale.MultiplyPoint(Matrix[0]));

        for (int i = 0; i < Matrix.Length; i++)
        {
            Scale.MultiplyPoint(Matrix[i]);
        }











    }


}
