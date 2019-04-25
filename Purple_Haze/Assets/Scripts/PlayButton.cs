using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayButton : MonoBehaviour
{

    public void PLAY()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLoop");
    }

}
