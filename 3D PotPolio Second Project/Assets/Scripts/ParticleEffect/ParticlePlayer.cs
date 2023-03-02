using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 파티클 시스템을 관리하는 스크립트
 * @details 
 * -싱글톤을 통해 구현
 * 
 * 
 */
public class ParticlePlayer : MonoBehaviour
{
    public static ParticlePlayer Instance { get; private set; }

    [SerializeField]
    protected GameObject[] _particles; 

    protected Dictionary<string, GameObject> _particlesDict = new Dictionary<string, GameObject>();




    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != null)
            {
                Destroy(this.gameObject);
            }
        }

        Initialize();
    }

    /// <summary>
    /// Awake에 쓸 함수들
    /// </summary>
    private void Initialize()
    {

    }
}
