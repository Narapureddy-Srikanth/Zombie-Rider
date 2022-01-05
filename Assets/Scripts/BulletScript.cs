using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private Transform hitVFXEffect;
    [SerializeField]
    private Transform noHitVFXEffect;

    private float bulletSpeed;
    private float pathLength;
    private Vector3 startPositionOfBullet, endPositionOfBullet;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 60f;
        pathLength = 12f;
        startPositionOfBullet = transform.position;
        endPositionOfBullet = transform.position + transform.forward * pathLength;
    }

    // Update is called once per frame
    void Update()
    {
        float updatedBulletSpeed = bulletSpeed;

        // To check if next position is out of pathLength, if going outside will reduce the speed in last update
        Vector3 nextPositionOfBullet = transform.position + transform.forward * bulletSpeed * Time.deltaTime;
        if(Vector3.Magnitude(nextPositionOfBullet - startPositionOfBullet) > pathLength)
        {
            updatedBulletSpeed = Mathf.Max(5f, Vector3.Magnitude(endPositionOfBullet - transform.position) / Time.deltaTime);
        }
        transform.Translate(Vector3.forward * updatedBulletSpeed * Time.deltaTime);

        if (Vector3.Magnitude(transform.position - startPositionOfBullet) >= pathLength)
        {
            Transform noHitEffect = Instantiate(noHitVFXEffect, transform.position, Quaternion.identity);
            Destroy(noHitEffect.gameObject, 1f);
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform hitEffect = Instantiate(hitVFXEffect, transform.position, Quaternion.identity);
        Destroy(hitEffect.gameObject, 1f);
        Destroy(gameObject);
    }
}
