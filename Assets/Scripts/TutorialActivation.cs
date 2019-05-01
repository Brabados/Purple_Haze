using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialActivation : MonoBehaviour
{

    public List<GameObject> Activate;
    public bool disable = true;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject n in Activate)
        {
            n.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            if (disable)
            {
                foreach (GameObject n in Activate)
                {
                    if (n.activeSelf == false)
                    {
                        n.SetActive(true);
                        AudioSource WargTalk = GetComponent<AudioSource>();
                        WargTalk.Play(); 
                    }
                }
            }
            disable = false;

        }
    }
}
