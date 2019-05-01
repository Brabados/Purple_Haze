using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TheEnding : MonoBehaviour
{
    public PlayButton Thebutt;
    public UnityEngine.UI.ScrollRect buttons;
    public List<UnityEngine.UI.Button> Buttons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.active == true)
        {
            buttons.gameObject.SetActive(false);
            foreach(UnityEngine.UI.Button a in Buttons)
            {
                a.enabled = false;
            }
            Thebutt.PLAY();
        }
    }
}
