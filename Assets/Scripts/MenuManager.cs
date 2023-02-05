using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Text zenText;
    [SerializeField] private Text kazooText;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Slider volumeSlider;

    private Color _startColor;
    private string _gameMode;
    private float _actualVolume = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Buttons
        _startColor = zenText.color;

        // Volume
        if(volumeSlider != null)
        {
            _actualVolume = PlayerPrefs.GetFloat("volume", volumeSlider.value);
        }

        // Game mode
        _gameMode = PlayerPrefs.GetString("gamemode", null);
        if(_gameMode != null && _gameMode != "") {
            if(continueButton != null) { continueButton.interactable = true; }
        } else {
            _gameMode = "zen";
        }
        SetGameMode(_gameMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameMode(string _newGameMode)
    {
        _gameMode = _newGameMode;

        if(_newGameMode == "zen") {
            AudioManager.instance.SetVolume("Zen", _actualVolume);
            AudioManager.instance.SetVolume("Kazoo", 0f);
            if(zenText != null) { zenText.color = selectedColor; }
        } else {
            if(zenText != null) { zenText.color = _startColor; }
        }

        if(_newGameMode == "kazoo") {
            AudioManager.instance.SetVolume("Kazoo", _actualVolume);
            AudioManager.instance.SetVolume("Zen", 0f);
            if(kazooText != null) { kazooText.color = selectedColor; }
        } else {
            if(kazooText != null) { kazooText.color = _startColor; }
        }
    }

    public void SetVolume()
    {
        _actualVolume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        SetGameMode(_gameMode);
    }

    public void NewGame(string _sceneName)
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("gamemode", _gameMode);
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        ContinueGame(_sceneName);
    }

    public void ContinueGame(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
