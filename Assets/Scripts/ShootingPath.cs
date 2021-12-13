using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPath : MonoBehaviour
{
    [SerializeField]
    private Joystick joystickForShootingPath;

    private LineRenderer lineRendererForShootingPath;
    private float thresholdForJoystick;
    private Vector3 offset, vectorPathDirection, liftOffset;
    private float pathLength;
    private bool isActive;
    private Vector3 ifActiveDirection;

    // Start is called before the first frame update
    void Start()
    {
        pathLength = 12f;
        thresholdForJoystick = 0.4f;
        lineRendererForShootingPath = GetComponent<LineRenderer>();
        offset = Vector3.down;
        liftOffset = Vector3.up * 0.1f;
        vectorPathDirection = new Vector3(0f, 0f, 1f);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If joystick is active show the path, else don't show.
        if(isJoystickActive())
        {
            lineRendererForShootingPath.positionCount = 2;
            lineRendererForShootingPath.SetPosition(0, offset);

            Vector3 locationOfJoystick = new Vector3(transform.position.x + HorizontalSpeed(), transform.position.y, transform.position.z + VerticalSpeed());
            transform.LookAt(locationOfJoystick);
            
            // sending raycast in direction of joystick
            RaycastHit hitPoint;
            if (Physics.Raycast(transform.position, transform.forward, out hitPoint, pathLength))
            {
                float lengthOfHitLocation = (hitPoint.point - transform.position).magnitude;
                lineRendererForShootingPath.SetPosition(1, offset + vectorPathDirection * lengthOfHitLocation + liftOffset);
            }
            else
            {
                lineRendererForShootingPath.SetPosition(1, offset + vectorPathDirection * pathLength + liftOffset);
            }
            ifActiveDirection = locationOfJoystick;
            isActive = true;
        }
        else
        {
            lineRendererForShootingPath.positionCount = 0;
            if (isActive)
            {
                // TODO: move this direction and fire
                GameObject playerGameObject = GameObject.Find("Player");
                playerGameObject.transform.LookAt(ifActiveDirection);
                isActive = false;
            }
        }
    }

    /// <summary>
    /// Returns true if joystick is active
    /// </summary>
    private bool isJoystickActive()
    {
        return ((Mathf.Abs(joystickForShootingPath.Horizontal) >= thresholdForJoystick)
            || (Mathf.Abs(joystickForShootingPath.Vertical) >= thresholdForJoystick));
    }


    /// <summary>
    /// For different inputs using mobile or PC provides horizontal input
    /// </summary>
    /// <returns> Horizontal input effect </returns>
    private float HorizontalSpeed()
    {
        return joystickForShootingPath.Horizontal;
    }

    /// <summary>
    /// For different inputs using mobile or PC provides vertical input
    /// </summary>
    /// <returns> vertical input effect </returns>
    private float VerticalSpeed()
    {
        return joystickForShootingPath.Vertical;
    }
}