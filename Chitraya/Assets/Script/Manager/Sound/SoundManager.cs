using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Audio Source")]
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("BGM == Audio Clip")]
    public AudioClip bgmClip;
    public AudioClip bossClip;

    [Header("SFX Player== Audio Clip")]
    public AudioClip jump;
    public AudioClip landing;
    public AudioClip melee;
    public AudioClip walk;

    [Header("SFX Enemy== Audio Clip")]
    public AudioClip hit;
    public AudioClip laserMinion;
    public AudioClip stomp;
    public AudioClip charge;
    public AudioClip laserBoss;

    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Slider Volume")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;



    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume") && PlayerPrefs.HasKey("BGMVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetBGMVolume();
            SetSFXVolume();
        }
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmClip != null)
        {
            BGMSource.Stop();
            Debug.Log("bBOS");
            BGMSource.clip = bgmClip;
            BGMSource.Play();
            BGMSource.loop = true;

        }
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }


    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetBGMVolume()
    {
        float volume = bgmSlider.value;
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMasterVolume();
        SetBGMVolume();
        SetSFXVolume();
    }
}
