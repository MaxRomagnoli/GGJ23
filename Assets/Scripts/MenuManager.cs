using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;

    [Header("Game mode")]
    [SerializeField] private Text zenText;
    [SerializeField] private Text kazooText;
    [SerializeField] private Color selectedColor;
    private Color _startColor;
    private string _gameMode;

    // Start is called before the first frame update
    void Start()
    {
        // Buttons
        _startColor = zenText.color;

        // Game mode
        _gameMode = PlayerPrefs.GetString("gamemode", null);
        continueButton.interactable = _gameMode != null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameMode(string _newGameMode)
    {
        _gameMode = _newGameMode;
        zenText.color = _newGameMode == "zen" ? selectedColor : _startColor;
        kazooText.color = _newGameMode == "kazoo" ? selectedColor : _startColor;
        newGameButton.interactable = true;
    }

    public void NewGame(string _sceneName)
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("gamemode", _gameMode);
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
