using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Movement variables")]
    private bool goingUp;

    [Header("Sight variables")]
    public float boxCastHalfExtents;
    public int rayCount;
    public float rayStep;

    private void Start()
    {
        //StartCoroutine(Hover());
    }

    private void Update()
    {
        //transform.Rotate(new Vector3(0, 0.3f, 0));

        //if (goingUp)
        //{
        //    transform.Translate(Vector3.up * Time.deltaTime);
        //}
        //else
        //{
        //    transform.Translate(Vector3.down * Time.deltaTime);
        //}

        Debug.DrawRay(transform.position, 50f * -transform.forward, Color.green);

        for (int i = 0; i < rayCount; i++)
        {
            Physics.Raycast(transform.position, -new Vector3(transform.forward.x + i * rayStep, transform.forward.y, transform.forward.z), out RaycastHit hit1, Mathf.Infinity);
            Debug.DrawRay(transform.position, 100f * -new Vector3(transform.forward.x + i * rayStep, transform.forward.y, transform.forward.z), Color.magenta);

            Physics.Raycast(transform.position, -new Vector3(transform.forward.x - i * rayStep, transform.forward.y, transform.forward.z), out RaycastHit hit2, Mathf.Infinity);
            Debug.DrawRay(transform.position, 100f * -new Vector3(transform.forward.x - i * rayStep, transform.forward.y, transform.forward.z), Color.magenta);

            if (hit1.collider.GetComponent<MoveFront>() || hit2.collider.GetComponent<MoveFront>())
            {
                Debug.Log("hit");
            }
            if (hit1.collider.GetComponent<MoveBack>() || hit2.collider.GetComponent<MoveBack>())
            {
                Debug.Log("hit");
            }
        }
    }

    IEnumerator Hover()
    {
        goingUp = true;
        yield return new WaitForSeconds(.5f);
        goingUp = false;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Hover());
    }
}
