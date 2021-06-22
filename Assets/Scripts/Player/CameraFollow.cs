using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera cam;
    public Transform player;

    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main; 
        }
    }

    void Update()
    {
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
