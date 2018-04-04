using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ActionUnitCode
{
    private List<int> idList;
    private List<Vector3> destinationList;

    public ActionUnitCode()
    {
        idList = new List<int>();
        destinationList = new List<Vector3>();
    }


    public ActionUnitCode(List<int> newIDList, List<Vector3> newDestinationList)
    {
        idList = newIDList;
        destinationList = newDestinationList;
    }

    public int GetId(int index)
    {
        return idList[index];
    }

    public Vector3 GetVertex(int index)
    {
        return destinationList[index];
    }

    public void SetId(int index, int pID)
    {
        idList[index] = pID;
    }

    public void SetVertex(int index, Vector3 pVertex)
    {
        destinationList[index] = pVertex;
    }

    public void Add(int pID, Vector3 pVertex )
    {
        idList.Add(pID);
        destinationList.Add(pVertex);
    }
}



public class ParseXML : MonoBehaviour {

    List<Dictionary<string, string>> candideVertexDic;
    Dictionary<string, string> dictionary;
    public List<Vector4> candideVertexList = new List<Vector4>();
    int amountOfVertex;

    //---------------------------------------------------------------------


    public List<Vector3> destinationList = new List<Vector3>();
    public List<int> ids = new List<int>();

    public List<List<int>> idList = new List<List<int>>();


    public List<ActionUnitCode> ActionUnitList = new List<ActionUnitCode>();
    int iterator = 0;

    // Use this for initialization
    void Start () {

        amountOfVertex = 0;

        candideVertexDic = parseVertices();
        //dictionary = candideVertexDic[1];

        /*AusDic = parseAUs();
        dictionary = AusDic[0];


        Debug.Log(dictionary["x"]);
        Debug.Log(dictionary["y"]);
        Debug.Log(dictionary["z"]);
        Debug.Log(dictionary["ID"]);*/

        /*Vector3 meat = new Vector3(float.Parse(dic["riddle"]), float.Parse(dic["ans"]), float.Parse(dic["test"]));
        Debug.Log(meat.y);*/

        for(int i=0; i<amountOfVertex; i++)
        {
            Vector4 v;
            v.x = float.Parse(candideVertexDic[i]["x"]);
            v.y = float.Parse(candideVertexDic[i]["y"]);
            v.z = float.Parse(candideVertexDic[i]["z"]);
            v.w = float.Parse(candideVertexDic[i]["ID"]);

            candideVertexList.Add(v);
        }

        parseAUs();


    }

    public List<Dictionary<string, string>> parseVertices()
    {
        TextAsset txtXmlAsset = Resources.Load<TextAsset>("listaVertices");
        var doc = XDocument.Parse(txtXmlAsset.text);

        var allDict = doc.Element("document").Elements("Mesh").Elements("Vertex");
        List<Dictionary<string, string>> allTextDic = new List<Dictionary<string, string>>();

        foreach (var oneDict in allDict)
        {
            var twoStrings = oneDict.Elements("v");
            var twoStrings2 = oneDict.Elements("id");

            XElement element1 = twoStrings.ElementAt(0);
            XElement element2 = twoStrings.ElementAt(1);
            XElement element3 = twoStrings.ElementAt(2);
            XElement element4 = twoStrings2.ElementAt(0);

            string first = element1.ToString().Replace("<v>", "").Replace("</v>", "");
            string second = element2.ToString().Replace("<v>", "").Replace("</v>", "");
            string third = element3.ToString().Replace("<v>", "").Replace("</v>", "");
            string fourth = element4.ToString().Replace("<id>", "").Replace("</id>", "");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("x", first);
            dic.Add("y", second);
            dic.Add("z", third);
            dic.Add("ID", fourth);

            allTextDic.Add(dic);
            amountOfVertex++;
        }

        return allTextDic;

    }

    public void parseAUs()
    {
        TextAsset txtXmlAsset = Resources.Load<TextAsset>("listaVertices");
        var doc = XDocument.Parse(txtXmlAsset.text);

        var allDict = doc.Element("document").Elements("Au");
        List<Dictionary<string, string>> allTextDic = new List<Dictionary<string, string>>();
        List<ActionUnitCode> auList = new List<ActionUnitCode>();

        foreach (var oneDict in allDict)
        {
            ActionUnitCode newActionUnitCode = new ActionUnitCode();
            var auCount = oneDict.Elements("Vertex");
            //Debug.Log(auCount.ElementAt(0));


            //Debug.Log(auCount.Count());

            for (int i = 0; i < auCount.Count(); i++)
            {
                /*var a = twoStrings.ElementAt(i).Elements("v");
                Debug.Log(a.ElementAt(0));*/

                var twoStringsVertex = auCount.ElementAt(i).Elements("v");
                //Debug.Log(twoStringsVertex.ElementAt(0));

                var twoStringsVertex2 = auCount.ElementAt(i).Elements("id");

                XElement element1 = twoStringsVertex.ElementAt(0);
                XElement element2 = twoStringsVertex.ElementAt(1);
                XElement element3 = twoStringsVertex.ElementAt(2);
                XElement element4 = twoStringsVertex2.ElementAt(0);

                string first = element1.ToString().Replace("<v>", "").Replace("</v>", "");
                string second = element2.ToString().Replace("<v>", "").Replace("</v>", "");
                string third = element3.ToString().Replace("<v>", "").Replace("</v>", "");
                string fourth = element4.ToString().Replace("<id>", "").Replace("</id>", "");

                //Debug.Log(first);

                Vector3 moveTo = new Vector3(float.Parse(first), float.Parse(second), float.Parse(third));

                newActionUnitCode.Add(int.Parse(fourth),moveTo);
                //Debug.Log(moveTo)

            }

            auList.Add(newActionUnitCode);
            idList.Add(ids);


            /*while(iterator < auCount.Count())
            {
                

                iterator++;

                if(iterator==auCount.Count())
                {
                    iterator = 0;
                }
            }*/

            //Debug.Log(auCount.Count());
            //ActionUnitList.Add(new ActionUnitCode(idList, destinationList));


            /*var twoStringsVertex = twoStrings.ElementAt(0).Elements("v");
            //Debug.Log(twoStringsVertex.ElementAt(0));

            var twoStringsVertex2 = twoStrings.Elements("id");

            XElement element1 = twoStringsVertex.ElementAt(0);
            XElement element2 = twoStringsVertex.ElementAt(1);
            XElement element3 = twoStringsVertex.ElementAt(2);
            XElement element4 = twoStringsVertex2.ElementAt(0);

            string first = element1.ToString().Replace("<v>", "").Replace("</v>", "");
            string second = element2.ToString().Replace("<v>", "").Replace("</v>", "");
            string third = element3.ToString().Replace("<v>", "").Replace("</v>", "");
            string fourth = element4.ToString().Replace("<id>", "").Replace("</id>", "");*/

            //Debug.Log(first);


            //Vector3 moveTo = new Vector3(float.Parse(first), float.Parse(second), float.Parse(third));

            /*destinationList.Add(moveTo);
            idList.Add(int.Parse(fourth));*/

        }


        /*Debug.Log(destinationList[1] + " " + idList[1]);
        Debug.Log(destinationList.Count);*/

        for (int i = 0; i < destinationList.Count; i++)
        {
            //Debug.Log(destinationList[i]);
            //Debug.Log(ids[i]);
        }

        //Debug.Log(auList[0].GetVertex(0));
        Debug.Log(auList[4].GetId(0));

        //Debug.Log(ActionUnitList[1].idList[i]);



    }

    // Update is called once per frame
    void Update () {
		
	}
}
