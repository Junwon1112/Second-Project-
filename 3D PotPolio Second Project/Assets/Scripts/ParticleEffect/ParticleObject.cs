using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleObject : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem particleSystemSource;

    IEnumerator particlePlayTime = null;

    /// <summary>
    /// ��ƼŬ �ý����� ������Ƽ�� �����Ϸ��� MainModule�� ���� �����ؾ� �Ѵٰ� �Ѵ�
    /// </summary>
    public ParticleSystem ParticleSystemSource
    {
        get { return particleSystemSource; }
        set { particleSystemSource = value; }
    }

    private void Awake()
    {
        particleSystemSource = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// ��ƼŬ �ý��� ���, ������Ƽ�� �ְ� ���¸� �����ϱ� ���ؼ��� ParticleSystem.Module�� Ÿ������ ������ ���� ������ ��
    /// �ٸ� ����Ƽ ���̵忡 ���� ������Ƽ�� �ý��� ��ü�� ������ �شٰ� �ȳ��Ǿ�����
    /// </summary>
    public void Play()
    {
        particleSystemSource.Play();
        StartCoroutine(DestroyAfterPlay());
    }

    IEnumerator DestroyAfterPlay()
    {
        float lifeTime = particleSystemSource.main.duration;
        yield return new WaitForSeconds(lifeTime);
        DestroyParticle();
        
    }

    public void Pause()
    {
        particleSystemSource.Pause();
    }


    public void Stop()
    {
        particleSystemSource.Stop();
        DestroyParticle();
    }

    public void DestroyParticle()
    {
        try
        {
            Destroy(this.gameObject);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}
