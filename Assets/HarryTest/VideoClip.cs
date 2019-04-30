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
        public AnimationTriggers BaseSetUp;
        public Image Circles;
        public SpriteHolder Held;
        private void Awake()
        {
            myState = ButtonState.deselected;
        }

        private void Start()
        {
            myButton = GetComponent<Button>();
            BaseSetUp = myButton.animationTriggers;
            Circles = GetComponent<Image>();
            Held = GetComponent<SpriteHolder>();
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
            var colours = myButton.animationTriggers;
            
            switch (myState)
            {
                // When the button is neither selected or clicked
                case ButtonState.deselected:

                    Circles.sprite = Held.clear;
                    
                    break;
                // When the button is clicked - and the Inspection screen is not combining
                case ButtonState.selected:

                    Circles.sprite = Held.Holden;

                    
                    break;
                // When the button is click - and the Inspection screen IS combining
                case ButtonState.combining:

                    Circles.sprite = Held.Holden;

                    
                    break;
            }
        }

    }
}

