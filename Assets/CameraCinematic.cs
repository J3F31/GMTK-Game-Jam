using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematic : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Time.deltaTime * 5, 0, 0);
    }
}
