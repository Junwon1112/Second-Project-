using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 파티클 시스템을 관리하는 스크립트
 * @details 
 * -싱글톤을 통해 구현
 * -파티클을 생성한 뒤 트랜스폼을 시전 물체에 종속시킨다 vs 시전 물체의 트랜스폼(위치, 회전)만 받고 종속시키진 않는다.
 * -2개버전 만들어야 할듯? 버프 효과는 물체 종속, 단순 이펙트는 종속 x
 * 
 * -재사용 vs 1회 사용 후 삭제
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

    /// <summary>
    /// 타입명을 통해 딕셔너리에서 게임오브젝트를 리턴받고 해당 오브젝트를 트랜스폼 파라미터에 종속시킬지 위치 값만 받을지 정한다
    /// </summary>
    /// <param name="particleType">만들고 싶은 파티클에 해당되는 타입명 </param>
    /// <param name="parentTransform">만들어진 파티클의 부모가 될 오브젝트</param>
    /// /// <param name="hasParent">트랜스폼을 오브젝트의 부모로 설정할지 말지</param>
    /// <returns></returns>
    protected ParticleObject CreateParticleObject(ParticleType particleType, Transform parentTransform, bool hasParent)
    {
        _particlesDict.TryGetValue(particleType, out GameObject gameObj);   //프리팹에 직접 접근함

        GameObject newParticleObj = Instantiate(gameObj, parentTransform);
        ParticleObject particleObj = newParticleObj.AddComponent<ParticleObject>();
        if (hasParent)
        {
            newParticleObj.transform.SetParent(parentTransform, false);   
        }

        return particleObj;
    }

    public void PlayParticle(ParticleType particleType, Transform parentTransform, bool hasParent)
    {
        ParticleObject particleObj = CreateParticleObject(particleType, parentTransform, hasParent);

        particleObj.Play();
    }



}
