using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationHelper : MonoBehaviour
{
    public static void Quit()
    {
        Debug.Log("QUITTING");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
