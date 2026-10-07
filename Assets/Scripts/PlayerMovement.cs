using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public int speed;
    public float friction;

    public int jumpHeight;
    public float DeploymentHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * friction, rb.linearVelocity.y,0);
        rb.linearVelocity = new Vector3(rb.linearVelocity.x + move * speed, rb.linearVelocity.y, 0);
        if(jump > .05f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, 0);
        }
    }

    Boolean isGrounded() 
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * DeploymentHeight);

        if (Physics.Raycast(landingRay, out hit, DeploymentHeight))
        {
            if (hit.collider == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Death")
        {
            Destroy(gameObject);
        }
        else if(collision.collider.tag == "Win")
        {
            Destroy(gameObject);
        }


    }
}
