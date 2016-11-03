using UnityEngine;
using System.Collections;

using System.Collections.Generic;

using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    /*
    Triggers Assignment - Manages player movement, slightly modified from Mirza's tutorial.

    Josh Bellyk - 100526009
    Owen Meier  - 100538643    
    */
    public float speed = 1.0f;
    public float turnSpeed = 0.5f;

    public float jumpForce = 5.0f;

    [Range(0.0f, 1.0f)]
    public float damper = 0.98f;

    private bool jump;
    private bool grounded;

    private CapsuleCollider body;

    private Rigidbody rb;

    public AudioSource collectableAudioSource;

    public int score { get; set; }
    public Text scoreText;
    public int scoreTrigger = 100;

    Vector3 velocity;

    private bool cutscene;
    public bool plrControl = true;

    /// 


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<CapsuleCollider>();

        updateScore();

    }

    void FixedUpdate()
    {
        if (plrControl)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, (body.height / 2.0f) + 0.15f))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }


            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            Vector3 force = Vector3.zero;

            force += transform.forward * vertical;

            force *= speed;

            rb.AddForce(force, ForceMode.VelocityChange);

            Vector3 torque = Vector3.zero;
            torque.y += horizontal * turnSpeed;

            rb.AddTorque(torque, ForceMode.VelocityChange);

            velocity = Vector3.zero;

            if (jump)
            {
                jump = false;
                jumpFunction();
            }

            velocity = rb.velocity;

            velocity.x *= damper;
            velocity.z *= damper;

            rb.velocity = velocity;
        }
    }

    public void jumpFunction()
    {
        velocity = rb.velocity;

        velocity.y = 0.0f;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump = true;
        }
    }

    public void updateScore()
    {
        scoreText.text = score.ToString("0");
    }

}