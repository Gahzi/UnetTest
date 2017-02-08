using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public int a = 0;

	// Use this for initialization
	void Start () {
	    if (isLocalPlayer && isClient)
	    {
	        a = 1;
	    }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
