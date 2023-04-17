using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float moveSpeed_Z = 10.0f;
    private float rotateSpeed = 30.0f;

    Quaternion rotate;

    private void Start()
    {
        rotate = Quaternion.Euler(0, Time.deltaTime * rotateSpeed, 0);
    }

    private void Update()
    {
        transform.localPosition += new Vector3(0, 0, moveSpeed_Z * Time.deltaTime);
        transform.rotation *=  rotate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            Destroy(gameObject, 0.05f);
        }
    }
}
