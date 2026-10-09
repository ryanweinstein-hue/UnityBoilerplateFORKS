using System;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] TextMeshProUGUI coinCount;
    public int speed;
    public float friction;

    public int jumpHeight;
    public float DeploymentHeight;

    private int coins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coins = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * friction, rb.linearVelocity.y,0);
        rb.linearVelocity = new Vector3(rb.linearVelocity.x + move * speed, rb.linearVelocity.y, 0);
        if(jump > .05f && isGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, 0);
        }
    }

    private void FixedUpdate()
    {
        coinCount.text = "Coins: " + coins;
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
            coins++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Death")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
        else if(collision.collider.tag == "Win")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameWin");
        }


    }
}
