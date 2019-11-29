using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PositionData
{
    public string session_id; //This field is autocompleted, do not worry
    public DateTime time = DateTime.Now;
    public Vector3 position;
    public Vector3 velocity;
    public Quaternion rotation;
    public uint current_lap;
}
