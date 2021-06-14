using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    [Header("Get variables")]
    private GameObject stopWall;
    private SwapTurn turnManager;
    private LineRenderer link;
    private Transform player1;
    private Transform player2;
    public Material linkDef;
    public Material linkStretch;

    [Header("Constraint variables")]
    public float distance;
    public float maxDistance;
    private Vector3 dir;

    void Start()
    {
        stopWall = GameObject.Find("StopWall");
        turnManager = FindObjectOfType<SwapTurn>();
        link = GetComponent<LineRenderer>();
        player1 = FindObjectOfType<MoveFront>().gameObject.transform;
        player2 = FindObjectOfType<MoveBack>().gameObject.transform;        
    }

    void Update()
    {
        link.SetPosition(0, player1.position);
        link.SetPosition(1, player2.position);

        distance = Mathf.Abs((player1.position - player2.position).magnitude);
        dir = (turnManager.currentTurnGuy.transform.position - turnManager.currentIdleGuy.transform.position).normalized;

        if (distance >= maxDistance)
        {           
            stopWall.transform.position = new Vector3(dir.x * turnManager.currentTurnGuy.GetComponent<Collider>().bounds.extents.x + turnManager.currentTurnGuy.transform.position.x, 0, turnManager.currentTurnGuy.transform.position.z);
            stopWall.SetActive(true);
        }
        else
        {
            stopWall.SetActive(false);
        }
    }
}
