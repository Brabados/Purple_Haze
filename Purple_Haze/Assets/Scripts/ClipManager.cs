using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class ClipManager : MonoBehaviour {

    //List of Clues, used for spawning buttons
    public List<ClipStruct> FoundCluesLisCS = new List<ClipStruct>();

    //The player to pump clips into to be played
    public Player MediaPlayer;

    //Prefab used in button creation
    public Button BaseButt;

    //Used to hold the insantiated button for use
    public Button placeholder;

    //The canvas element that is storing the buttons for later use
    public ScrollRect Can;

    void Start ()
    {
        //Gets the meidea player and the canvas obj 
        MediaPlayer = GetComponent<Player>();
        Can = FindObjectOfType<ScrollRect>();

        //Adds the create new button function to a global event to be called when cluesa are found 
        GlobleEvents.OnClueActivate += ADD;

        //Gets all base clipstructs. To be used in loading games later
        foreach(GameObject N in FindObjectsOfType<GameObject>())
        {
            ClipStruct basic = N.GetComponent<ClipStruct>();
            if (basic != null )
            {
                FoundCluesLisCS.Add(basic);
            }

        }
        int i = 0;
        //Constructs button layout from found clipstructs. To be used in loading games later
        foreach (ClipStruct A in FoundCluesLisCS)
        {
            placeholder = Instantiate(BaseButt);
            placeholder.transform.parent = Can.viewport.GetChild(0).transform;
            placeholder.GetComponent<RectTransform>().localPosition = new Vector3(90,(i * - 40) -20,0);
            placeholder.GetComponentInChildren<Text>().text = A.Question;
            placeholder.onClick.AddListener(delegate { test(A); Can.gameObject.SetActive(false); });
            i = i+ 1;
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


    //Function to call when adding a new button to the investigation canvas
    public void ADD(ClipStruct adder)
    {
        //Constructs button layout from found clipstructs. With the addition of only adding it if the clue hasn't already been found
        if (!FoundCluesLisCS.Contains(adder))
        {
            FoundCluesLisCS.Add(adder);
            placeholder = Instantiate(BaseButt);
            placeholder.transform.parent = Can.viewport.GetChild(0).transform;
            placeholder.GetComponent<RectTransform>().localPosition = new Vector3(90, ((FoundCluesLisCS.Count - 1) * -40) - 20, 0);
            placeholder.GetComponentInChildren<Text>().text = adder.Question;
            placeholder.onClick.AddListener(delegate { test(adder); Can.gameObject.SetActive(false); });
        }
      

    }

}
