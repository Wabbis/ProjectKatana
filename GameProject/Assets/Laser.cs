using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        Debug.DrawLine(transform.position, hit.point);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.position);
    }
}
