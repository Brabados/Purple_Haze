using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Harrison
{
    
    public class CamControl : MonoBehaviour
    {
        // player's camera
        public GameObject camera;
        
        // camera's new rotation
        private Vector2 camRotation;
        
        // camera base speed and speed boost multiplier
        public float camSpeed;
        public float camBoostSpeed;
        
        // Bool so speed boost isnt applied multiple times
        private bool boostActive = false;

        // X and Y axis sensitivity/camera clamp angle values
        public float sensitivityX, sensitivityY, clampAngle;
        
        // input strings set in inspector to setup input values
        public string inputX, inputY, mouseX, mouseY, inputZ, speedBoost ;

        // floats bound to their axis's value
        private float inputXValue { get { return Input.GetAxis(inputX); } }
        private float inputYValue { get { return Input.GetAxis(inputY); } }
        private float mouseXValue { get { return Input.GetAxis(mouseX); } }
        private float mouseYValue { get { return Input.GetAxis(mouseY); } }
        private float inputZValue { get { return Input.GetAxis(inputZ); } }
        private float speedBoostValue { get { return Input.GetAxis(speedBoost); } }
        
        private void Update()
        {
            // run cam movement
            CameraMovement();
            
            // activate speed boost on button press
            if (speedBoostValue > 0 && !boostActive) ApplySpeedBoost();
            
            // deactivate speed boost on button release
            if (speedBoostValue <= 0 && boostActive) RemoveSpeedBoost();

        }

        public void CameraMovement()
        {    
            // if game paused, take no input
            if (Time.timeScale <= 0) return;
            
            // setting camera's new rotation to be equal to the mouse input * our sensitivity 
            camRotation.x += mouseXValue * sensitivityX * 15 * Time.deltaTime;
            camRotation.y += mouseYValue * sensitivityY * 15 * Time.deltaTime;
        
            // clamping the camera's Y axis so we cannot over rotate and go upside down
            camRotation.y = Mathf.Clamp (camRotation.y, -clampAngle, clampAngle);
        
            // applying the rotation to the camera
            camera.transform.rotation = Quaternion.AngleAxis (camRotation.x, Vector3.up);
            camera.transform.rotation *= Quaternion.AngleAxis (camRotation.y, Vector3.left);
        
            // right/left input * speed
            camera.transform.position += camera.transform.right * camSpeed * inputXValue * Time.deltaTime;
            // forward/back input * speed
            camera.transform.position += camera.transform.forward * camSpeed * inputYValue * Time.deltaTime;
            // up/down input * speed
            camera.transform.position += Vector3.up * camSpeed * inputZValue * Time.deltaTime;
            
        }

        public void ApplySpeedBoost()
        {
            // if game paused, take no input
            if (Time.timeScale <= 0) return;
            
            // add boost
            camSpeed *= camBoostSpeed;
            boostActive = true;
        }

        public void RemoveSpeedBoost()
        {
            // if game paused, take no input
            if (Time.timeScale <= 0) return;
            
            // remove boost
            camSpeed /= camBoostSpeed;
            boostActive = false;
        }
    }

}

