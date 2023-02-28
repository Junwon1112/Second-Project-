using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance { get; private set; }

    [SerializeField]
    protected AudioSource audioSource_bgm = null;

    [SerializeField]
    protected SoundObject audio_basic = null;

    [SerializeField]
    protected AudioClip[] audioClips_Effect = null;

    protected List<SoundObject> list_Sound = new List<SoundObject> ();

    protected Dictionary<SoundType, AudioClip> dic_EffectSound = new Dictionary<SoundType, AudioClip>();

    protected readonly string SOUND_NAME = "_Sound";

    protected readonly float BGM_VOLUME = 0.5f;


    private void Awake()
    {
        Instance = this;

        dic_EffectSound.Clear ();

        list_Sound.Add(audio_basic);

        for(int i = 0; i < audioClips_Effect.Length; i++)
        {
            //Enum.Parse는 (Type enumType(Enum의 이름), string value(해당 Enum의 멤버 string 이름))
            //을 통해 object로 리턴하고 캐스팅해 원하는 타입으로 받을 수 있다
            dic_EffectSound.Add((SoundType)System.Enum.Parse(typeof(SoundType), audioClips_Effect[i].name), audioClips_Effect[i]);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public bool IsBgmPlaying
    {
        get
        {
            return audioSource_bgm.isPlaying;
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

    public AudioClip GetSoundClip(SoundType type)
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


    public void PlayBGM(AudioClip clip, float volume)
    {
        PlayBGM(clip, volume, true);
    }

    public void PlayBGM(AudioClip clip)
    {
        PlayBGM(clip, BGM_VOLUME);
    }

    public void StopBGM()
    {
        audioSource_bgm.Stop();
    }

    public void PauseBGM()
    {
        if(audioSource_bgm.isPlaying)
        {
            audioSource_bgm.Pause();
        }
    }

    public void UnpauseBGM()
    {
        audioSource_bgm.UnPause();
    }

    protected SoundObject CreateSoundObject(AudioClip clip)
    {
        GameObject Obj = new GameObject(SOUND_NAME);
        Obj.transform.SetParent(audio_basic.transform);
        SoundObject soundObj = Obj.AddComponent<SoundObject>();
        soundObj.AudioClip = clip;
        return soundObj;
    }


    public void PlaySound(AudioClip clip, float volume, float delaySeconds, bool isLoop, bool isStoppable, System.Action finishListener)
    {
        if(clip == null)
        {
            Debug.Log("No Sound Clip");
            return;
        }

        if(IsPlaying() || IsPause())
        {
            SoundObject obj_Sound = CreateSoundObject(clip);

            list_Sound.Add(obj_Sound);
            obj_Sound.Play(volume, delaySeconds, isLoop, isStoppable, () =>
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
            audio_basic.Play(volume, delaySeconds, isLoop, isStoppable, () =>
            {
                if(finishListener != null)
                {
                    finishListener();
                }
            });
            
        }
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

    public void PlaySound(AudioClip clip, float volume, float delaySeconds, bool isLoop, System.Action finishListener)
    {
        PlaySound(clip, volume, delaySeconds, isLoop, true, finishListener);
    }

    public void PlaySound(AudioClip clip, float volume, float delaySeconds, System.Action finishListener)
    {
        PlaySound(clip, volume, delaySeconds, false, finishListener);
    }

    public void PlaySound(AudioClip clip, float volume, System.Action finishListener)
    {
        PlaySound(clip, volume, 0, false, finishListener);
    }

    public void PlaySound(AudioClip clip, System.Action finishListener)
    {
        PlaySound(clip, 1f, finishListener);
    }

    public void PlaySound(SoundType soundType, float volume)
    {
        if (soundType == SoundType.None)
        {
            return;
        }

        PlaySound(GetSoundClip(soundType), volume, null);
    }

    public void PlaySound(SoundType soundType, bool isStoppable)
    {
        if(soundType == SoundType.None)
        {
            return;
        }

        PlaySound(GetSoundClip(soundType), 1.0f, 0, false, isStoppable, null);
    }

    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, null);
    }

    public void PlaySound(SoundType soundType)
    {
        PlaySound(soundType, true);
    }

    public IEnumerator CoPlaySound(AudioClip clip, float minWaitSec = 0.5f)
    {
        float waitSec = (clip != null && clip.length > minWaitSec) ? clip.length : minWaitSec;
        PlaySound(clip, null);
        yield return new WaitForSeconds(waitSec);
    }
}