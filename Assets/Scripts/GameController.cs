using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Camera FrontCamera;
    public Camera SideCamera;
    int i = 0;
	
	// Update is called once per frame
	void Update () {

        if(i == 0)
        {
            FrontCamera.enabled = true;
            SideCamera.enabled = false;
        }
        else
        {
            FrontCamera.enabled = false;
            SideCamera.enabled = true;
        }

        if(Input.GetButtonDown("Jump"))
        {
            if(i == 0)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
        }
		
	}
}
