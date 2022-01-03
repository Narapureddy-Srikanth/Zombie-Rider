using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : MonoBehaviour
{
    [SerializeField] Transform camera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
