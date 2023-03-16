using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SoundVolumeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource[] audioSources;

    [Header("Keys")]
    [SerializeField] private string saveMusicVolumeKey;
    [SerializeField] private string saveEffectVolumeKey;

    [Header("Tags")]
    [SerializeField] private string musicSliderTag;
    [SerializeField] private string effectSliderTag;

    [Header("Parameters")]
    [SerializeField] private float musicVolume;
    [SerializeField] private float effectVolume;
    [SerializeField] private float swapTime = 2;
    [SerializeField] private AudioClip[] c_clips;
    [SerializeField] private AudioClip[] c_battleClips;
    [SerializeField] bool randomize = false;

    private Slider musicSlider;
    private Slider effectSlider;
    private AudioClip c_currentClip;
    private int c_index = 0;
    private int a_indexGlobal = 0;
    private int a_indexLocal = 0;
    private bool inSwap = false;
    private enum states {normal, battle};
    private states state = states.normal;

    public static SoundVolumeController instance;

    private void Awake()
    {
        instance = this;
        InitSliders();
        if (PlayerPrefs.HasKey(saveMusicVolumeKey) && PlayerPrefs.HasKey(saveEffectVolumeKey))
        {
            musicVolume = PlayerPrefs.GetFloat(saveMusicVolumeKey);
            effectVolume = PlayerPrefs.GetFloat(saveEffectVolumeKey);
        }
        else
        {
            musicVolume = 0.5f;
            effectVolume = 0.5f;
            PlayerPrefs.SetFloat(saveMusicVolumeKey, musicVolume);
            PlayerPrefs.SetFloat(saveEffectVolumeKey, effectVolume);
        }
        if (randomize) Shuffle(c_clips);

        a_indexLocal = a_indexGlobal % audioSources.Length;
        c_currentClip = c_clips[c_index];
        audioSources[a_indexLocal].clip = c_currentClip;

        audioSources[a_indexLocal].Play();
        SetMusicVolume(0, musicVolume);
        SetMusicVolume(1, musicVolume);
    }
    private void InitSliders()
    {
        if (GameObject.FindWithTag(musicSliderTag) == null) return;

        musicSlider = GameObject.FindWithTag(musicSliderTag).GetComponent<Slider>();
        musicSlider.value = musicVolume;
        musicSlider.onValueChanged.AddListener(musicValueChanged);

        effectSlider = GameObject.FindWithTag(effectSliderTag).GetComponent<Slider>();
        effectSlider.value = effectVolume;
        effectSlider.onValueChanged.AddListener(effectValueChanged);

    }
    private void musicValueChanged(float a)
    {
        musicVolume = a;
        PlayerPrefs.SetFloat(saveMusicVolumeKey, musicVolume);
        SetMusicVolume(0, musicVolume);
        SetMusicVolume(1, musicVolume);
    }
    private void effectValueChanged(float a)
    {
        effectVolume = a;
        PlayerPrefs.SetFloat(saveEffectVolumeKey, effectVolume);
        //effectVolumeSet
    }
    private void SetMusicVolume(int a, float volume)
    {
        audioSources[a].volume = volume;
    }
    
    private void SwitchToBattleLocal(int a)
    {
        state = states.battle;
        c_currentClip = c_battleClips[a];
        StartCoroutine(SwitchSource(c_currentClip));
    }
    private void SwitchToNormalLocal()
    {
        state = states.normal;
        c_index++;
        c_currentClip = c_clips[c_index % c_clips.Length];
        StartCoroutine(SwitchSource(c_currentClip));
    }
    //перемешать массив bg клипов
    public void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
    private void Update()
    {
        if (musicSlider == null)
        {
            InitSliders();
        }
        //Debug.Log(audioSources[a_indexLocal].time);

        if(!inSwap && state == states.normal && c_currentClip.length < audioSources[a_indexLocal].time + swapTime)
        {
            c_index++;
            c_currentClip = c_clips[c_index % c_clips.Length];
            StartCoroutine(SwitchSource(c_currentClip));
        }
    }
    //плавный переход между клипами
    IEnumerator SwitchSource(AudioClip clip)
    {
        inSwap = true;
        int a_indexLocalPrevious = a_indexLocal;
        a_indexGlobal++;
        a_indexLocal = a_indexGlobal % audioSources.Length;

        audioSources[a_indexLocal].clip = c_currentClip;
        audioSources[a_indexLocal].Play();

        int points = 10;
        for (int i = 0; i < points-1; i++)
        {
            SetMusicVolume(a_indexLocalPrevious, (1f - (float)i / points) * musicVolume);
            SetMusicVolume(a_indexLocal, (float)i / points * musicVolume);
            yield return new WaitForSeconds(swapTime / points);
        }
        SetMusicVolume(a_indexLocalPrevious, 0);
        SetMusicVolume(a_indexLocal, musicVolume);
        inSwap = false;
    }

    public static void SwitchToBattle(int a = 0)
    {
        instance.SwitchToBattleLocal(a);
    }
    public static void SwitchToNormal()
    {
        instance.SwitchToNormalLocal();
    }
}
