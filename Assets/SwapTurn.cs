using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTurn : MonoBehaviour
{
    public MoveFront frontGuy;
    public MoveBack backGuy;
    public GameObject currentTurnGuy;
    public GameObject currentIdleGuy;

    void Start()
    {
        frontGuy = FindObjectOfType<MoveFront>();
        backGuy = FindObjectOfType<MoveBack>();
        currentTurnGuy = FindObjectOfType<MoveFront>().gameObject;
        currentIdleGuy = FindObjectOfType<MoveBack>().gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            SwapCharacter();
            
        }
    }

    private void SwapCharacter()
    {
        if (frontGuy.frontGuyTurn)
        {
            frontGuy.frontGuyTurn = false;
            backGuy.backGuyTurn = true;
            currentTurnGuy = FindObjectOfType<MoveBack>().gameObject;
            currentIdleGuy = FindObjectOfType<MoveFront>().gameObject;
        }
        else 
        {
            frontGuy.frontGuyTurn = true;
            backGuy.backGuyTurn = false;
            currentTurnGuy = FindObjectOfType<MoveFront>().gameObject;
            currentIdleGuy = FindObjectOfType<MoveBack>().gameObject;
        }
    }
}
