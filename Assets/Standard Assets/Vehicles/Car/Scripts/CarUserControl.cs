using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private Vector2 input;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }
        
        public void OnInput(Vector2 joystick)
        {
            input = joystick;
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = input.x;
            float v = input.y;
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
