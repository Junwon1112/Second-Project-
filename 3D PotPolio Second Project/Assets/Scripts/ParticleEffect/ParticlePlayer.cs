using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief ��ƼŬ �ý����� �����ϴ� ��ũ��Ʈ
 * @details 
 * -�̱����� ���� ����
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
    /// Awake�� �� �Լ���
    /// </summary>
    private void Initialize()
    {

    }
}
