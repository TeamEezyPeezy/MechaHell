using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
   
   private enum State {
       Normal,
       Dashing,
   }
   public float moveSpeed = 5f;
   public float dashStartSpeed;
   public float dashSpeedDropMultiplier;
   public float dashCooldown;
   public TextMeshProUGUI dashCooldownTimer;
   float dashSpeed;
   public Rigidbody2D playerRigidBody;
   public Camera cam;

    public Animator animator;

    public AudioSource DashSound;


    float dashStartTime;
   Vector2 movement;
   Vector2 lastMovementDirection;
   Vector2 mousePos;
   float angleOffset = -90f;

   State state;


    bool isWalking = false;
    bool animationChanged = false;
    bool isDashButtonDown;
    bool dashOnCooldown;
    void Awake()
    {
        state = State.Normal;
        dashOnCooldown = false;
        dashCooldownTimer.SetText("Dash ready!");

    }
    void Update()
    {
        GetMovementInputs();
        GetMousePosition();
        HandleMovementAnimations();
        HandleCooldowns();
        
      
    }

    void HandleCooldowns()
    {
        if(dashOnCooldown)
        {
            dashCooldownTimer.SetText(dashCooldown - ((int)(Time.time - dashStartTime)) + "s");
            if(Time.time - dashStartTime > dashCooldown)
            {
                dashOnCooldown = false;
                dashCooldownTimer.SetText("Dash ready!");
            }
        }
    }

    void HandleMovementAnimations()
    {
        if(animationChanged)
        {
            animator.SetBool("isMoving", isWalking);
            animationChanged = false;
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        switch(state){
            case State.Normal:
                playerRigidBody.MovePosition(playerRigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
                break;
            case State.Dashing:
                playerRigidBody.velocity = lastMovementDirection * dashSpeed;;
            break;
        }
        
    }
    void RotatePlayer()
    {
        Vector2 lookDir = mousePos - playerRigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - angleOffset;
        playerRigidBody.rotation = angle;
    }

    void GetMovementInputs()
    {
        switch(state){
            case State.Normal:
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                if (movement.x != 0 || movement.y != 0)
                {
                    lastMovementDirection = movement; // allows dashing even when character is not moving

                    if(!isWalking){
                        animationChanged = true;
                        isWalking = true;
                    }
                } else
                {
                    if(isWalking){
                        animationChanged = true;
                        isWalking = false;
                    }
                }

                if(Input.GetButtonDown("Jump")){
                    // dash
                    if(!dashOnCooldown)
                    {
                        isDashButtonDown = true;
                        dashSpeed = 100f;
                        state = State.Dashing;
                        dashStartTime = Time.time;
                        dashOnCooldown = true;

                        // start dash animation here

                        DashSound.Play();
                    }
                    
                }
                break;
            }
            case State.Dashing:
                dashSpeed -= dashSpeed * dashSpeedDropMultiplier * Time.deltaTime;

                float dashSpeedMinimum = moveSpeed;
                if (dashSpeed < dashSpeedMinimum) {
                    state = State.Normal;
                    // end dash animation here
                }
                break;
        }
    }
    void GetMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
