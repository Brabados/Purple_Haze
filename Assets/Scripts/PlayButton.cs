using UnityEngine;

public class PlayButton : MonoBehaviour
{

    public void PLAY()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLoop");
    }

}
