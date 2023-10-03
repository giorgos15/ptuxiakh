/*using UnityEngine;

public class BrainControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    // Variables for synthetic EEG simulation
    private float currentTime = 0f;
    public float signalFrequency = 1.0f; // Adjust this for signal frequency
    public float signalAmplitude = 1.0f; // Adjust this for signal amplitude

    private void Update()
    {
        // Generate synthetic EEG-like signal
        float syntheticSignal = GenerateSyntheticSignal();

        // Control cube's movement based on the synthetic signal
        Vector3 newPosition = transform.position;
        newPosition.x += syntheticSignal * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    // Generate synthetic EEG-like signal
    float GenerateSyntheticSignal()
    {
        currentTime += Time.deltaTime;
        float syntheticSignal = Mathf.Sin(2 * Mathf.PI * signalFrequency * currentTime) * signalAmplitude;
        return syntheticSignal;
    }
}*/
