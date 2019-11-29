using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crash
{
    public string username = "FlanStudio";
    public uint crash_id = EventManager.GetRandomUUID();
    public Vector3 position;
    public uint current_lap;
    public DateTime time = DateTime.Now;
    public string session_id; //This field is autocompleted, do not worry
    public uint collision_obj_id;  
}
