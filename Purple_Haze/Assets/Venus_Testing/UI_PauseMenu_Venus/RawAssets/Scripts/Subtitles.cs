using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Subtitles
{
    public string Name;

    [TextArea(3, 10)]
    public string[] Sentences;
}
