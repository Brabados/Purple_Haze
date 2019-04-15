using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Harrison
{
    public class VideoClipManager : MonoBehaviour
    {

        public static VideoClipManager VCM;

        public enum ManagerState { combining, normal }
        public ManagerState state = ManagerState.normal;
        
        public List<VideoClip> clips;
        //List of Clues, used for spawning buttons
        public List<ClipStruct> FoundCluesLisCS = new List<ClipStruct>();
        
        public Player MediaPlayer;
        public ScrollRect Can;
        
        //Used to hold the insantiated button for use
        public Button placeholder;
        //Prefab used in button creation
        public Button BaseButt;
        
        private void Awake()
        {
            VCM = this;
            
            // Gets the media player and the canvas obj 
            MediaPlayer = GetComponent<Player>();
            Can = FindObjectOfType<ScrollRect>();
            
            // Adds the create new button function to a global event to be called when cluesa are found 
            GlobleEvents.OnClueActivate += ADD;

            MediaPlayer.EndPlay += PurgeList;
        }
        
        public void ADD(ClipStruct adder)
        {
            //Constructs button layout from found clipstructs. With the addition of only adding it if the clue hasn't already been found
            if (!FoundCluesLisCS.Contains(adder))
            {
                FoundCluesLisCS.Add(adder);
                placeholder = Instantiate(BaseButt);
                placeholder.transform.parent = Can.viewport.GetChild(0).transform;
                
                VideoClip v = placeholder.gameObject.AddComponent<VideoClip>();
                v.myClip = adder;
                
                placeholder.GetComponent<RectTransform>().localPosition = new Vector3(90, ((FoundCluesLisCS.Count - 1) * -40) - 20, 0);
                placeholder.GetComponentInChildren<Text>().text = adder.Question;
                
                placeholder.onClick.AddListener(delegate { v.ToggleButton(); Can.gameObject.SetActive(false); });
            }
      
        }

        public void test(ClipStruct MyClip)
        {
            //Hands the clip to the player to run in a coroutine
            StartCoroutine(MediaPlayer.playVideo(MyClip));
            MediaPlayer.EndPlay += Redraw;
        }
        
        public void Redraw()
        {
            //resets the Investigation canvas
            Can.gameObject.SetActive(true);
            MediaPlayer.EndPlay -= Redraw;
        }
        
        public void FindVideo()
        {
            switch (state)
            {
                case ManagerState.normal:

                    // Play Video
                    test(clips[0].myClip);
                    
                    break;
                
                case ManagerState.combining:

                    // Check list + play corresponding video if found
                    List<int> toPlayList = new List<int>();

                    for (int i = 0; i < clips.Count - 1; i++)
                    {
                        toPlayList.Add(clips[i].combineId);
                    }

                    RunVideo(toPlayList);
                    
                    break;
            }
        }
        
        /// <summary>
        /// To run a combo video, input a sorted list of id's to search for
        /// </summary>
        /// <param name="s"></param>
        private void RunVideo(List<int> i)
        {
            // Find file matching combined id list 
            
            // search reference file for matching id
            // if found play and PurgeList()
            // if not found PurgeList() and throw message to player
            
            i.Sort();
            
            string toPlayString = "";

            for (int j = 0; j < clips.Count - 1; j++)
            {
                toPlayString = toPlayString + i[j] + ",";
            }

            toPlayString.Remove(toPlayString.Length - 1);
            
        }
        
        public void PurgeList()
        {
            foreach (VideoClip v in clips)
            {
                clips[0].myState = VideoClip.ButtonState.deselected;
                clips[0].CheckColour();
                clips.Remove(clips[0]);
            }
        }

        public void SwapState()
        {
            switch (state)
            {
                case ManagerState.normal:
                    state = ManagerState.combining;
                    // Hide/Show ui
                    
                    break;
                
                case ManagerState.combining:
                    state = ManagerState.normal;
                    // Hide/Show ui
                    
                    break;
            }
        }

        private void OnDestroy()
        {
            GlobleEvents.OnClueActivate -= ADD;

            MediaPlayer.EndPlay -= PurgeList;
        }
    }
}

