using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    [Header("Movement variables")]
    Vector3 pos;
    public float velocity;
    public float jumpHeight;
    private float speed;

    [Header("Object variables")]
    private new Collider collider;
    private Rigidbody rb;

    [Header("Status variables")]
    public bool backGuyTurn = false;
    private bool isGrounded = true;
    public Animator animatorPlayer2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (backGuyTurn)
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
                animatorPlayer2.SetBool("Walking2", false);
            }
            else if (speed > 0)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                animatorPlayer2.SetBool("Walking2", true);
            }
            else if (speed < 0)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                animatorPlayer2.SetBool("Walking2", true);
            }

            if (rb.velocity.y < .5f)
            {
                animatorPlayer2.SetBool("Jumping2", false);
            }
            else
            {
                animatorPlayer2.SetBool("Jumping2", true);
            }
        }
        else
        {
            animatorPlayer2.SetBool("Walking2", false);
            animatorPlayer2.SetBool("Jumping2", false);
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
