using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHouse : MonoBehaviour
{
    MainCamera mainCamera;
    uint repeatCount = 5;

    private void Awake()
    {
        mainCamera = FindObjectOfType<MainCamera>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            float targetDistance_X = 0;
            float targetDistance_Y = 1.0f;
            float targetDistance_Z = 0.3f;

            mainCamera.SetCameraDistance(targetDistance_X, targetDistance_Y, targetDistance_Z);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        mainCamera.SetCameraDistance(mainCamera.DefaultCameraPos);
    }

}
