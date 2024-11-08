using UnityEngine;

public class PoliceSiren : MonoBehaviour
{
    // AudioSource components for the siren and horn sounds
    private AudioSource sirenAudioSource;
    private AudioSource hornAudioSource;

    // Boolean flag to track if siren is on
    private bool isSirenOn = false;

    void Start()
    {
        // Get the AudioSource components attached to the object
        AudioSource[] audioSources = GetComponents<AudioSource>();

        // Assuming the first AudioSource is for the siren and the second one is for the horn
        sirenAudioSource = audioSources[0];
        hornAudioSource = audioSources[1];

        sirenAudioSource.Stop();
        hornAudioSource.Stop();

        // Set the loop property for siren (since we want it to loop when active)
        sirenAudioSource.loop = true;
    }

    void Update()
    {
        // When Shift is pressed, toggle the siren (start/stop)
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleSiren();
        }

        // When Space is pressed, play the horn sound ("tít còi")
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayHornSound();
        }
    }

    // Toggle siren sound (start/stop)
    void ToggleSiren()
    {
        if (isSirenOn)
        {
            // Stop siren sound
            sirenAudioSource.Stop();
        }
        else
        {
            // Play siren sound
            sirenAudioSource.Play();
        }

        // Toggle the siren state
        isSirenOn = !isSirenOn;
    }

    // Play horn sound ("tít còi")
    void PlayHornSound()
    {
        // Play the horn sound (assuming it plays only once per press)
        hornAudioSource.PlayOneShot(hornAudioSource.clip);
    }
}
