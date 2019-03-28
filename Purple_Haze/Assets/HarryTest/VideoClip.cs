using UnityEngine;
using UnityEngine.UI;

namespace Harrison
{
    public class VideoClip : MonoBehaviour
    {
        public int id;
        public string fileReference;
        [HideInInspector] public Button myButton;
        public enum ButtonState { selected, combining, deselected }

        public ButtonState myState = ButtonState.deselected;

        private void Start()
        {
            myButton = GetComponent<Button>();
        }

        public void ToggleButton()
        {
            switch (VideoClipManager.VCM.state)
            {
                case VideoClipManager.ManagerState.normal:

                    if (myState == ButtonState.deselected)
                    {
                        AddToList(ButtonState.selected);   
                        CheckColour();
                        VideoClipManager.VCM.FindVideo();
                    } 
                    
                    break;
                
                case VideoClipManager.ManagerState.combining:

                    if (myState == ButtonState.deselected)
                    {
                        AddToList(ButtonState.combining);   
                        CheckColour();
                    }

                    if (myState == ButtonState.combining)
                    {
                        RemoveFromList(ButtonState.deselected);
                        CheckColour();
                    }
                    
                    break;
            }
        }
        
        public void AddToList(ButtonState b)
        {
            VideoClipManager.VCM.clips.Insert(0, this);
            myState = b;
        }

        public void RemoveFromList(ButtonState b)
        {
            VideoClipManager.VCM.clips.Remove(this);
            myState = b;
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

