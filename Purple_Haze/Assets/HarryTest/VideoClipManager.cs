using System.Collections.Generic;
using UnityEngine;

namespace Harrison
{
    public class VideoClipManager : MonoBehaviour
    {

        public static VideoClipManager VCM;

        public enum ManagerState { combining, normal }
        public ManagerState state = ManagerState.normal;
        
        public List<VideoClip> clips;
    
        private void Awake()
        {
            VCM = this;
        }

        public void FindVideo()
        {
            switch (state)
            {
                case ManagerState.normal:

                    // Play Video

                    string toPlay = clips[0].fileReference;
                    // Search For File Reference 
                    RunVideo(toPlay);
                    
                    break;
                
                case ManagerState.combining:

                    // Check list + play corresponding video if found
                    List<int> toPlayList = new List<int>();

                    for (int i = 0; i < clips.Count - 1; i++)
                    {
                        toPlayList.Add(clips[i].id);
                    }

                    RunVideo(toPlayList);
                    
                    break;
            }
        }

        /// <summary>
        /// To run a single video just input the file reference
        /// </summary>
        /// <param name="s"></param>
        private void RunVideo(string s)
        {
            // Find file matching string 
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
        
    }
}

