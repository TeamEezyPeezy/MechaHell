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
<<<<<<< HEAD
=======

>>>>>>> main


    // Update is called once per frame
    void Update()
    {
        GetMovementInputs();
        GetMousePosition();
<<<<<<< HEAD

        animator = GetComponent<Animator>();

        if (Input.GetButtonDown("Jump")) SceneManager.LoadScene("AiTestScene");
=======
        if(Input.GetButtonDown("Jump")) SceneManager.LoadScene("DoorTestScene");
>>>>>>> main
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
        animator.SetBool("isMoving", true);
    }
    void GetMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
