using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_SmokeBombData", menuName = "Weapons/Smoke Bomb")]
public class SmokeBombDataSO : ToolDataSO
{
    public float Length = 5f;
    public GameObject Prefab;

    public override void UseTool(Transform playerRoot)
    {
        Debug.Log(playerRoot.gameObject.name + " has used a smoke bomb.");
        playerRoot.GetComponentInChildren<AgentCamoflague>().HideForSeconds(Length);
        Instantiate(Prefab, playerRoot.position, Quaternion.identity);
    }
}
