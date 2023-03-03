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

    protected ParticleObject particleBasic;

    protected List<ParticleObject> list_Particles = new List<ParticleObject>();

    protected Dictionary<ParticleType, GameObject> _particlesDict = new Dictionary<ParticleType, GameObject>();




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
        _particlesDict.Clear(); //��ųʸ� �ʱ�ȭ

        list_Particles.Add(particleBasic);  //����Ʈ�� �⺻ ��ƼŬ �߰�

        for(int i = 0; i < _particles.Length; i++)  //��ųʸ��� Enum�� Ű������ ���ӿ�����Ʈ(��ƼŬ������Ʈ)�����ϵ��� ���
        {
            _particlesDict.Add((ParticleType)System.Enum.Parse(typeof(ParticleType), _particles[i].name), _particles[i]);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

}
