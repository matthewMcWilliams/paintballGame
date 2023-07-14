using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private string @tag = "DestroyOnLoadUnload";

    private void OnScene()
    {
        var splats = GameObject.FindGameObjectsWithTag(@tag);
        foreach (var splat in splats)
        {
            Destroy(splat);
        }
    }
}
