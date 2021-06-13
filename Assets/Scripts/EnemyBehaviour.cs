using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Movement variables")]
    public float rotationSpeed;
    private bool goingUp;
    private float time;
    private bool patroling = true;
    public PatrolBeacon[] patrolPoints;
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
    private float targetRotation;
    //private Vector3 targetDirection;
    public GameObject playerInSight = null;

    [Header("State variables")]
    public bool shouldRotate;
    public bool shouldHover;
    public bool shouldPatrol;

    public Animator animatorEnemy;
    private bool GameOver = false;
    
    void Start()
    {
        player2 = FindObjectOfType<MoveBack>();
        player1 = FindObjectOfType<MoveFront>();     

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
        if (shouldPatrol)
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
                
                targetRotation = Vector3.Angle(transform.forward,player1.transform.position - transform.position);                
                if (targetRotation > 1f)
                {
                    transform.Rotate(new Vector3(0, targetRotation * Time.deltaTime * sightSpeed, 0));
                }
                animatorEnemy.SetBool("IsWalking", false);
                
            }
            else if (hit4.collider.name == "Player2")
            {
                patroling = false;
                playerInSight = hit4.collider.gameObject;

                targetRotation = Vector3.Angle(transform.forward,player1.transform.position - transform.position);                
                if (targetRotation > 1f)
                {
                    transform.Rotate(new Vector3(0, targetRotation * Time.deltaTime * sightSpeed, 0));
                }
                animatorEnemy.SetBool("IsWalking", false);
            }
            else if (!patroling)
            {
                patroling = true;
                playerInSight = null;
                targetRotation = 0;
                animatorEnemy.SetBool("IsWalking", true);
            }
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
                    transform.rotation = Quaternion.LookRotation(-patrolLookDir);
                }               
            }
        }

        //detect if players are seen
        for (int i = 0; i < rayCount; i++)
        {
            //targetDirection = new Vector3(transform.forward.x + i * rayStep, (transform.position.y - player1.transform.position.y) * transform.forward.y, (transform.position.z - player1.transform.position.z) * transform.forward.z);

            Physics.Raycast(transform.position, -new Vector3(transform.forward.x + i * rayStep, transform.forward.y, transform.forward.z), out RaycastHit hit1, Mathf.Infinity);
            Debug.DrawRay(transform.position, 100f * new Vector3(transform.forward.x + i * rayStep, transform.forward.y, transform.forward.z), Color.magenta);

            Physics.Raycast(transform.position, -new Vector3(transform.forward.x - i * rayStep, transform.forward.y, transform.forward.z), out RaycastHit hit2, Mathf.Infinity);
            Debug.DrawRay(transform.position, 100f * new Vector3(transform.forward.x - i * rayStep, transform.forward.y, transform.forward.z), Color.magenta);
  
            if ((hit1.collider.name == "Player1" || hit1.collider.name == "Player2" || hit2.collider.name == "Player1" || hit2.collider.name == "Player2"))
            {
                //Game Over
                GameOver = true;               
            }                       
        }

        if (GameOver)
        {
            this.GetComponentInChildren<Light>().spotAngle += .1f;
            this.GetComponentInChildren<Light>().color = Color.red;
            if (Time.timeScale > .2f)
            {
                Time.timeScale -= .1f;
            }           
            StartCoroutine(EndGame());
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

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
