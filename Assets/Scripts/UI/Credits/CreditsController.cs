using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public void ReturnToMenu()
    {
        StartCoroutine(LoadMenuAsyncScene());
    }

    private IEnumerator LoadMenuAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
