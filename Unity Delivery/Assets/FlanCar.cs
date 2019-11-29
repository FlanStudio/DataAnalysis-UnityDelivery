using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlanCar : MonoBehaviour
{
    private float lastPosTime = 0f;

    private int currentLap = 0;
    private float lapStart = 0f;
    private bool halfLap = false;

    private void Awake()
    {
        lapStart = Time.time;

        //TODO ORI: Send: 
        //session_id
        //username
        //session_start
    }

    private void Update()
    {
        if (Time.time - lastPosTime > 0.1f)
        {
            lastPosTime = Time.time;
            //PositionData positionData = new PositionData();
            float timeStamp = Time.time;
            Vector3 position = transform.position;
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Quaternion rotation = transform.rotation;
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
        //session Ends
    }

    private void CheckLapFinish()
    {
        // Collide with half lap collider first, avoid multiple laps while being still
        if (halfLap)
        {
            halfLap = false;
            uint id = 0u;
            float time = Time.time - lapStart;
            lapStart = Time.time;
        }
    }
}
