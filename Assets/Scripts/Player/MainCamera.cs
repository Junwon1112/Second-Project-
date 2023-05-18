using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    float position_X;
    float position_Y;
    float position_Z;

    Vector3 defaultCameraPos;
    Vector3 cameraDistance;

    

    public float Position_X { get; set; }
    public float Position_Y { get; set; }
    public float Position_Z { get; set; }

    public Vector3 DefaultCameraPos { get; private set; }
    public Vector3 CameraDistance { get; set; }

    private void Start()
    {
        SettingDefaultPosition();
    }

    public void SetCameraDistance(float _x, float _y, float _z)
    {
        position_X = _x; 
        position_Y = _y; 
        position_Z = _z;
        CameraDistance = new Vector3(position_X, position_Y, position_Z);
        transform.localPosition = CameraDistance;
    }

    public void SetCameraDistance(Vector3 _position)
    {
        CameraDistance = _position;
        transform.localPosition = CameraDistance;
    }

    private void SettingDefaultPosition()
    {
        DefaultCameraPos = transform.localPosition;
    }
}
