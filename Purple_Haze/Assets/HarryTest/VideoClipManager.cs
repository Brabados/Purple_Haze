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
        public List<ComboVideo> comboVideos;
        private List<ComboVideo> usedComboVideos = new List<ComboVideo>();
        
        //List of Clues, used for spawning buttons
        public List<ClipStruct> FoundCluesLisCS = new List<ClipStruct>();
        
        public Player MediaPlayer;
        public ScrollRect Can;
        
        //Used to hold the insantiated button for use
        public Button placeholder;
        //Prefab used in button creation
        public Button BaseButt;

        public GameObject combineToggleButton;
        public GameObject testCombineButton;
        
        private void Awake()
        {
            VCM = this;
            state = ManagerState.normal;
            
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
                Debug.Log("ran ADD");
                FoundCluesLisCS.Add(adder);
                placeholder = Instantiate(BaseButt);
                placeholder.transform.parent = Can.viewport.GetChild(0).transform;
                
                VideoClip v = placeholder.gameObject.AddComponent<VideoClip>();
                v.myClip = adder;
                
                placeholder.GetComponent<RectTransform>().localPosition = new Vector3(90, ((FoundCluesLisCS.Count - 1) * -40) - 20, 0);
                placeholder.GetComponentInChildren<Text>().text = adder.Question;
                
                placeholder.onClick.AddListener(delegate { v.ToggleButton(); ToggleCanvasNormal(); });
            }
        }

        public void ToggleCanvasNormal()
        {
            if (state == ManagerState.normal)
            {
                Can.gameObject.SetActive(false);
            }
        }
        
        public void Play(ClipStruct MyClip)
        {
            //Hands the clip to the player to run in a coroutine
            MediaPlayer.EndPlay += Redraw;
            StartCoroutine(MediaPlayer.playVideo(MyClip));
        }
        
        public void Redraw()
        {
            //resets the Investigation canvas
            Can.enabled = true;
            Can.gameObject.SetActive(true);
            GetComponent<RawImage>().enabled = false;    
            MediaPlayer.EndPlay -= Redraw;
        }
        
        public void FindVideo()
        {
            switch (state)
            {
                case ManagerState.normal:

                    // Play Video
                    Play(clips[0].myClip);
                    
                    break;
                
                case ManagerState.combining:

                    // Check list + play corresponding video if found
                    List<int> toPlayList = new List<int>();

                    int i = 0;
                    foreach (var c in clips)
                    {
                        toPlayList.Add(clips[i].myClip.combineId);
                        i++;
                    }
                    
                    RunComboVideo(toPlayList);
                    
                    break;
            }
        }
        
        /// <summary>
        /// To run a combo video, input a sorted list of id's to search for
        /// </summary>
        /// <param name="s"></param>
        private void RunComboVideo(List<int> input)
        {
            Debug.Log("finding combo video");
            input.Sort();
            ComboVideo toPlay = ScriptableObject.CreateInstance<ComboVideo>();
            bool found = false;
            
            foreach (ComboVideo v in comboVideos)
            {
                List<int> id = v.ids;
                id.Sort();

                string a = "";

                foreach (int i in input)
                {
                    a = a + i.ToString() + ",";
                }

                string b = "";

                foreach (int i in id)
                {
                    b = b + i.ToString() + ",";
                }
                
                Debug.Log("Checking: " + a + "     against: " + b);
                
                if (a == b)
                {
                    toPlay = v;
                    found = true;
                    break;
                }
            }
            
            if (found)
            {
                Debug.Log("video found");
                if (!usedComboVideos.Contains(toPlay))
                {
                    usedComboVideos.Add(toPlay);
                    ADD(toPlay.clipStruct);
                }
                
                Can.gameObject.SetActive(false);
                Play(toPlay.clipStruct);
            }
            else
            {
                Debug.Log("Could not find video");
                PurgeList();
                // THROW ERROR FOR PLAYER
                // THROW ERROR FOR PLAYER
                // THROW ERROR FOR PLAYER
            }
            
        }
        
        public void PurgeList()
        {
            if (clips == null) return;
            
            int i = 0;
            foreach (VideoClip v in clips)
            {
                clips[i].myState = VideoClip.ButtonState.deselected;
                clips[i].CheckColour();
                i++;
            }
            
            clips.Clear();
        }

        public void SwapState()
        {
            switch (state)
            {
                case ManagerState.normal:
                    state = ManagerState.combining;
                    // Hide/Show ui
                    combineToggleButton.GetComponentInChildren<Text>().text = "Question";
                    testCombineButton.SetActive(true);
                    PurgeList();
                    
                    break;
                
                case ManagerState.combining:
                    state = ManagerState.normal;
                    // Hide/Show ui
                    combineToggleButton.GetComponentInChildren<Text>().text = "Push Them";
                    testCombineButton.SetActive(false);
                    PurgeList();
                    
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

