using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

public class EnemyPatrolLogic : MonoBehaviour
{
    public PlayMakerFSM fsm;
    public FsmGameObject go;

    // Start is called before the first frame update
    void Start()
    {
        fsm = GetComponent<PlayMakerFSM>();
        go = fsm.FsmVariables.GetFsmGameObject("Waypoint_1");
    }

    public void Flip()
    {
        Debug.Log(go.Value.transform.position);
        Transform waypoint = go.Value.transform;
        if (transform.position.x < waypoint.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
            GetComponent<SpriteRenderer>().flipX = true;
    }
}
