using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSetting : MonoBehaviour
{
    private void Awake()
    {
        SetLight();
    }

    public void SetLight()
    {
        if(RenderSettings.ambientIntensity > 0)
        {
            RenderSettings.ambientIntensity = 0.0f;
        }
        else
        {
            RenderSettings.ambientIntensity = 1.0f;
        }
        
    }
}
