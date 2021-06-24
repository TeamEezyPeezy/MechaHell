using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
   
   public float moveSpeed = 5f;
   public Rigidbody2D playerRigidBody;
   public Camera cam;

    public Animator animator;

   Vector2 movement;
   Vector2 mousePos;
   float angleOffset = -90f;

    bool isWalking = false;
    bool animationChanged = false;
    void Awake()
    {
        // animator = GetComponent<Animator>();
    }
    void Update()
    {
        GetMovementInputs();
        GetMousePosition();
        HandleMovementAnimations();
        
        // temporary reset for testing purposes
        if(Input.GetButtonDown("Jump")) SceneManager.LoadScene("DoorTestScene");
    }

    void HandleMovementAnimations()
    {
        if(animationChanged)
        {
            Debug.Log("animation change");
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
        playerRigidBody.MovePosition(playerRigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
        
    }
    void RotatePlayer()
    {
        Vector2 lookDir = mousePos - playerRigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - angleOffset;
        playerRigidBody.rotation = angle;
    }

    void GetMovementInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x != 0 || movement.y != 0)
        {
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

  
    }
    void GetMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
