using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        private float lastPosTime = 0f;

        private int currentLap = 0;
        private float lapStart = 0f;
        private bool halfLap = false;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();

            lapStart = Time.time;
        }

        private void Update()
        {
            if (Time.timeSinceLevelLoad - lastPosTime > 0.1f)
            {
                float timeStamp = Time.time;
                Vector3 position = transform.position;
                Vector3 velocity = GetComponent<Rigidbody>().velocity;
                Quaternion rotation = transform.rotation;
            }

        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
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
}
