using System.IO;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;

    // File path to save the time data
    private string filePath;

    void Start()
    {
        startTime = Time.time;

        // Set the file path where time data will be saved
        filePath = Path.Combine(Application.dataPath, "elapsed_time.txt");
        Debug.Log("File path: " + filePath);
    }

    void Update()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
    }

    // Method to save the elapsed time to a file
    public void SaveElapsedTime()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                float elapsedTime = Time.time - startTime;
                writer.WriteLine(elapsedTime);
            }
            Debug.Log("Elapsed time saved to file: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving elapsed time: " + e.Message);
        }
    }
}

