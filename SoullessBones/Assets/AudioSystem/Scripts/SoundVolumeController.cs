using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;

    [Header("Keys")]
    [SerializeField] private string saveMusicVolumeKey;
    [SerializeField] private string saveEffectVolumeKey;

    [Header("Tags")]
    [SerializeField] private string musicSliderTag;
    [SerializeField] private string effectSliderTag;
    
    [Header("Parameters")]
    [SerializeField] private float musicVolume;
    [SerializeField] private float effectVolume;

    private void Awake()
    {
        InitSliders();
        if (PlayerPrefs.HasKey(saveMusicVolumeKey) && PlayerPrefs.HasKey(saveEffectVolumeKey))
        {
            musicVolume = PlayerPrefs.GetFloat(saveMusicVolumeKey);
            effectVolume = PlayerPrefs.GetFloat(saveEffectVolumeKey);
            //effectVolumeSet
        }
        else 
        {
            musicVolume = 0.5f;
            effectVolume = 0.5f;
            PlayerPrefs.SetFloat(saveMusicVolumeKey, musicVolume);
            PlayerPrefs.SetFloat(saveEffectVolumeKey, effectVolume);
            //effectVolumeSet
        }
        audioSource.volume = musicVolume;
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
        audioSource.volume = musicVolume;
    }
    private void effectValueChanged(float a)
    {
        effectVolume = a;
        PlayerPrefs.SetFloat(saveEffectVolumeKey, effectVolume);
        //effectVolumeSet
    }
    private void Update()
    {
        if (musicSlider == null)
        {
            InitSliders();
        }
    }
}
