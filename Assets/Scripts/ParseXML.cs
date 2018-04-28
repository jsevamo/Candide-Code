using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class ActionUnit
{
    [SerializeField]
    List<int> idList;
    [SerializeField]
    List<Vector3> destinationList;

    public ActionUnit()
    {
        idList = new List<int>();
        destinationList = new List<Vector3>();
    }


    public ActionUnit(List<int> newIDList, List<Vector3> newDestinationList)
    {
        idList = newIDList;
        destinationList = newDestinationList;
    }

    public int GetId(int index)
    {
        return idList[index];
    }

    public Vector3 GetDestination(int index)
    {
        return destinationList[index];
    }

    public void SetId(int index, int pID)
    {
        idList[index] = pID;
    }

    public void SetDestination(int index, Vector3 pVertex)
    {
        destinationList[index] = pVertex;
    }

    public void Add(int pID, Vector3 pVertex )
    {
        idList.Add(pID);
        destinationList.Add(pVertex);
    }

    public int Count()
    {
        int count;
        count = idList.Count();
        return count;
    }
}



public class ParseXML {

    // Atributos Candide
    public List<Vector3> candideVertexList = new List<Vector3>();
    public List<int> candideVertexID = new List<int>();
    public List<ActionUnit> auList = new List<ActionUnit>();

    // Otros atributos
    List<Dictionary<string, string>> candideVertexDic;
    Dictionary<string, string> dictionary;
    public List<Vector3> destinationList = new List<Vector3>();
    public List<int> ids = new List<int>();
    int amountOfVertex;

    //---------------------------------------------------------------------

    public List<string> ActionUnitNames = new List<string>();


    // Use this for initialization
    public void ParceFile () {

        amountOfVertex = 0;

        candideVertexDic = parseVertices();
        
        for(int i=0; i<amountOfVertex; i++)
        {
            Vector3 v;
            int id;
            v.x = float.Parse(candideVertexDic[i]["x"]);
            v.y = float.Parse(candideVertexDic[i]["y"]);
            v.z = float.Parse(candideVertexDic[i]["z"]);
            id = int.Parse(candideVertexDic[i]["ID"]);

            candideVertexList.Add(v);
            candideVertexID.Add(id);
        }

        parseAUs();


    }

    public List<Dictionary<string, string>> parseVertices()
    {
        TextAsset txtXmlAsset = Resources.Load<TextAsset>("listaVerticesCandide");
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
        TextAsset txtXmlAsset = Resources.Load<TextAsset>("listaVerticesCandide");
        var doc = XDocument.Parse(txtXmlAsset.text);

        var allDict = doc.Element("document").Elements("Au");
        
        foreach (var oneDict in allDict)
        {
            var nameOfAus = oneDict.Elements("Name");

            for (int i = 0; i < nameOfAus.Count(); i++)
            {
                var twoStringsVertex = nameOfAus.ElementAt(i).Elements("N");
                XElement element1 = twoStringsVertex.ElementAt(0);
                string first = element1.ToString().Replace("<N>", "").Replace("</N>", "");
                ActionUnitNames.Add(first);
            }
        }


        foreach (var oneDict in allDict)
        { 
            ActionUnit newActionUnit = new ActionUnit();
            var auCount = oneDict.Elements("Vertex");

            for (int i = 0; i < auCount.Count(); i++)
            {

                var twoStringsVertex = auCount.ElementAt(i).Elements("v");
                var twoStringsVertex2 = auCount.ElementAt(i).Elements("id");

                XElement element1 = twoStringsVertex.ElementAt(0);
                XElement element2 = twoStringsVertex.ElementAt(1);
                XElement element3 = twoStringsVertex.ElementAt(2);
                XElement element4 = twoStringsVertex2.ElementAt(0);

                string first = element1.ToString().Replace("<v>", "").Replace("</v>", "");
                string second = element2.ToString().Replace("<v>", "").Replace("</v>", "");
                string third = element3.ToString().Replace("<v>", "").Replace("</v>", "");
                string fourth = element4.ToString().Replace("<id>", "").Replace("</id>", "");

                Vector3 moveTo = new Vector3(float.Parse(first), float.Parse(second), float.Parse(third));

                newActionUnit.Add(int.Parse(fourth),moveTo);

            }

            auList.Add(newActionUnit);


            //var nameOfAus = oneDict.Elements("Name");
            //Debug.Log(nameOfAus.Count());

            /*for (int i = 0; i < nameOfAus.Count(); i++)
            {
                var twoStringsVertex = auCount.ElementAt(i).Elements("N");
                XElement element1 = twoStringsVertex.ElementAt(0);
                string name = element1.ToString().Replace("<N>", "").Replace("</N>", "");
                ActionUnitNames.Add(name);
            }*/
        }

        //Debug.Log(auList[4].GetId(0));


    }
}
