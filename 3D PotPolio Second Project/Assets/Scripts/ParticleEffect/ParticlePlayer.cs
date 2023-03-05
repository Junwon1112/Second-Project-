using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief ��ƼŬ �ý����� �����ϴ� ��ũ��Ʈ
 * @details 
 * -�̱����� ���� ����
 * -��ƼŬ�� ������ �� Ʈ�������� ���� ��ü�� ���ӽ�Ų�� vs ���� ��ü�� Ʈ������(��ġ, ȸ��)�� �ް� ���ӽ�Ű�� �ʴ´�.
 * -2������ ������ �ҵ�? ���� ȿ���� ��ü ����, �ܼ� ����Ʈ�� ���� x
 * 
 * -���� vs 1ȸ ��� �� ����
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

    /// <summary>
    /// Ÿ�Ը��� ���� ��ųʸ����� ���ӿ�����Ʈ�� ���Ϲް� �ش� ������Ʈ�� Ʈ������ �Ķ���Ϳ� ���ӽ�ų�� ��ġ ���� ������ ���Ѵ�
    /// </summary>
    /// <param name="particleType">����� ���� ��ƼŬ�� �ش�Ǵ� Ÿ�Ը� </param>
    /// <param name="parentTransform">������� ��ƼŬ�� �θ� �� ������Ʈ</param>
    /// /// <param name="hasParent">Ʈ�������� ������Ʈ�� �θ�� �������� ����</param>
    /// <returns></returns>
    protected ParticleObject CreateParticleObject(ParticleType particleType, Transform parentTransform, bool hasParent)
    {
        _particlesDict.TryGetValue(particleType, out GameObject gameObj);   //�����տ� ���� ������

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
