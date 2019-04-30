
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class Player : MonoBehaviour {

    //RawImage to have video clip played on
    public RawImage _Image;

    //Clip to play
    public VideoClip ToPlay;

    //Actual Video player
    private VideoPlayer _Player;

    //Video of clip to play
    private VideoSource _VSource;

    //Audio of clip to play
    private AudioSource _ASource;

    //Event to call on completion of a clip
    public event Action EndPlay;


    public IEnumerator playVideo(ClipStruct toplay)
    {
            //Places RawImage to enabled so clip can be watched
            _Image.enabled = true;

            //Sets sent clip as clip to play
            ToPlay = toplay.ToPlay;
            //Add VideoPlayer to the GameObject
            if (_Player == null)
            {
                _Player = gameObject.AddComponent<VideoPlayer>();

                //Add AudioSource
                _ASource = gameObject.AddComponent<AudioSource>();
            }

            //Disable Play on Awake for both Video and Audio
            _Player.playOnAwake = false;
            _ASource.playOnAwake = false;
            _ASource.Pause();

            //We want to play from video clip not from url

            _Player.source = VideoSource.VideoClip;

            // Vide clip from Url
            //videoPlayer.source = VideoSource.Url;
            //videoPlayer.url = ;


            //Set Audio Output to AudioSource
            _Player.audioOutputMode = VideoAudioOutputMode.AudioSource;

            //Assign the Audio from Video to AudioSource to be played
            _Player.EnableAudioTrack(0, true);
            _Player.SetTargetAudioSource(0, _ASource);

            //Set video To Play then prepare Audio to prevent Buffering
            _Player.clip = ToPlay;
            _Player.Prepare();

            //Wait until video is prepared
            WaitForSeconds waitTime = new WaitForSeconds(1);
            while (!_Player.isPrepared)
            {
          //      Debug.Log("Preparing Video");
                //Prepare/Wait for 5 sceonds only
                yield return waitTime;
                //Break out of the while loop after 5 seconds wait
                break;
            }

        //    Debug.Log("Done Preparing Video");

            //Assign the Texture from Video to RawImage to be displayed
            _Image.texture = _Player.texture;

            //Play Video
            _Player.Play();

            //Play Sound
            _ASource.Play();

  //          Debug.Log("Playing Video");

            //Allows for canceling out of a clip while playing
            while (_Player.isPlaying)
            {
//                Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)_Player.time));
                if (Input.anyKeyDown)
                {
                    _Player.Stop();
                }
                yield return null;
            }

    //        Debug.Log("Done Playing Video");

            //Stops playback at end of clip
            _Player.Stop();
            if (EndPlay != null)
            {
                // Add scroll view re-enable and raw image disable
      //          Debug.Log("Running end event");
                EndPlay();
            }
        //if the clip hasn't been watched before 

        List<GameObject> JustEnabled = new List<GameObject>();
        foreach (GameObject g in ActivatedOnClip.ToEnable)
        {
            if (g.GetComponent<ActivatedOnClip>().videoId == toplay.combineId)
            {
                g.SetActive(true);
                JustEnabled.Add(g);
            }
        }




        foreach (GameObject g in ActivatedOnClip.ToDisable)
        {
            ActivatedOnClip[] p = g.GetComponents<ActivatedOnClip>();
            foreach (ActivatedOnClip x in p)
            {
                if (x.videoId == toplay.combineId)
                {
                    foreach (GameObject n in JustEnabled)
                    {
                        if (n != g)
                        {
                            g.SetActive(false);
                        }
                    }
                }
            }

        }

        JustEnabled.Clear();
 
            
            // Sets watched to true to avoid retriggers
            toplay.beenPlayed = true;
    }
}
