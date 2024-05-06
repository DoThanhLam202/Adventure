using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    Vector2 startPos;
    float startZ;
    Vector2 canMoveSinceStart =>(Vector2) cam.transform.position - startPos;
    float dis => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (dis > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(dis) / clippingPlane;
    private void Start()
    {
        startPos = transform.position;
        startZ = transform.localPosition.z;
    }

    private void Update()
    {
        Vector2 newPos = startPos + canMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
