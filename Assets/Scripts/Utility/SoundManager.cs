using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    public struct BGMInfo
    {
        public eBGM type;
        public AudioClip clip;
        [Range(0.0f, 1.0f)]
        public float volume;
        public bool isLoopMusic;
        public float startSecond;
        public float endSecond;
    }

    [Serializable]
    public struct SFXInfo
    {
        public eSFX type;
        public AudioClip clip;
        [Range(0.0f, 1.0f)]
        public float volume;
    }

    [Serializable]
    public struct BGMPlayer
    {
        public AudioSource audioSource;
        public float volume;
        public int startSample;
        public int endSample;
        public bool isLoop;
    }

    [SerializeField]
    private List<BGMInfo> m_bgmList;
    [SerializeField]
    private List<SFXInfo> m_sfxList;
    [SerializeField]
    private List<BGMPlayer> m_multiBgmPlayer;
    [SerializeField]
    private List<AudioSource> m_multiSfxPlayer;

    private List<BGMPlayer> m_currentPlayers = new List<BGMPlayer>();
    private List<AudioSource> m_currentSFXPlayers = new List<AudioSource>();

    private float m_bgmVolume;
    private float m_sfxVolume;
    //[SerializeField] private AudioMixerGroup m_bgmGroup;
    //[SerializeField] private AudioMixerGroup m_sfxGroup;

    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < m_multiSfxPlayer.Count; i++)
        {
            m_multiSfxPlayer[i] = gameObject.AddComponent<AudioSource>(); ;
        }
        SoundPlayer.BGM_Volume = m_bgmVolume = m_multiBgmPlayer.First().volume;
        SoundPlayer.SFX_Volume = m_sfxVolume = m_multiSfxPlayer.First().volume;
        //m_multiBgmPlayer.ForEach(player => { player.audioSource.outputAudioMixerGroup = m_bgmGroup; });

        //m_multiSfxPlayer.ForEach(player => { player.outputAudioMixerGroup = m_sfxGroup; });

    }

    // Update is called once per frame
    void Update()
    {
        if (SoundPlayer.BGM_Volume >= 0.01f)
        {
            m_multiBgmPlayer.ForEach(player => { player.audioSource.mute = false; });
            m_multiBgmPlayer.ForEach(player => { player.volume = SoundPlayer.BGM_Volume; player.audioSource.volume = player.audioSource.volume / m_bgmVolume * player.volume; });
            m_currentPlayers.ForEach(player => { player.audioSource.mute = false; });
            m_currentPlayers.ForEach(player => { player.volume = SoundPlayer.BGM_Volume; player.audioSource.volume = player.audioSource.volume / m_bgmVolume * player.volume; });

            m_bgmVolume = SoundPlayer.BGM_Volume;
        }
        else
        {
            m_multiBgmPlayer.ForEach(player => { player.audioSource.mute = true; });
            m_multiBgmPlayer.ForEach(player => { player.volume = 0.0f; player.audioSource.volume = 0.0f; });
            m_currentPlayers.ForEach(player => { player.audioSource.mute = true; });
            m_currentPlayers.ForEach(player => { player.volume = 0.0f; player.audioSource.volume = 0.0f; });
            m_bgmVolume = 0.0f;

        }
        if (SoundPlayer.SFX_Volume >= 0.01f)
        {
            m_multiSfxPlayer.ForEach(player => { player.mute = false; });
            m_multiSfxPlayer.ForEach(player => { player.volume = SoundPlayer.SFX_Volume; player.volume = player.volume / m_sfxVolume * player.volume; });
            m_currentSFXPlayers.ForEach(player => { player.mute = false; });
            m_currentSFXPlayers.ForEach(player => { player.volume = SoundPlayer.SFX_Volume; player.volume = player.volume / m_sfxVolume * player.volume; });

            m_sfxVolume = SoundPlayer.SFX_Volume;
        }
        else
        {
            m_multiSfxPlayer.ForEach(player => { player.mute = true; });
            m_multiSfxPlayer.ForEach(player => { player.volume = 0.0f; player.volume = 0.0f; });
            m_currentSFXPlayers.ForEach(player => { player.mute = true; });
            m_currentSFXPlayers.ForEach(player => { player.volume = 0.0f; player.volume = 0.0f; });

            m_sfxVolume = 0.0f;
        }
        //m_bgmGroup.audioMixer.SetFloat("BGM", VolumeToDB(SoundPlayer.BGM_Volume));
        //m_sfxGroup.audioMixer.SetFloat("SFX", VolumeToDB(SoundPlayer.SFX_Volume));
        SoundPlayer.BGM_Volume = m_bgmVolume;
        SoundPlayer.SFX_Volume = m_sfxVolume;

        if (m_currentPlayers.Count == 0) return;
        if (m_currentPlayers.First().audioSource.timeSamples >= m_currentPlayers.First().endSample - CalculateSample(0.05f) && m_currentPlayers.First().isLoop)
        {
            var unusedBgmPlayer = GetUnUsedBGMPlayer();

            unusedBgmPlayer.audioSource.clip = m_currentPlayers[0].audioSource.clip;
            unusedBgmPlayer.volume = m_currentPlayers[0].volume;
            unusedBgmPlayer.audioSource.volume = m_currentPlayers[0].audioSource.volume;
            unusedBgmPlayer.startSample = m_currentPlayers[0].startSample;
            unusedBgmPlayer.endSample = m_currentPlayers[0].endSample;
            unusedBgmPlayer.isLoop = m_currentPlayers[0].isLoop;
            unusedBgmPlayer.audioSource.Play();
            unusedBgmPlayer.audioSource.timeSamples = unusedBgmPlayer.startSample;
            //unusedBgmPlayer.audioSource.outputAudioMixerGroup = m_bgmGroup;
            m_currentPlayers.Add(unusedBgmPlayer);
            m_currentPlayers.Remove(m_currentPlayers.First());
            //m_currentPlayer.audioSource.Stop();
        }
    }

    public BGMInfo GetBGM(eBGM type)
    {
        return m_bgmList.Find(bgm => bgm.type == type);
    }

    public SFXInfo GetSFX(eSFX type)
    {
        return m_sfxList.Find(sfx => sfx.type == type);
    }

    public void PlayBGM(in eBGM type)
    {
        var bgmPlayer = GetUnUsedBGMPlayer();
        var bgm = GetBGM(type);
        bgmPlayer.audioSource.clip = bgm.clip;
        bgmPlayer.volume = bgm.volume;
        bgmPlayer.audioSource.volume = m_bgmVolume;
        if (bgm.isLoopMusic)
        {
            if (bgm.endSecond <= 0)
                bgm.endSecond = float.Parse(bgm.clip.length.ToString("f2")) - 0.01f;
            Debug.Log($"End:{bgm.endSecond}");
            bgmPlayer.endSample = CalculateSample(bgm.endSecond);
            bgmPlayer.startSample = CalculateSample(bgm.startSecond);
        }
        bgmPlayer.isLoop = bgm.isLoopMusic;
        bgmPlayer.audioSource.loop = true;
        //bgmPlayer.audioSource.outputAudioMixerGroup = m_bgmGroup;

        bgmPlayer.audioSource.Play();
        m_currentPlayers.Add(bgmPlayer);
    }

    public int CalculateSample(in float second)
    {
        float secondToMill = second * 100;
        return (int)secondToMill * 441;
    }

    public void StopBGM()
    {
        if (m_currentPlayers.Count == 0) return;
        m_currentPlayers.Find(player => player.audioSource.isPlaying).audioSource.Stop();
    }

    public void StopSFX()
    {
        if (m_currentSFXPlayers.Count == 0) return;
        m_currentSFXPlayers.Find(player => player.loop).Stop();
    }


    public void PlaySFX(in eSFX type, in bool isLoop = false)
    {
        var sfx = GetSFX(type);
        AudioSource sfxPlayer = GetUnUsedSFXPlayer();
        if (isLoop)
        {
            sfxPlayer.loop = true;
            sfxPlayer.clip = sfx.clip;
            sfxPlayer.volume = m_sfxVolume * sfx.volume;
            sfxPlayer.Play();
            m_currentSFXPlayers.Add(sfxPlayer);
        }
        else
        {
            sfxPlayer.PlayOneShot(sfx.clip, m_sfxVolume * sfx.volume);
        }
    }

    public AudioSource GetUnUsedSFXPlayer()
    {
        return m_multiSfxPlayer.Find(player => player.isPlaying == false);
    }

    public BGMPlayer GetUnUsedBGMPlayer()
    {
        return m_multiBgmPlayer.Find(player => player.audioSource.isPlaying == false);
    }
    public BGMPlayer GetUsingBGMPlayer()
    {
        return m_multiBgmPlayer.Find(player => player.audioSource.isPlaying == true);
    }

    private float VolumeToDB(float volume) { return Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f); }

}


public enum eBGM
{
    TITLE,
    PLAY,
    RESULT,
}

public enum eSFX
{
    BLADE,
    BOW,
    CHANGE_WEAPON,
    DAMAGE,
    DECISION,
    DROP,
    FLAME,
    GET_EXP,
    HIGH_SPEED,
    WATER,
    OK,
    PINCHI,
}
