using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLevel : MonoBehaviour
{
    public int nextScene;
    public Text percentage;
    public Slider bar;

    void Start()
    {
        StartCoroutine(LoadAsynchronously(nextScene));
    }

    public void LevelLoad()
    {
        StartCoroutine(LoadAsynchronously(nextScene));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log(progress);
            percentage.text = progress * 100f + "%";
            bar.value = progress;
            yield return null;
        }
    }
}
