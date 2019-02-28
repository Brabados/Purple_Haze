using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class MenuManager : MonoBehaviour
    {
        //Camera Controller game object
        public GameObject View;
        public GameObject TimeController;

        //Verious diffrent canvases to put on display port.
        public Canvas Interview;
        public Canvas PauseMenu;
        public Canvas GUI;

        //Bools to define what GUI system to render.
        public bool InterviewToggle;
        public bool PauseToggle;
        public bool GUIToggle;

        void Start()
        {
            //RECODE: Demo set up will be needed to be recoded for main menu intergration
            InterviewToggle = false;
            PauseToggle = false;
            GUIToggle = true;
            Interview.enabled = false;
            GUI.enabled = true;
            PauseMenu.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            //Controls switching between intrview and 1st person gameplay
            if (Input.GetKeyDown(KeyCode.R) && PauseToggle == false)
            {
                //Toggles the Interview canvas if in 1st person
                if (GUIToggle)
                {
                    InterviewToggle = true;
                    GUIToggle = false;
                    GUI.enabled = false;
                    Interview.enabled = true;
                    View.GetComponent<Harrison.CamControl>().enabled = false;
                    Cursor.visible = true;
                    TimeController.GetComponent<Harrison.TimeControler>().AniStop();
                }
                //Toggles the 1st person canvas if in interview
                else if (InterviewToggle)
                {
                    InterviewToggle = false;
                    GUIToggle = true;
                    GUI.enabled = true;
                    Interview.enabled = false;
                    View.GetComponent<Harrison.CamControl>().enabled = true;
                    Cursor.visible = false;
                    TimeController.GetComponent<Harrison.TimeControler>().AniPlay();
                }
            }

        }
    }
