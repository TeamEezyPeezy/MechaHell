using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Door : MonoBehaviour, iDoor
{
    private GameManager gameManager;
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
        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }

        doorLights = GetComponentsInChildren<Light2D>();
        doorCloseColor = Color.red;
        doorOpenColor = Color.green;
    }

    private void OnEnable()
    {
        GameManager.onDoorOpen += this.OpenDoorOverride;
    }

    private void OpenDoorOverride()
    {
        canFade = true;
        SetAnimTriggers();
    }

    private void OnDisable()
    {
        GameManager.onDoorOpen -= this.OpenDoorOverride;
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
            isOpen = true;
        }

        if (!doorPairOpen)
        {
            if (doorPair.animator != null)
            {
                doorPair.animator.SetTrigger("OpenDoor");
                doorPairOpen = true;
                doorPair.isOpen = true;
            }
        }

        GameManager.Instance.RoomsOpen++;
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            if (GameManager.Instance.Keycards >= doorCost)
            {
                print("Opening door...");
                SetAnimTriggers();
                Invoke("PlayDoorSound", doorSoundDelay);

                canFade = true;
                GameManager.Instance.Keycards -= doorCost;
            }
        }
    }

    private void PlayDoorSound()
    {
        DoorOpenAudioSource.Play();
    }
}
