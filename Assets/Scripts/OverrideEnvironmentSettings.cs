using UnityEngine;

public class OverrideEnvironmentSettings : MonoBehaviour
{
    [Header("Skybox")]
    public Material weatherSkybox;

    [Header("Ambient Lighting")]
    public UnityEngine.Rendering.AmbientMode ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
    public Color ambientColor = Color.gray;
    [Range(0f, 8f)]
    public float ambientIntensity = 1f;

    [Header("Reflections")]
    public UnityEngine.Rendering.DefaultReflectionMode defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Skybox;
    [Range(0f, 5f)]
    public float reflectionIntensity = 1f;

    [Header("Directional Light")]
    public Light directionalLight; // Assign your main directional light here
    public Color directionalLightColor = new Color(0.5f, 0.5f, 0.5f);
    [Range(0f, 1f)]
    public float directionalLightIntensity = 0.2f; // Reduced intensity for gloomy rain

    [Header("Fog")]
    public bool enableFog = true;
    public Color fogColor = new Color(0.6f, 0.6f, 0.6f);
    public FogMode fogMode = FogMode.Linear;
    public float fogDensity = 0.01f;
    public float fogStartDistance = 0f;
    public float fogEndDistance = 300f;

    void Start()
    {
        if (weatherSkybox != null)
        {
            RenderSettings.skybox = weatherSkybox;
        }

        RenderSettings.ambientMode = ambientMode;
        if (ambientMode == UnityEngine.Rendering.AmbientMode.Flat)
        {
            RenderSettings.ambientLight = ambientColor * ambientIntensity;
        }
        else
        {
            RenderSettings.ambientIntensity = ambientIntensity;
        }

        RenderSettings.defaultReflectionMode = defaultReflectionMode;
        RenderSettings.reflectionIntensity = reflectionIntensity;

        // Control Directional Light
        if (directionalLight != null)
        {
            directionalLight.color = directionalLightColor;
            directionalLight.intensity = directionalLightIntensity;
        }

        // Control Fog
        RenderSettings.fog = enableFog;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogMode = fogMode;
        if (fogMode == FogMode.Linear)
        {
            RenderSettings.fogStartDistance = fogStartDistance;
            RenderSettings.fogEndDistance = fogEndDistance;
        }
        else if (fogMode == FogMode.Exponential || fogMode == FogMode.ExponentialSquared)
        {
            RenderSettings.fogDensity = fogDensity;
        }

        Debug.Log("Environment settings overridden by weather scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}