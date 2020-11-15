
//using System.Numerics;
//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CarDrive : MonoBehaviour
{
    [SerializeField] private WheelJoint2D backWheel, frontWheel;
    [SerializeField] private CircleCollider2D backWheelCollider, frontWheelCollider;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Rigidbody2D rigidBody2d, backWheelRb, frontWheelRb;
    [SerializeField] private float speedForward;
    [SerializeField] private float speedBackward;
    [SerializeField] private float wheelTorque;
    [SerializeField] private float bodyTorque;
    [SerializeField] private float jumpForce;
    [SerializeField] private AudioSource jumpSound,dropSound,coinCollect;
    
    private float horizontalInput;
    private float movement;
    private float rotation;
    private bool hasJumped = true;
    private int distance = 0;

    private bool isGameOver;
    public static event Action<int> OnDriving;

    void OnEnable() 
    {
        GameOverEvent.OnGameOver += IsGameOver;
    }

    void OnDisable() 
    {
        GameOverEvent.OnGameOver -= IsGameOver;
    }
    void Start() 
    {
        isGameOver = false;
        
    }
    void Update()
    {
       bool grounded = IsGrounded();

        movement = Input.GetAxisRaw("Vertical") * speedForward;
        rotation = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && grounded && !isGameOver)
        {
            jumpSound.Play();
            hasJumped = true;
            Jump();
        }

        DrivingScore();
    }

    void FixedUpdate()
    {
        if(!isGameOver)
        { 
            DriveOnGround();
         if(!IsGrounded())
            AirControl();    
        }        
    }

    void DriveOnGround()
    {
       if(movement == 0)
       {
           backWheel.useMotor = false;
           frontWheel.useMotor = false;
       }

       else
       {
           backWheel.useMotor = true;
           frontWheel.useMotor = true;
           JointMotor2D motor = new JointMotor2D{ motorSpeed = movement , maxMotorTorque = wheelTorque};
           backWheel.motor = motor;
           frontWheel.motor = motor;


       }

     
    }

    bool IsGrounded()
    {
        bool isGrounded;

        RaycastHit2D backWheelGrounded = Physics2D.CircleCast(backWheelCollider.bounds.center ,backWheelCollider.radius ,Vector2.down, 0.5f, layer);
        RaycastHit2D frontWheelGrounded = Physics2D.CircleCast(frontWheelCollider.bounds.center ,frontWheelCollider.radius ,Vector2.down, 0.5f, layer);
        
        
        Debug.DrawRay(frontWheelCollider.bounds.center, Vector2.down* (frontWheelCollider.bounds.extents.y + 0.5f), Color.red);
        Debug.DrawRay(backWheelCollider.bounds.center, Vector2.down * (backWheelCollider.bounds.extents.y + 0.5f), Color.red);
        
        
        
        if(backWheelGrounded || frontWheelGrounded)
        {
            
            isGrounded = true;
            if(hasJumped)
            {
                dropSound.Play();
                hasJumped = false;
            }
        }
        else
        {
            isGrounded = false;
        }

        return isGrounded;
        
    }


    void Jump()
    { 
        if(movement > 0)
        {
           rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x + 5f, jumpForce); 
        }
        if(movement < 0)
        {
           rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x - 5f, jumpForce); 
        }
        else
        {
             rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x, jumpForce);
        }
       
    }


    void AirControl() => rigidBody2d.AddTorque(-rotation * bodyTorque * Time.fixedDeltaTime);

    void DrivingScore()
    {
        if(movement != 0)
        {
          distance = (int) Mathf.Floor(this.transform.position.x) + 25;
          if(OnDriving != null)
             OnDriving(distance);
        }
    }

    void IsGameOver()
    {
        isGameOver = true;
        backWheelRb.AddForce(new Vector2(0, 100f), ForceMode2D.Impulse);
        frontWheelRb.AddForce(new Vector2(0, 100f), ForceMode2D.Impulse);
        backWheel.breakForce = 100;
        frontWheel.breakForce = 100;

        Time.timeScale = 0;

    }

}
