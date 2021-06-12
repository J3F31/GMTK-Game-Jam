using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Quaternion rotatePos;
    public float rotateSpeed;

    private void Update()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotatePos, rotateSpeed * Time.deltaTime);
        
    }

}
