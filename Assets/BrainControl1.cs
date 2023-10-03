using UnityEngine;
using brainflow;

public class BrainControl1 : MonoBehaviour
{
    private BoardShim boardShim = null;
    private int samplingRate = 0;
    private GameObject cube;
    private float moveSpeed = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            BoardShim.set_log_file("brainflow_log.txt");
            BoardShim.enable_dev_board_logger();

            BrainFlowInputParams inputParams = new BrainFlowInputParams();
            int boardId = (int)BoardIds.SYNTHETIC_BOARD;
            boardShim = new BoardShim(boardId, inputParams);
            boardShim.prepare_session();
            boardShim.start_stream(450000, "file://brainflow_data.csv:w");
            samplingRate = BoardShim.get_sampling_rate(boardId);
            Debug.Log("Brainflow streaming was started");

            // Get a reference to the cube GameObject
            cube = GameObject.Find("Cube"); // Replace "Cube" with the actual name of your cube
        }
        catch (BrainFlowError e)
        {
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boardShim == null)
        {
            return;
        }
        int numberOfDataPoints = samplingRate * 4;
        double[,] data = boardShim.get_current_board_data(numberOfDataPoints);

        // Check if data is available
        if (data != null && data.GetLength(1) > 0)
        {
            // Use EEG data to control cube's movement
            float signalValue = (float)data[0, 0] * 0.1f; // Adjust the index for the EEG channel you want to use
            signalValue = Mathf.Clamp(signalValue, -1f, 1f);
            Vector3 newPosition = cube.transform.position;
            newPosition.x += signalValue * moveSpeed * Time.deltaTime;
            cube.transform.position = newPosition;
        }
    }

    // OnDestroy is called when the script is destroyed
    private void OnDestroy()
    {
        if (boardShim != null)
        {
            try
            {
                boardShim.release_session();
            }
            catch (BrainFlowError e)
            {
                Debug.Log(e);
            }
            Debug.Log("Brainflow streaming was stopped");
        }
    }
}
