using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform mainPlayerPosition;
    private Vector3 offset;
    private Quaternion quaternionRotation;
    private float smoothSpeed;

    private void Start()
    {
        offset = new Vector3(0f, 8f, -10f);
        quaternionRotation = Quaternion.Euler(30f, 0f, 0f);
        smoothSpeed = 0.15f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Moving camera smoothly relative to the player
        Vector3 desiredPosition = mainPlayerPosition.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Rotating the camera relative to the player
        transform.rotation = quaternionRotation;
    }
}
