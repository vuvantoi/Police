using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
    // Reference to the Light component
    private Light emergencyLight;

    // Frequency of flashing (time in seconds)
    public float flashInterval = 0.5f;
    private float nextFlashTime = 0f;

    // Start with the light turned off
    private bool lightOn = false;

    void Start()
    {
        // Get the Light component attached to the object
        emergencyLight = GetComponent<Light>();
        emergencyLight.enabled = false;  // Start with the light off
    }

    void Update()
    {
        // If it's time to flash, toggle the light on/off
        if (Time.time >= nextFlashTime)
        {
            // Toggle light
            lightOn = !lightOn;
            emergencyLight.enabled = lightOn;

            // Set the next flash time
            nextFlashTime = Time.time + flashInterval;
        }
    }
}
