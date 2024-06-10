using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;


public class SelectionTextHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _hoverText;
    private float hoverStartTime = 0f;
    private float sceneStartTime = 0f;

    private void Awake()
    {
        if (_hoverText == null)
        {
            _hoverText = GetComponentInChildren<TextMeshPro>();
        }

        // Ensure the text is initially hidden
        _hoverText.gameObject.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hoverStartTime = 0f;
        sceneStartTime = Time.time;// Reset hover start time when a new scene is loaded
    }

    public void ShowHoverText()
    {
        _hoverText.gameObject.SetActive(true);
        _hoverText.text = "SELECTED";
        _hoverText.color = Color.yellow;

        hoverStartTime = Time.time - sceneStartTime; // Store the current time
        SaveHoverStartTimeToFile();
    }

    public void HideHoverText()
    {
        _hoverText.gameObject.SetActive(false);
    }
    private void SaveHoverStartTimeToFile()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string data = $"{sceneName},{gameObject.name},{hoverStartTime:F2}";

        string filePath = Path.Combine(Application.persistentDataPath, $"{sceneName}_hover_start_times.txt");

        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.WriteLine("Scene Name,Object Name,Hover Start Time (s)");
            }
        }

        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine(data);
        }

        Debug.Log($"Hover start time for {gameObject.name} recorded at {hoverStartTime:F2} seconds. Data saved to file.");
    }
}



