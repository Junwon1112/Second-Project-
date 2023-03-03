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
    /// Awake에 쓸 함수들
    /// </summary>
    private void Initialize()
    {
        _particlesDict.Clear(); //딕셔너리 초기화

        list_Particles.Add(particleBasic);  //리스트에 기본 파티클 추가

        for(int i = 0; i < _particles.Length; i++)  //딕셔너리에 Enum을 키값으로 게임오브젝트(파티클오브젝트)리턴하도록 등록
        {
            _particlesDict.Add((ParticleType)System.Enum.Parse(typeof(ParticleType), _particles[i].name), _particles[i]);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

}
