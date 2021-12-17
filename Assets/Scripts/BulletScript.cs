using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private CharacterController bulletController;
    private float bulletSpeed;
    private float pathLength;

    // Start is called before the first frame update
    void Start()
    {
        bulletController = GetComponent<CharacterController>();
        bulletSpeed = 100f;
        pathLength = 12f;
    }

    // Update is called once per frame
    void Update()
    {
        bulletController.Move(transform.forward * bulletSpeed * Time.deltaTime);
        pathLength -= bulletSpeed * Time.deltaTime;
        if (pathLength <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Destroy(gameObject);
    }
}
