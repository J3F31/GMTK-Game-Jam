using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Get variables")]
    private SwapTurn turnsManager;

    [Header("Camera variables")]
    private Vector3 velocity = Vector3.zero;
    public float smoothTime; 


    void Start()
    {
        turnsManager = FindObjectOfType<SwapTurn>();
    }

    void Update()
    {
        this.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(turnsManager.currentTurnGuy.transform.position.x, transform.position.y, transform.position.z), ref velocity, smoothTime);
    }
}
