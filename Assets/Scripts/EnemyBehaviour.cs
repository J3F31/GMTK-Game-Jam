using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Movement variables")]
    public float rotationSpeed;
    private bool goingUp;
    private float time;
    private bool patroling = true;
    private Quaternion initialPatrolRotation;
    private Vector3 initialPatrolPosition;
    private PatrolBeacon[] patrolPoints;
    public float patrolSpeed;
    private Vector3 nextPatrolPoint;
    public float timeBetweenPatrolPoints;
    private Vector3 patrolLookDir;

    [Header("Sight variables")]
    public int rayCount;
    public float rayStep;
    public float sightSpeed;
    private MoveBack player2;
    private MoveFront player1;
    private Quaternion targetRotation;
    //private Vector3 targetDirection;
    public GameObject playerInSight = null;

    [Header("State variables")]
    public bool shouldRotate;
    public bool shouldHover;
    public bool shouldPatrol;

    void Start()
    {
        player2 = FindObjectOfType<MoveBack>();
        player1 = FindObjectOfType<MoveFront>();

        initialPatrolRotation = transform.rotation;
        initialPatrolPosition = transform.position;

        patrolPoints = FindObjectsOfType<PatrolBeacon>();

        if (shouldRotate)
        {
            this.time = 2f;
        }

        if (shouldHover)
        {
            this.time = 3f;
        }  

        StartCoroutine(Timer());

        if (shouldPatrol)
        {
            StartCoroutine(Patrol());
        }        
    }

    void Update()
    {
        //look if players are in sight area        
        Physics.Raycast(transform.position, -(transform.position - player1.transform.position).normalized, out RaycastHit hit3, Mathf.Infinity);
        Debug.DrawRay(transform.position, 100f * -(transform.position - player1.transform.position).normalized, Color.blue);
        Physics.Raycast(transform.position, -(transform.position - player2.transform.position).normalized, out RaycastHit hit4, Mathf.Infinity);
        Debug.DrawRay(transform.position, 100f * -(transform.position - player2.transform.position).normalized, Color.blue);

        if (hit3.collider.name == "Player1")
        {
            patroling = false;
            playerInSight = hit3.collider.gameObject;
            Debug.Log(hit3.collider.name);

            float str = Mathf.Min(sightSpeed * Time.deltaTime, 1);
            targetRotation = Quaternion.LookRotation(-playerInSight.transform.position + transform.position, -Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
        else if (hit4.collider.name == "Player2")
        {
            patroling = false;
            playerInSight = hit4.collider.gameObject;
            Debug.Log(hit4.collider.name);

            float str = Mathf.Min(sightSpeed * Time.deltaTime, 1);
            targetRotation = Quaternion.LookRotation(-playerInSight.transform.position + transform.position, -Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
        else if (!patroling)
        {
            transform.rotation = initialPatrolRotation;
            transform.position = initialPatrolPosition;
            patroling = true;
            playerInSight = null;
        }

        if (patroling)
        {
            if (shouldHover)
            {
                if (goingUp)
                {
                    transform.Translate(Vector3.down * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.up * Time.deltaTime);
                }
            }

            if (shouldRotate)
            {
                if (goingUp)
                {
                    transform.Rotate(new Vector3(0, rotationSpeed, 0));
                }
                else
                {
                    transform.Rotate(new Vector3(0, -rotationSpeed, 0));
                }
            }

            if (shouldPatrol)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPatrolPoint, patrolSpeed * Time.deltaTime);
                if (transform.position != nextPatrolPoint)
                {
                    transform.rotation = Quaternion.LookRotation(patrolLookDir);
                }               
            }
        }

        //detect if players are seen
        for (int i = 0; i < rayCount; i++)
        {
            //targetDirection = new Vector3(transform.forward.x + i * rayStep, (transform.position.y - player1.transform.position.y) * transform.forward.y, (transform.position.z - player1.transform.position.z) * transform.forward.z);

            //Debug.Log(targetDirection);

            Physics.Raycast(transform.position, -new Vector3(transform.forward.x + i * rayStep, transform.forward.y, transform.forward.z), out RaycastHit hit1, Mathf.Infinity);
            Debug.DrawRay(transform.position, 100f * -new Vector3(transform.forward.x + i * rayStep, transform.forward.y, transform.forward.z), Color.magenta);

            Physics.Raycast(transform.position, -new Vector3(transform.forward.x - i * rayStep, transform.forward.y, transform.forward.z), out RaycastHit hit2, Mathf.Infinity);
            Debug.DrawRay(transform.position, 100f * -new Vector3(transform.forward.x - i * rayStep, transform.forward.y, transform.forward.z), Color.magenta);
  
            if ((hit1.collider.name == "Player1" || hit1.collider.name == "Player2" || hit2.collider.name == "Player1" || hit2.collider.name == "Player2"))
            {
                Debug.Log("Game Over");
            }                       
        }       
    }

    IEnumerator Patrol()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {           
            nextPatrolPoint = patrolPoints[i].transform.position;
            patrolLookDir = (transform.position - nextPatrolPoint).normalized;
            yield return new WaitForSeconds(timeBetweenPatrolPoints);
        }       
        StartCoroutine(Patrol());
    }

    IEnumerator Timer()
    {
        this.goingUp = true;
        yield return new WaitForSeconds(time);
        this.goingUp = false;
        if (shouldRotate)
        {
            this.time = 3.5f;
        }        
        yield return new WaitForSeconds(time);
        StartCoroutine(Timer());
    }
}
