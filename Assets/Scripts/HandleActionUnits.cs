/* This code handles everything that has got to do with the Action Units of the candide face*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;



/*
 * First, we create a simple class called ActionUnit. Each ActionUnit object has an ID and an influence.
 * ****ID = There are 28 basic Action Units. 
 * 
 * ****influence = Each Action unit corresponds to a group to muscles in the face that move a certain way. 
 * Since we are using a 3D mesh, we must instead move group of vertices. The influence variable controls how 
 * these group of vertices move. They are held within values from 0 to 1. If the value is 0, the vertex group
 * doesn't move. If the value is 1, the vertex group moves to it's maximun displacent value. Everything in
 * between will move the vertex group to a value in the middle.
 * */

[System.Serializable]
public class AU
{
    //This is the comment of the class of action units

    public string ID;
    public float influence;

    public AU(string newId, float newInfluence)
    {
        ID = newId;
        influence = newInfluence;
    }

    public float getInfluence()
    {
        return influence;
    }

}



public class HandleActionUnits : MonoBehaviour {

    public GameObject obj;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    int blendShapeCount;
    //public float au1;
    //public float au2;


    public List<AU> ActionUnitsList = new List<AU>();


    // Use this for initialization
    void Start () {


        skinnedMeshRenderer = obj.gameObject.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = obj.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;


        blendShapeCount = skinnedMesh.blendShapeCount;

        //Debug.Log(blendShapeCount);

        for (int i = 0; i < blendShapeCount; i++)
        {
            ActionUnitsList.Add(new AU("AU" + (i+1).ToString(), 0f));
        }

    }
	
	// Update is called once per frame
	void Update () {

        foreach (AU AU in ActionUnitsList)
        {

           if(AU.getInfluence()<0)
                AU.influence = 0;
            

           if(AU.getInfluence()>1)
                AU.influence = 1;
         }

        for (int i = 0; i < ActionUnitsList.Count; i++)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(i, ActionUnitsList[i].getInfluence() * 100);

        }

        
    }


    
    public void ShowArrayProperty(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list);

        EditorGUI.indentLevel += 1;
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i),
            new GUIContent("Bla" + (i + 1).ToString()));
        }
        EditorGUI.indentLevel -= 1;
    }


}



