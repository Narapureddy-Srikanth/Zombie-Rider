using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Joystick joystickController;
    private CharacterController playerCharacterController;
    private int playerSpeed;
    private int rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacterController = GetComponent<CharacterController>();
        playerSpeed = 100;
        rotationSpeed = 500;
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement using simple move, which by default includes gravify force
        Vector3 directionMovement = new Vector3(HorizontalSpeed(), 0f, VerticalSpeed());
        directionMovement.Normalize();
        playerCharacterController.SimpleMove(directionMovement * playerSpeed * Time.deltaTime);

        if (directionMovement.magnitude > 0)
        {
            // Along with movement, we rotate the player towards the movement with a smooth transition
            Quaternion quaternion = Quaternion.LookRotation(directionMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, rotationSpeed * Time.deltaTime);
        }
    }


    /// <summary>
    /// For different inputs using mobile or PC provides horizontal input
    /// </summary>
    /// <returns> Horizontal input effect </returns>
    private float HorizontalSpeed()
    {
        if(joystickController.Horizontal != 0)
        {
            return joystickController.Horizontal;
        }
        return Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// For different inputs using mobile or PC provides vertical input
    /// </summary>
    /// <returns> vertical input effect </returns>
    private float VerticalSpeed()
    {
        if(joystickController.Vertical != 0)
        {
            return joystickController.Vertical;
        }
        return Input.GetAxis("Vertical");
    }
}
