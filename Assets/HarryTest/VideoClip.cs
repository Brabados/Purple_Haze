using UnityEngine;
using UnityEngine.UI;

namespace Harrison
{
    public class VideoClip : MonoBehaviour
    {
        public ClipStruct myClip;
        
        [HideInInspector] public Button myButton;
        public enum ButtonState { selected, combining, deselected }

        public ButtonState myState = ButtonState.deselected;

        private void Awake()
        {
            myState = ButtonState.deselected;
        }

        private void Start()
        {
            myButton = GetComponent<Button>();
        }

        public void ToggleButton()
        {
            Debug.Log(VideoClipManager.VCM.state);
            
            switch (VideoClipManager.VCM.state)
            {
                case VideoClipManager.ManagerState.normal:

                    if (myState == ButtonState.deselected)
                    {
                        AddToList(ButtonState.selected);   
                        VideoClipManager.VCM.FindVideo();
                    } 
                    
                    break;
                
                case VideoClipManager.ManagerState.combining:

                    if (myState == ButtonState.deselected)
                    {
                        AddToList(ButtonState.combining);   
                    } 
                    else if (myState == ButtonState.combining)
                    {
                        RemoveFromList(ButtonState.deselected);
                    }
                    
                    break;
            }
        }
        
        public void AddToList(ButtonState b)
        {
            if (VideoClipManager.VCM.clips.Contains(this)) return;
            
            VideoClipManager.VCM.clips.Insert(0, this);
            myState = b;
            CheckColour();
        }

        public void RemoveFromList(ButtonState b)
        {
            if (!VideoClipManager.VCM.clips.Contains(this)) return;
            
            VideoClipManager.VCM.clips.Remove(this);
            myState = b;
            CheckColour();
        }
        
        public void CheckColour()
        {
            var colours = myButton.colors;
            
            switch (myState)
            {
                case ButtonState.deselected:
                    
                    colours.normalColor = Color.white;
                    colours.highlightedColor = Color.white;
                    myButton.colors = colours;
                    
                    break;
                
                case ButtonState.selected:

                    colours.normalColor = Color.green;
                    colours.highlightedColor = Color.green;
                    myButton.colors = colours;
                    
                    break;
                
                case ButtonState.combining:
                    
                    colours.normalColor = Color.yellow;
                    colours.highlightedColor = Color.yellow;
                    myButton.colors = colours;
                    
                    break;
            }
        }

    }
}

