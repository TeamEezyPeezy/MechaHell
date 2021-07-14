using UnityEngine;

public class RemoveFuckingShit : MonoBehaviour
{
    void Start()
    {
        //temp to remove unity's debug updater which crashes with new input
        GameObject go = GameObject.Find("[Debug Updater]");
        if (go != null) DestroyImmediate(go);
    }
}
