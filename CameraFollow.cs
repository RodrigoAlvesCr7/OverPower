using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 2f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    // Additional zoom variables
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    void Update()
    {
        // Zoom using the scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newSize = Camera.main.orthographicSize - scroll * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);

        // Follow the target with smoothing
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
