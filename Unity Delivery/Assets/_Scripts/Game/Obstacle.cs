using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public static uint barrel_counter = 0;
    public uint barrel_uid = 0;

    // Use this for initialization
    void Start () 
    {
        barrel_uid = barrel_counter;
        barrel_counter += 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
