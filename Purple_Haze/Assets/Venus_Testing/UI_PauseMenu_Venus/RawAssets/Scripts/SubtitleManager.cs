using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public Queue<string> Dialogue;
    public Text nameText;
    public Text SubtitleText;
    public Subtitles subtitles;

    public float[] Delay;
    //public float timer;

    // Start is called before the first frame update
    void Start()
    {
        Dialogue = new Queue<string>();

        //FindObjectOfType<SubtitleTrigger>().SubsOff += EndSubtitles;
        //timer += Time.deltaTime;
    }

    void Update()
    {
        //timer += Time.deltaTime;
    }

    public void PlaySubtitle(bool IsOn)
    {
        if (IsOn == true)
        {
            GetComponent<Canvas>().enabled = true;

            //Debug.Log("Showing subtitles!");
            Dialogue.Clear();

            foreach (string sentence in subtitles.Sentences)
            {
                Dialogue.Enqueue(sentence);
            }

            NextSubtitle();
            nameText.text = subtitles.Name;
            StartCoroutine(Sequence());
        }
        else
        {
            EndSubtitles();
        }
    }

    public void NextSubtitle()
    {
        if (Dialogue.Count == 0)
        {
            EndSubtitles();
            return;
        }

        string sentence = Dialogue.Dequeue();
        SubtitleText.text = sentence;
    }

    public void EndSubtitles()
    {
        Dialogue.Clear();
        GetComponent<Canvas>().enabled = false;
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(4);
        NextSubtitle();
    }
}