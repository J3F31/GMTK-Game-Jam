using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFront : MonoBehaviour
{
    [Header("Movement variables")]
    Vector3 pos;
    public float velocity;
    public float jumpHeight;
    private float speed;

    [Header("Position variables")]
    private new Collider collider;
    private Rigidbody rb;

    [Header("Status variables")]
    public bool frontGuyTurn = true;
    private bool isGrounded;
    public Animator animatorPlayer1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (frontGuyTurn)
        {
            speed = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;
            pos.x = velocity * speed;
            this.transform.position += pos;

            if (Input.GetKeyDown("up") && isGrounded)
            {
                rb.AddForce(jumpHeight * Vector3.up, ForceMode.Impulse);
            }

            if (speed == 0)
            {
                animatorPlayer1.SetBool("Walking1", false);
            }
            else if (speed > 0)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                animatorPlayer1.SetBool("Walking1", true);
            }
            else if (speed < 0)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                animatorPlayer1.SetBool("Walking1", true);
            }

            if (rb.velocity.y == 0)
            {
                animatorPlayer1.SetBool("Jumping1", false);
            }
            else
            {
                animatorPlayer1.SetBool("Jumping1", true);
            }
        }
        else
        {
            animatorPlayer1.SetBool("Jumping1", false);
            animatorPlayer1.SetBool("Walking1", false);
        }



        Debug.DrawLine(transform.position, transform.position + Vector3.down * collider.bounds.extents.y / 2, Color.magenta);

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, collider.bounds.extents.y / 2))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    
}
