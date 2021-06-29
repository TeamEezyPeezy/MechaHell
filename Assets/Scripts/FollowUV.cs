using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUV : MonoBehaviour
{

    public float parallax = 2;

    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.GetTextureOffset("_BaseMap");

        offset.x = transform.position.x / transform.localScale.x / parallax;
        offset.y = transform.position.y / transform.localScale.y / parallax;

        mat.mainTextureOffset = offset;
    }
}
