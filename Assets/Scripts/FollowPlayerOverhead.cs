using UnityEngine;

public class FollowPlayerOverhead : MonoBehaviour
{
    [Tooltip("The height offset of the particle system above the player.")]
    public float heightOffset = 10f;

    private Transform playerTransform;
    private ParticleSystem rainParticleSystem; // Optional: If you need to access particle system properties

    void Start()
    {
        // Find the player car by its tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found in the scene!");
            enabled = false; // Disable the script if the player is not found
            return;
        }

        // Optional: Get the ParticleSystem component if you need to access its properties
        rainParticleSystem = GetComponent<ParticleSystem>();
        if (rainParticleSystem == null)
        {
            Debug.LogWarning("ParticleSystem component not found on this GameObject.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Update the position of this GameObject (which should have the particle system)
            transform.position = new Vector3(
                playerTransform.position.x,
                playerTransform.position.y + heightOffset, // Adjust height as needed
                playerTransform.position.z
            );
        }
    }
}