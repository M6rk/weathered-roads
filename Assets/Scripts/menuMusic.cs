using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; 
using UnityEngine.UI; 

public class menuMusic : MonoBehaviour
{
    [Header("UI Sounds")]
    [SerializeField] public AudioSource menuSoundSource; 
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip invalidClickSound; 

    [Header("Volume Control")]
    [SerializeField] private float masterVolume = 1f;
    private AudioSource[] allAudioSources;

    private GameObject lastHoveredButton;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayHoverSound()
    {
        if (menuSoundSource != null && hoverSound != null)
        {
            menuSoundSource.PlayOneShot(hoverSound,3f);
        }
    }

    public void PlayClickSound()
    {
        if (menuSoundSource != null && clickSound != null)
        {
            menuSoundSource.PlayOneShot(clickSound,3f);
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            UpdateMasterVolume(masterVolume);
        }
    }

    public void UpdateMasterVolume(float volume)
    {
        masterVolume = volume;
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();

        allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            source.volume = masterVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "TrackOne" || currentScene == "TrackTwo" || currentScene == "TrackThree")
        {
            Destroy(gameObject);
            return;
        }

        if (Input.GetMouseButtonDown(0) && EventSystem.current != null)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            bool clickedButton = false;
            foreach (var result in results)
            {
                if (result.gameObject.CompareTag("button"))
                {
                    PlayClickSound();
                    clickedButton = true;
                    break;
                }
            }

            if (!clickedButton && menuSoundSource != null && invalidClickSound != null)
            {
                menuSoundSource.PlayOneShot(invalidClickSound, 2f);
            }
        }

        if (EventSystem.current != null)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            GameObject currentHoveredObject = null;
            foreach (var result in results)
            {
                if (result.gameObject.CompareTag("button"))
                {
                    currentHoveredObject = result.gameObject;
                    break;
                }
            }

            if (currentHoveredObject != null && currentHoveredObject != lastHoveredButton)
            {
                PlayHoverSound();
                lastHoveredButton = currentHoveredObject;
            }
            else if (currentHoveredObject == null)
            {
                lastHoveredButton = null;
            }
        }
    }
}
