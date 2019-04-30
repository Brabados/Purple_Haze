using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public List<AudioSource> UISounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeNoise()
    {
        int rando = Random.Range(0, UISounds.Count);
        UISounds[rando].Play();
    }
}
