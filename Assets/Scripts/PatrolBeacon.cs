using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBeacon : MonoBehaviour
{
    void Update()
    {
        //Collider[] collider = Physics.OverlapBox(transform.position, new Vector3(boxSize, boxSize, boxSize), Quaternion.identity);
        //if (collider.Length > 0)
        //{
        //    isNextPoint = false;
        //    nextPatrolPoint.isNextPoint = true;
        //}


        Debug.DrawLine(transform.position, transform.position + Vector3.down * 100f, Color.cyan);
    }
}
