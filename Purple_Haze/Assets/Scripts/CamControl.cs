#define USINGRIGIDBODY

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
        private Rigidbody camBody;
        
        // camera's new rotation
        private Vector2 camRotation;
        
        // camera base speed and speed boost multiplier
        public float camSpeed;
        public float camBoostSpeed;
        public float camMaxSpeed;
        [Range(0,1)] public float camBreakSpeed = 0.03f;
        
        // Bool so speed boost isn't applied multiple times
        private bool boostActive;

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

        private void Awake()
        {
            camBody = GetComponent<Rigidbody>();
        }

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
            
            #if USINGRIGIDBODY // Rigidbody based movement

                Vector3 forwardForce = camera.transform.forward * inputYValue;
                Vector3 horizontalForce = camera.transform.right * inputXValue;
                Vector3 verticalForce = camera.transform.up * inputZValue;
            
                Vector3 _force = forwardForce + horizontalForce + verticalForce;
                
                _force *= (camSpeed * 1000) * Time.deltaTime;
                
                camBody.AddForce(_force);

                camBody.velocity = Vector3.Lerp(camBody.velocity, Vector3.zero, camBreakSpeed);                
                camBody.velocity = new Vector3(
                    Mathf.Clamp(camBody.velocity.x, -camMaxSpeed, camMaxSpeed),
                    Mathf.Clamp(camBody.velocity.y, -camMaxSpeed, camMaxSpeed), 
                    Mathf.Clamp(camBody.velocity.z, -camMaxSpeed, camMaxSpeed));
                
            #else // Old transform based movement
                
                // right/left input * speed
                camera.transform.position += camera.transform.right * camSpeed * inputXValue * Time.deltaTime;
                // forward/back input * speed
                camera.transform.position += camera.transform.forward * camSpeed * inputYValue * Time.deltaTime;
                // up/down input * speed
                camera.transform.position += Vector3.up * camSpeed * inputZValue * Time.deltaTime;
                
            #endif
        }

        public void ApplySpeedBoost()
        {
            // if game paused, take no input
            if (Time.timeScale <= 0) return;
            
            #if USINGRIGIDBODY
            camMaxSpeed *= camBoostSpeed;
            #endif
            
            // add boost
            camSpeed *= camBoostSpeed;
            boostActive = true;
        }

        public void RemoveSpeedBoost()
        {
            // if game paused, take no input
            if (Time.timeScale <= 0) return;
            
            #if USINGRIGIDBODY
            camMaxSpeed /= camBoostSpeed;
            #endif
            
            // remove boost
            camSpeed /= camBoostSpeed;
            boostActive = false;
        }
    }

}

