using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundVolumeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource[] audioSourcesBG;
    [SerializeField] private AudioSource audioSourcesEffects;
    [SerializeField] private AudioSource audioSourcesEffects2;
    [SerializeField] private AudioSource audioSourcesEffectsLong;

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
    [SerializeField] private float pauseSwapTime = 1;
    [SerializeField] private AudioClip[] c_clips;
    [SerializeField] private AudioClip[] c_battleClips;
    [SerializeField] private AudioClip c_menuClip;
    [SerializeField] private AudioClip[] c_effects;
    [SerializeField] private AudioClip[] c_effects2;
    [SerializeField] private AudioClip[] c_longEffects;
    [SerializeField] bool randomize = false;

    private Slider musicSlider;
    private Slider effectSlider;
    private AudioClip c_currentClip;
    private int c_index = 0;
    private int a_indexGlobal = 0;
    private int a_indexLocal = 0;
    private float dopMusicVolume = 1f;
    private bool inSwap = false;
    private enum states {normal, battle, menu};
    private states state = states.menu;

    public static SoundVolumeController instance;

    private void Awake()
    {
        instance = this;
        
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

        a_indexLocal = a_indexGlobal % audioSourcesBG.Length;
        //c_currentClip = c_clips[c_index];
        c_currentClip = c_menuClip;
        audioSourcesBG[a_indexLocal].clip = c_currentClip;

        audioSourcesBG[a_indexLocal].Play();
        musicValueChanged(musicVolume);
        effectValueChanged(effectVolume);
    }
    private void InitSlidersLocal()
    {
        if (GameObject.FindWithTag(musicSliderTag) == null || musicSlider != null) return;

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
        audioSourcesEffects.volume = a;
        audioSourcesEffects2.volume = a;
        audioSourcesEffectsLong.volume = a;
    }
    private void SetMusicVolume(int a, float volume)
    {
        audioSourcesBG[a].volume = volume;
    }
    
    private void SwitchToBattleLocal(int a)
    {
        state = states.battle;
        StopCoroutine(SwitchSource(c_currentClip));
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
    private void SwitchToMenuLocal()
    {
        state = states.menu;
        StopCoroutine(SwitchSource(c_currentClip));
        c_currentClip = c_menuClip;
        StartCoroutine(SwitchSource(c_currentClip));
    }
    private void PlaySoundEffectLocal(int a)
    {
        audioSourcesEffects.clip = c_effects[a];
        audioSourcesEffects.Play();
    }
    private void PlaySoundEffectLocal2(int a)
    {
        audioSourcesEffects2.clip = c_effects2[a];
        audioSourcesEffects2.Play();
    }
    private void PlayLongEffectLocal(bool a, int n)
    {
        audioSourcesEffectsLong.clip = c_longEffects[n];
        if (a)
            audioSourcesEffectsLong.Play();
        else
            audioSourcesEffectsLong.Stop();
    }
    private void PauseMusicLocal(bool a)
    {
        StartCoroutine(FadeSource(a));
    }
    private void LoadToSceneLocal(string s)
    {
        //string s = SceneManager.GetActiveScene().name;
        if (audioSourcesEffectsLong.mute == true) audioSourcesEffectsLong.mute = false;
        if (s == "Menu" && state != states.menu)
        {
            SwitchToMenuLocal();
        }
        else if (s == "Titrs" && state != states.menu)
        {
            SwitchToMenuLocal();
            audioSourcesEffectsLong.mute = true;
        }
        else if(state != states.normal)
        {
            SwitchToNormalLocal();
        }
    }
    //���������� ������ bg ������
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
        if(!inSwap && state == states.normal && c_currentClip.length < audioSourcesBG[a_indexLocal].time + swapTime)
        {
            c_index++;
            c_currentClip = c_clips[c_index % c_clips.Length];
            StartCoroutine(SwitchSource(c_currentClip));
        }
        else if (!inSwap && state == states.battle && c_currentClip.length < audioSourcesBG[a_indexLocal].time + swapTime)
        {
            StartCoroutine(SwitchSource(c_currentClip));
        }
        else if(state == states.menu && c_currentClip.length < audioSourcesBG[a_indexLocal].time + swapTime)
        {
            StartCoroutine(SwitchSource(c_currentClip));
        }

    }
    //������� ������� ����� �������
    IEnumerator SwitchSource(AudioClip clip)
    {
        inSwap = true;
        int a_indexLocalPrevious = a_indexLocal;
        a_indexGlobal++;
        a_indexLocal = a_indexGlobal % audioSourcesBG.Length;

        audioSourcesBG[a_indexLocal].clip = c_currentClip;
        audioSourcesBG[a_indexLocal].Play();

        float points = 10;
        for (int i = 0; i < points; i++)
        {
            SetMusicVolume(a_indexLocalPrevious, (1f - i / points) * musicVolume * dopMusicVolume);
            SetMusicVolume(a_indexLocal, i / points * musicVolume * dopMusicVolume);
            yield return new WaitForSeconds(swapTime / points);
        }
        SetMusicVolume(a_indexLocalPrevious, 0);
        SetMusicVolume(a_indexLocal, musicVolume * dopMusicVolume);
        inSwap = false;
    }

    //������� ��������� � �����
    public IEnumerator FadeSource(bool a)
    {
        inSwap = true;

        float points = 10;
        if (!a) audioSourcesBG[a_indexLocal].UnPause();
        for (int i = 0; i < points; i++)
        {
            dopMusicVolume = a ? (1f - i / points) : (i / points);
            SetMusicVolume(a_indexLocal, musicVolume * dopMusicVolume);
            yield return new WaitForSecondsRealtime(pauseSwapTime / points);
        }
        SetMusicVolume(a_indexLocal, a ? 0 : musicVolume);
        if (a) audioSourcesBG[a_indexLocal].Pause();
        
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

    /// <summary>
    /// 0 - open door,
    /// 1 - slash,
    /// 2 - tear,
    /// 3 - death,
    /// 4 - jump,
    /// 5 - land,
    /// 6 - item
    /// </summary>
    public static void PlaySoundEffect(int a)
    {
        instance.PlaySoundEffectLocal(a);
    }
    public static void PlaySoundEffect2(int a)
    {
        instance.PlaySoundEffectLocal2(a);
    }
    /// <summary>
    /// 0 - run,
    /// 1 - slide,
    /// </summary>
    public static void PlayLongEffect(bool a, int n)
    {
        instance.PlayLongEffectLocal(a, n);
    }
    public static void InitSliders()
    {
        instance.InitSlidersLocal();
    }
    public static void PauseMusic(bool a)
    {
        instance.PauseMusicLocal(a);
    }
    public static void LoadToScene(string s)
    {
        instance.LoadToSceneLocal(s);
    }
}
