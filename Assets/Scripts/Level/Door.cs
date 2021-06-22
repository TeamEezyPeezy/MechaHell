using System;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, iDoor
{
    public Animator animator;

    public Room nextRoom;

    private bool isOpen = false;

    [Tooltip("Cost to open door, for example key / money")]
    public int doorCost = 1;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            if (GameManager.Instance.Money >= doorCost)
            {
                // Open Door.
                animator.SetTrigger("OpenDoor");
                GameManager.Instance.Money -= doorCost;
                isOpen = true;
            }
        }
    }
}
