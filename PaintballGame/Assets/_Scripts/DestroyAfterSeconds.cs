using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _seconds = 1f;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_seconds);
        Destroy(gameObject);
    }
}
