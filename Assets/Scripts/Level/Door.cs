using System;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, iDoor
{
    public Animator animator;

    public Door doorPair;

    public bool isOpen = false;
    private bool doorPairOpen = false;

    [Tooltip("Cost to open door, for example key / money")]
    public int doorCost = 1;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            if (GameManager.Instance.Money >= doorCost)
            {
                // Open Door.
                if (animator != null)
                {
                    animator.SetTrigger("OpenDoor");
                }

                GameManager.Instance.Money -= doorCost;
                isOpen = true;
            }

            if (!doorPairOpen)
            {
                if (doorPair.animator != null)
                {
                    doorPair.animator.SetTrigger("OpenDoor");
                }

                doorPairOpen = true;
            }
        }
    }
}
