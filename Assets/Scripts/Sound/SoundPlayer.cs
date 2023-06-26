using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �÷��̾� Ŭ����
/// �̱���
/// 1���� �⺻ SoundObject���, 2�� �̻� ���� ���� ó���� ��� ���� ����
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance { get; private set; }

    [SerializeField]
    protected AudioSource audioSource_bgm = null;

    [SerializeField]
    protected SoundObject audio_basic = null;

    [SerializeField]
    protected AudioClip[] audioClips_Effect = null;

    [SerializeField]
    protected AudioClip[] audioClips_BGM = null;

    protected List<SoundObject> list_Sound = new List<SoundObject>();

    protected Dictionary<SoundType_BGM, AudioClip> dic_BGMSound = new Dictionary<SoundType_BGM, AudioClip>();

    protected Dictionary<SoundType_Effect, AudioClip> dic_EffectSound = new Dictionary<SoundType_Effect, AudioClip>();

    protected readonly string SOUND_NAME = "_Sound";

    protected readonly float BGM_VOLUME = 0.25f;

    protected readonly float Effect_VOLUME = 0.75f;

    protected float bgmCurrentVolume = 1.0f;

    protected float effectCurrentVolume = 1.0f;

    public bool IsBgmPlaying
    {
        get
        {
            return audioSource_bgm.isPlaying;
        }
    }

    public float BGMCurrentVolume
    {
        get { return bgmCurrentVolume; }
        set
        {
            if (value > 1)
            {
                bgmCurrentVolume = 1.0f;
            }
            else if (value < 0)
            {
                bgmCurrentVolume = 0.0f;
            }
            else
            {
                bgmCurrentVolume = value;
            }

        }
    }
    public float EffectCurrentVolume
    {
        get { return effectCurrentVolume; }
        set
        {
            if (value > 1)
            {
                effectCurrentVolume = 1.0f;
            }
            else if (value < 0)
            {
                effectCurrentVolume = 0.0f;
            }
            else
            {
                effectCurrentVolume = value;
            }

        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        dic_EffectSound.Clear ();
        dic_BGMSound.Clear();


        list_Sound.Add(audio_basic);

        for (int i = 0; i < audioClips_BGM.Length; i++)
        {
            //Enum.Parse�� Type enumType(Enum�� �̸�), string value(�ش� Enum�� ��� string �̸�))
            //�� ���� object�� �����ϰ� ĳ������ ���ϴ� Ÿ������ ���� �� �ִ�
            //��ϵ� �����Ŭ���� �̸��� Enum�� ������ �����Ƿ� �����Ŭ�� ���ϸ�� Enum����� �̸��� ���ƾ��Ѵ�
            dic_BGMSound.Add((SoundType_BGM)System.Enum.Parse(typeof(SoundType_BGM), audioClips_BGM[i].name), audioClips_BGM[i]);
        }

        for (int i = 0; i < audioClips_Effect.Length; i++)
        {
            //Enum.Parse�� Type enumType(Enum�� �̸�), string value(�ش� Enum�� ��� string �̸�))
            //�� ���� object�� �����ϰ� ĳ������ ���ϴ� Ÿ������ ���� �� �ִ�
            //��ϵ� �����Ŭ���� �̸��� Enum�� ������ �����Ƿ� �����Ŭ�� ���ϸ�� Enum����� �̸��� ���ƾ��Ѵ�
            dic_EffectSound.Add((SoundType_Effect)System.Enum.Parse(typeof(SoundType_Effect), audioClips_Effect[i].name), audioClips_Effect[i]);
        }

        InitializeVolume();

    }

    /// <summary>
    /// �� �޼���� �� MonoBehavior�� ����� �� ȣ��
    /// </summary>
    private void OnDestroy()
    {
        Instance = null;
    }

    public void InitializeVolume()
    {
        BGMVolumeChange(BGM_VOLUME);
        EffectVolumeChange(Effect_VOLUME);
    }

    public void BGMVolumeChange(float _ChangeVolume)
    {
        BGMCurrentVolume = _ChangeVolume;

        audioSource_bgm.volume = BGMCurrentVolume;
    }

    public void EffectVolumeChange(float _ChangeVolume)
    {
        effectCurrentVolume = _ChangeVolume;

        foreach (SoundObject sound in list_Sound)
        {
            sound.AudioSource.volume = effectCurrentVolume;
        }

    }


    public bool IsPause()
    {
        if (Time.timeScale <= 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// ���� ��� ����
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        foreach (SoundObject sound in list_Sound)
        {
            if (sound.IsPlaying)
            {
                return true;
            }
            
        }
        return false;
    }

    /// <summary>
    /// ���� ��� ����
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    public bool IsPlaying(AudioClip clip)
    {
        foreach (SoundObject sound in list_Sound)
        {
            if(sound.AudioClip == clip)
            {
                if(sound.IsPlaying)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// ���� Ÿ�Կ� ���� ���� Ŭ���� �����ϴ� �޼���
    /// </summary>
    /// <param name="type">� Ÿ�Կ� ���Ѱ� ���ϴ���</param>
    /// <returns>���� Ÿ�Կ� �����ϴ� ����� Ŭ��</returns>
    public AudioClip GetSoundClip_Effect(SoundType_Effect type)
    {
        if (dic_EffectSound.ContainsKey(type))
        {
            return dic_EffectSound[type];
        }
        else
        {
            return null;
        }
    }

    public AudioClip GetSoundClip_BGM(SoundType_BGM type)
    {
        if (dic_BGMSound.ContainsKey(type))
        {
            return dic_BGMSound[type];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// ������� ����ϴ� �޼���
    /// </summary>
    /// <param name="clip">� ����� Ŭ������</param>
    /// <param name="volume">������ ������ ����</param>
    /// <param name="isloop">������ ����</param>
    public void PlayBGM(AudioClip clip, float volume, bool isloop)
    {
        if(clip == null)
        {
            Debug.Log("BGM clip is null");
            return;
        }
        if(IsBgmPlaying)
        {
            audioSource_bgm.Stop();
        }

        audioSource_bgm.clip = clip;
        audioSource_bgm.volume = volume;
        audioSource_bgm.loop = isloop;
        audioSource_bgm.Play();
    }

    /// <summary>
    /// ������� ����ϴ� �޼���
    /// </summary>
    public void PlayBGM(AudioClip clip, float volume)
    {
        PlayBGM(clip, volume, true);
    }

    /// <summary>
    /// ������� ����ϴ� �޼���
    /// </summary>
    public void PlayBGM(AudioClip clip)
    {
        PlayBGM(clip, BGM_VOLUME);
    }

    public void PlayBGM()
    {
        PlayBGM(audioSource_bgm.clip);
    }

    public void PlayBGM(SoundType_BGM soundType)
    {
        PlayBGM(GetSoundClip_BGM(soundType));
    }

    /// <summary>
    /// ��� �� ����
    /// </summary>
    public void StopBGM()
    {
        audioSource_bgm.Stop();
    }

    /// <summary>
    /// ��� �� ����
    /// </summary>
    public void PauseBGM()
    {
        if(audioSource_bgm.isPlaying)
        {
            audioSource_bgm.Pause();
        }
    }

    /// <summary>
    /// �ٽ� ���
    /// </summary>
    public void UnpauseBGM()
    {
        audioSource_bgm.UnPause();
    }

    /// <summary>
    /// ���ο� ���� ������Ʈ �������ִ� �޼���
    /// </summary>
    /// <param name="clip">�ش� ������Ʈ�� �� ����� Ŭ��</param>
    /// <returns>������ ���� ������Ʈ ����</returns>
    protected SoundObject CreateSoundObject(AudioClip clip)
    {
        GameObject Obj = new GameObject(SOUND_NAME);
        Obj.transform.SetParent(audio_basic.transform);
        SoundObject soundObj = Obj.AddComponent<SoundObject>();
        soundObj.AudioClip = clip;
        return soundObj;
    }

    

    public void UnpauseAllSound(bool includeBGM = true)
    {
        foreach(SoundObject soundObject in list_Sound)
        {
            soundObject.UnPause();
        }
        if(includeBGM)
        {
            audioSource_bgm.UnPause();
        }
    }

    public void PauseAllSound(bool includeBGM = true)
    {
        foreach(SoundObject soundObj in list_Sound)
        {
            soundObj.Pause();
        }
        if (includeBGM)
        {
            audioSource_bgm.Pause();
        }
    }

    public bool StopSound(AudioClip clip)
    {
        for(int i = 0; i < list_Sound.Count; i++)
        {
            if (list_Sound[i].AudioClip == clip)
            {
                list_Sound[i].Stop();

                if(i != 0)
                {
                    list_Sound.Remove(list_Sound[i]);
                }
                return true;
            }
        }

        return false;
    }

    public bool StopAllSound(bool includeBGM)
    {
        foreach(SoundObject soundObject in list_Sound)
        {
            soundObject.Stop();
        }

        if(includeBGM)
        {
            audioSource_bgm.Stop();
        }

        ClearAllSound();

        return true;
    }

    protected void ClearAllSound()
    {
        list_Sound.Clear();
        list_Sound.Add(audio_basic);
    }

    public bool StopAllSOund()
    {
        return StopAllSound(false);
    }


    /// <summary>
    /// ���� �÷���, �ϳ� �̻� ������� �� ���ο� ������Ʈ ����, �ƴϸ� ���� basic���� ���
    /// </summary>
    /// <param name="clip">����� ����� Ŭ��</param>
    /// <param name="volume">������ ������ ����</param>
    /// <param name="delaySeconds">���ʰ� ������ �Ŀ� �������</param>
    /// <param name="isLoop">������ ����</param>
    /// <param name="isStoppable">����� �ִ���</param>
    /// <param name="finishListener">���� �� ������ ��������Ʈ</param>
    public void PlaySound(AudioClip clip, /*float volume ,*/ float delaySeconds, bool isLoop, bool isStoppable, System.Action finishListener)
    {
        if (clip == null)
        {
            Debug.Log("No Sound Clip");
            return;
        }

        if (IsPlaying() || IsPause())
        {
            SoundObject obj_Sound = CreateSoundObject(clip);

            list_Sound.Add(obj_Sound);
            obj_Sound.Play(effectCurrentVolume, delaySeconds, isLoop, isStoppable, () =>
            {
                list_Sound.Remove(obj_Sound);
                if (finishListener != null)
                {
                    finishListener();
                }
            });
        }
        else
        {
            audio_basic.AudioClip = clip;
            audio_basic.Play(effectCurrentVolume, delaySeconds, isLoop, isStoppable, () =>
            {
                if (finishListener != null)
                {
                    finishListener();
                }
            });

        }
    }

    public void PlaySound(AudioClip clip, /*float volume,*/ float delaySeconds, bool isLoop, System.Action finishListener)
    {
        PlaySound(clip, /*volume,*/delaySeconds, isLoop, true, finishListener);
    }

    public void PlaySound(AudioClip clip, /*float volume,*/ float delaySeconds, System.Action finishListener)
    {
        PlaySound(clip, /*volume,*/ delaySeconds, false, finishListener);
    }

    public void PlaySound(AudioClip clip, /*float volume,*/ System.Action finishListener)
    {
        PlaySound(clip, /*volume,*/ 0, false, finishListener);
    }


    public void PlaySound(SoundType_Effect soundType/*, float volume*/)
    {
        if (soundType == SoundType_Effect.None)
        {
            return;
        }

        PlaySound(GetSoundClip_Effect(soundType), /*volume,*/ null);
    }

    public void PlaySound(SoundType_Effect soundType, bool isStoppable)
    {
        if(soundType == SoundType_Effect.None)
        {
            return;
        }

        PlaySound(GetSoundClip_Effect(soundType), /*1.0f,*/ 0, false, isStoppable, null);
    }

    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, null);
    }

    //public void PlaySound(SoundType soundType)
    //{
    //    PlaySound(soundType, true);
    //}

    public IEnumerator CoPlaySound(AudioClip clip, float minWaitSec = 0.5f)
    {
        float waitSec = (clip != null && clip.length > minWaitSec) ? clip.length : minWaitSec;
        PlaySound(clip, null);
        yield return new WaitForSeconds(waitSec);
    }
}