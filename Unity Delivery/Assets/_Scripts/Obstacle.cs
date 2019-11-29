using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        //TODO ORI: Send: 
        //playerusername
        //crash_id
        //position
        //current_lap
        //time
        //session_id
        //collision_obj_id
        int i = 0;
    }

}
