using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Movement variables")]
    private bool goingUp;
    float time = 3f; //2

    [Header("Sight variables")]
    public float boxCastHalfExtents;
    public int rayCount;
    public float rayStep;

    private void Start()
    {        
        StartCoroutine(Hover());
    }

    private void Update()
    {
        

        if (goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            //transform.Rotate(new Vector3(0, 0.3f, 0));
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime);
            //transform.Rotate(new Vector3(0, -0.3f, 0));
        }

        for (int i = 0; i < rayCount; i++)
        {
            Physics.Raycast(transform.position, -new Vector3(transform.forward.x + i * rayStep, transform.forward.y, transform.forward.z), out RaycastHit hit1, Mathf.Infinity);
            Debug.DrawRay(transform.position, 100f * -new Vector3(transform.forward.x + i * rayStep, transform.forward.y, transform.forward.z), Color.magenta);

            Physics.Raycast(transform.position, -new Vector3(transform.forward.x - i * rayStep, transform.forward.y, transform.forward.z), out RaycastHit hit2, Mathf.Infinity);
            Debug.DrawRay(transform.position, 100f * -new Vector3(transform.forward.x - i * rayStep, transform.forward.y, transform.forward.z), Color.magenta);


            if (hit1.collider || hit2.collider)
            {
                if ((hit1.collider.name == "Player1" || hit1.collider.name == "Player2" || hit2.collider.name == "Player1" || hit2.collider.name == "Player2"))
                {
                    Debug.Log("Game Over");
                    //slow time until stopped, fade game to end, restart game
                }
            }           
        }
    }

    IEnumerator Hover()
    {
        goingUp = true;
        yield return new WaitForSeconds(time);
        goingUp = false;
        //time = 3.5f;
        yield return new WaitForSeconds(time);
        StartCoroutine(Hover());
    }
}
