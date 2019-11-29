using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FlanCar : MonoBehaviour
{
    private float lastPosTime = 0f;

    private uint currentLap = 0;
    private DateTime lapStart;
    private bool halfLap = false;

    private void Awake()
    {
        lapStart = DateTime.Now;

        //TODO ORI: Send: 
        EventManager.SessionStarted();
    }

    private void Update()
    {
        if (Time.time - lastPosTime > 0.1f)
        {
            PositionData positionData = new PositionData
            {
                position = transform.position,
                velocity = GetComponent<Rigidbody>().velocity,
                rotation = transform.rotation,
                current_lap = currentLap
            };

            EventManager.OnPositionUpdate(positionData);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            CheckLapFinish();
        }
        else if (other.CompareTag("Check"))
        {
            halfLap = true;
        }
    }

    private void OnApplicationQuit()
    {
        //TODO ORI: Send: 
        EventManager.SessionFinished();
    }

    private void CheckLapFinish()
    {
        // Collide with half lap collider first, avoid multiple laps while being still
        if (halfLap)
        {
            halfLap = false;

            LapData lapData = new LapData
            {
                lap_id = currentLap,
                time = (DateTime.Now - lapStart)
            };
            EventManager.OnLap(lapData);

            lapStart = DateTime.Now;
        }
    }
}
