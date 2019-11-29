using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager Instance;

    //SessionData
    private DateTime sessionStartTime;
    private DateTime sessionEndTime;
    private uint sessionID = 0u;
    private string username = "FlanStudio";

    private List<Crash> crashes = new List<Crash>();
    private List<PositionData> positions = new List<PositionData>();
    private List<LapData> laps = new List<LapData>();

    private void Awake()
    {
        Instance = this;
    }

    public static uint GetRandomUUID()
    {
        return BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0);
    }
    public static void SessionStarted()
    {
        Instance.sessionStartTime = DateTime.Now;
        Instance.sessionID = GetRandomUUID();
    }

    public static void SessionFinished()
    {
        Instance.sessionEndTime = DateTime.Now;
    }

    public static void OnCrash(Crash crash)
    {
        crash.session_id = Instance.sessionID.ToString();
        Instance.crashes.Add(crash);
    }

    public static void OnPositionUpdate(PositionData position)
    {
        position.session_id = Instance.sessionID.ToString();
        Instance.positions.Add(position);
    }
    public static void OnLap(LapData lap)
    {
        lap.session_id = Instance.sessionID.ToString();
        Instance.laps.Add(lap);
    }

    public static void SerializeData()
    {

    }


}
