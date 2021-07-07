using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Door : MonoBehaviour, iDoor
{
    public Animator animator;

    public AudioSource DoorOpenAudioSource;

    public Door doorPair;

    private Light2D[] doorLights;
    private Color doorOpenColor;
    private Color doorCloseColor;

    private bool canFade;
    private float fadeTime = .75f;
    private float doorSoundDelay = .55f;
    private float fadeStart;

    public bool isOpen = false;
    private bool doorPairOpen = false;

    [Tooltip("Cost to open door, for example key / money")]
    public int doorCost = 1;

    private void Awake()
    {
        doorLights = GetComponentsInChildren<Light2D>();
        doorCloseColor = Color.red;
        doorOpenColor = Color.green;
    }

    private void Start()
    {
        canFade = false;
        fadeStart = 0;
    }

    private void Update()
    {
        if (canFade)
        {
            if (fadeStart < fadeTime)
            {
                fadeStart += Time.deltaTime * fadeTime;

                foreach (var doorLight in doorLights)
                {
                    doorLight.color = Color.Lerp(doorCloseColor, doorOpenColor, fadeStart); ;
                }

                foreach (var doorLight in doorPair.doorLights)
                {
                    doorLight.color = Color.Lerp(doorCloseColor, doorOpenColor, fadeStart); ;
                }
            }
        }
    }

    private void SetAnimTriggers()
    {
        if (animator != null)
        {
            animator.SetTrigger("OpenDoor");
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

    public void OpenDoor()
    {
        if (!isOpen)
        {
            if (GameManager.Instance.Money >= doorCost)
            {
                // Open Door.
                SetAnimTriggers();

                Invoke("PlayDoorSound", doorSoundDelay);

                // Set doors to green lights
                canFade = true;

                GameManager.Instance.Money -= doorCost;

                isOpen = true;
            }
        }
    }

    private void PlayDoorSound()
    {
        DoorOpenAudioSource.Play();
    }
}
