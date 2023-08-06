using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    private void Start()
    {
        EnableUI(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            EnableUI();
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

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (SceneManager.GetSceneByName("ShopUI").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("ShopUI", LoadSceneMode.Additive);
            } else
            {
                SceneManager.UnloadSceneAsync("ShopUI");
            }
        }
    }

    private static void EnableUI(bool alwaysEnable = false)
    {
        if (SceneManager.GetSceneByName("UI").isLoaded == false)
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        else if (!alwaysEnable)
            SceneManager.UnloadSceneAsync("UI");
    }
}
