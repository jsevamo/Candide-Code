using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNormal : MonoBehaviour {

    Renderer m_Renderer;
    Texture tex;
    Texture tex2;
    int choose;
    Renderer rend;

    // Use this for initialization
    void Start () {
        choose = 0;
        rend = GetComponent<Renderer>();
        
        tex = Resources.Load("normal") as Texture;
        tex2 = Resources.Load("normal2") as Texture;

        rend.material.EnableKeyword("_NORMALMAP");

        
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Jump"))
        {
            if(choose == 0)
            {
                rend.material.SetTexture("_BumpMap", tex);
                choose++;
            }
            else
            {
                rend.material.SetTexture("_BumpMap", tex2);
                choose = 0;
            }
        }
		
	}
}
