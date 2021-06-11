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
    private MeshRenderer meshRend;
    private Rigidbody rb;

    [Header("Status variables")]
    public bool frontGuyTurn = true;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (frontGuyTurn)
        {
            speed = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;
            pos.x = velocity * speed;
            this.transform.position += pos;

            if (Input.GetKeyDown("up") && isGrounded || Input.GetKeyDown("w") && isGrounded)
            {
                rb.AddForce(jumpHeight * Vector3.up, ForceMode.Impulse);
            }
        }


        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, meshRend.bounds.extents.y * 2))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    
}
