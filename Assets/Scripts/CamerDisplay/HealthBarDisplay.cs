using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : MonoBehaviour
{
    [SerializeField] Transform cam;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
