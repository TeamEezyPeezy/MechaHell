using System;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public GameObject keycardEffect;

    private void OnDestroy()
    {
        if (keycardEffect != null)
        {
            GameObject de = Instantiate(keycardEffect, transform.position, Quaternion.identity);
            Destroy(de.gameObject, 3f);
        }
    }
}
