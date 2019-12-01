using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        SerializeData();
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
        if(File.Exists("sessions.csv"))
        {
            StreamWriter writer = File.AppendText("sessions.csv");
            writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.username + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));
            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("sessions.csv");
            writer.WriteLine("session_id;username;session_start;session_end");
            writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.username + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));
            writer.Close();
        }

        if(File.Exists("crashes.csv"))
        {
            StreamWriter writer = File.AppendText("crashes.csv");

            foreach(Crash crash in Instance.crashes)
            {
                writer.WriteLine(Instance.username + ";" + crash.crash_id.ToString("0000000000") + ";" + crash.position.x + ";" + crash.position.y + ";" + crash.position.z + ";" + crash.current_lap + ";" + crash.time.ToString("dd/MM/yyyy hh:mm:ss") + ";" + crash.session_id + ";" + crash.collision_obj_id);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("crashes.csv");

            writer.WriteLine("username;crash_id;position_x;position_y;position_z;current_lap;time;session_id;collision_obj_id");

            foreach (Crash crash in Instance.crashes)
            {
                writer.WriteLine(Instance.username + ";" + crash.crash_id.ToString("0000000000") + ";" + crash.position.x + ";" + crash.position.y + ";" + crash.position.z + ";" + crash.current_lap + ";" + crash.time.ToString("dd/MM/yyyy hh:mm:ss") + ";" + crash.session_id + ";" + crash.collision_obj_id);
            }
            writer.Close();
        }

        if (File.Exists("positions.csv"))
        {
            StreamWriter writer = File.AppendText("positions.csv");
            foreach (PositionData position in Instance.positions)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.username + ";" + position.time.ToString("dd/MM/yyyy hh:mm:ss") + ";" + position.position.x + ";" + position.position.y + ";" + position.position.z + ";" + position.velocity.x + ";" + position.velocity.y + ";" + position.velocity.z + ";" + position.rotation.x + ";" + position.rotation.y + ";" + position.rotation.z + ";" + position.rotation.w + ";" + position.current_lap);
            }
            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("positions.csv");

            writer.WriteLine("session_id;username;time;position_x;position_y;position_z;velocity_x;velocity_y;velocity_z;rotation_x;rotation_y;rotation_z;rotation_w;current_lap");

            foreach (PositionData position in Instance.positions)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.username + ";" + position.time.ToString("dd/MM/yyyy hh:mm:ss") + ";" + position.position.x + ";" + position.position.y + ";" + position.position.z + ";" + position.velocity.x + ";" + position.velocity.y + ";" + position.velocity.z + ";" + position.rotation.x + ";" + position.rotation.y + ";" + position.rotation.z + ";" + position.rotation.w + ";" + position.current_lap);
            }
            writer.Close();
        }

        if (File.Exists("laps.csv"))
        {
            StreamWriter writer = File.AppendText("laps.csv");
            foreach (LapData lap in Instance.laps)
            {
                writer.WriteLine(lap.lap_id.ToString() + ";" + Instance.sessionID.ToString("0000000000") + ";" + lap.username + ";" + "00/00/0000 " + lap.time.ToString("hh\\:mm\\:ss"));
            }
            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("laps.csv");

            writer.WriteLine("lap_id;session_id;username;time");

            foreach (LapData lap in Instance.laps)
            {
                writer.WriteLine(lap.lap_id.ToString() + ";" + Instance.sessionID.ToString("0000000000") + ";" + lap.username + ";" + "00/00/0000 " + lap.time.ToString("hh\\:mm\\:ss"));
            }
            writer.Close();
        }
    }
}
