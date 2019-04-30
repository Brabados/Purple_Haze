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

    //AudioSouce for audio cues
    public GameObject cam;
    public List<AudioSource> Music;
    public AudioSource GameMusic;
    public AudioSource InterroMusic;

    void Start()
        {
            //RECODE: Demo set up will be needed to be recoded for main menu intergration
            InterviewToggle = false;
            PauseToggle = false;
            GUIToggle = true;
            Interview.enabled = false;
            GUI.enabled = true;
            PauseMenu.enabled = false;
            Cursor.visible = true;
        foreach(AudioSource n in cam.GetComponents<AudioSource>())
        {
            Music.Add(n);
        }
        GameMusic = Music[0];
        InterroMusic = Music[1];
        InterroMusic.mute = true;
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
                    View.GetComponent<Harrison.CamControl>().frozen = true;
                    Cursor.visible = true;
                    TimeController.GetComponent<Harrison.TimeControler>().AniStop();
                GameMusic.mute = true;
                InterroMusic.mute = false;
                }
                //Toggles the 1st person canvas if in interview
                else if (InterviewToggle)
                {
                    InterviewToggle = false;
                    GUIToggle = true;
                    GUI.enabled = true;
                    Interview.enabled = false;
                    View.GetComponent<Harrison.CamControl>().frozen = false;
                    Cursor.visible = false;
                    TimeController.GetComponent<Harrison.TimeControler>().AniPlay();
                GameMusic.mute = false;
                InterroMusic.mute = true;
            }
            }

        }
    }
