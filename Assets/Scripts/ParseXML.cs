using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;



public class ParseXML : MonoBehaviour {

    List<Dictionary<string, string>> candideVertexDic;
    Dictionary<string, string> dictionary;
    public List<Vector4> candideVertexList = new List<Vector4>();
    int amountOfVertex;

    //---------------------------------------------------------------------


    public List<Vector3> destinationList = new List<Vector3>();
    public List<int> idList = new List<int>();

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


        foreach (var oneDict in allDict)
        {
            var twoStrings = oneDict.Elements("Vertex");
            //Debug.Log(twoStrings.ElementAt(0));

            Debug.Log(twoStrings.Count());

            var twoStringsVertex = twoStrings.ElementAt(0).Elements("v");
            //Debug.Log(twoStringsVertex.ElementAt(0));

            var twoStringsVertex2 = twoStrings.Elements("id");

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

            /*destinationList.Add(moveTo);
            idList.Add(int.Parse(fourth));*/

        }


        /*Debug.Log(destinationList[1] + " " + idList[1]);
        Debug.Log(destinationList.Count);*/

    }

    // Update is called once per frame
    void Update () {
		
	}
}
