using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
   public float moveSpeed = 5f;
   public Rigidbody2D playerRigidBody;
   public Camera cam;
   Vector2 movement;
   Vector2 mousePos;
   float angleOffset = 0f;



    // Update is called once per frame
    void Update()
    {
        GetMovementInputs();
        GetMousePosition();
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
    }
    void GetMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
