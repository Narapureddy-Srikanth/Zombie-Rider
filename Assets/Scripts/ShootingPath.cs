using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPath : MonoBehaviour
{
    [SerializeField]
    private Joystick joystickForShootingPath;
    [SerializeField]
    private Transform bullet;
    [SerializeField]
    private Transform shootingPosition;

    private LineRenderer lineRendererForShootingPath;
    private Animator playerAnimator;
    private float thresholdForJoystick;
    private Vector3 offset, vectorPathDirection, liftOffset;
    private float pathLength;
    private float minJoystickMovement, defaultMinJoystickMovement;
    private Vector3 ifActiveDirection;

    // Start is called before the first frame update
    void Start()
    {
        lineRendererForShootingPath = GetComponent<LineRenderer>();
        playerAnimator = GetComponentInParent<Animator>();
        pathLength = 12f;
        thresholdForJoystick = 0.3f;
        defaultMinJoystickMovement = 0.35f;
        minJoystickMovement = 0f;
        offset = Vector3.down;
        liftOffset = Vector3.up * 0.1f;
        vectorPathDirection = new Vector3(0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerAnimator.GetBool("isFiring") == true)
        {
            playerAnimator.SetBool("isFiring", false);
        }
        // If joystick is active show the path, else don't show.
        if (isJoystickActive())
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
            minJoystickMovement = Mathf.Max(Mathf.Abs(HorizontalSpeed()), Mathf.Abs(VerticalSpeed()));
        }
        else
        {
            lineRendererForShootingPath.positionCount = 0;
            // If previous joystick movement is greater the default joystick movement, we fire.
            if (minJoystickMovement >= defaultMinJoystickMovement)
            {
                // Move the player towards direction of firing
                GameObject playerGameObject = GameObject.Find("Player");
                playerGameObject.transform.LookAt(ifActiveDirection + Vector3.down);
                transform.LookAt(ifActiveDirection);
                minJoystickMovement = 0f;

                // initiate a bullet
                Instantiate(bullet, shootingPosition.position, shootingPosition.transform.rotation);

                // Add fire animation
                playerAnimator.SetBool("isFiring", true);
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
