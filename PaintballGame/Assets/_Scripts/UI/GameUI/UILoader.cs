using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (SceneManager.GetSceneByName("UI").isLoaded == false)
                SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            else
                SceneManager.UnloadSceneAsync("UI");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (SceneManager.GetSceneByName("PauseMenu").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("PauseMenu");
            }
        }
    }
}
