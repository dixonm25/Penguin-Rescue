using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] InputManager _input;

    [SerializeField] private GameObject _mainMenuCanvasGO;
    [SerializeField] private GameObject _interMainMenuCanvasGO;
    [SerializeField] private GameObject _settingsMenuCanvasGO;
    [SerializeField] private GameObject _gamepadMenuCanvasGO;
    [SerializeField] private GameObject _keyboardMenuCanvasGO;

    //first button selected on open
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _interMainMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _gamepadMenuFirst;
    [SerializeField] private GameObject _keyboardMenuFirst;

    // Start is called before the first frame update
    void Start()
    {
       OpenMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMainMenu()
    {
        _mainMenuCanvasGO.SetActive(true);
        _interMainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    public void OpenInterMainMenu()
    {
        _mainMenuCanvasGO.SetActive(false);
        _interMainMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    public void OnInterPress()
    {
        OpenInterMainMenu();
    }

    public void OnStartGamePress()
    {
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void OnQuitGamePress()
    {
        QuitGame();
    }

    private void QuitGame() 
    { 
        Application.Quit(); 
    }

    public void OnSettingsPress()
    {
        OpenSettingsMenu();
    }
    private void OpenSettingsMenu()
    {
        _mainMenuCanvasGO.SetActive(false);
        _interMainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(true);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);
    }

    public void OpenSettingsBack()
    {
        BackButton();
    }

    private void BackButton()
    {
        OpenMainMenu();
    }

    public void OnGamepadPress()
    {
        OpenGamepadMenu();
    }

    private void OpenGamepadMenu()
    {
        _mainMenuCanvasGO.SetActive(false);
        _interMainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(true);
        _keyboardMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_gamepadMenuFirst);
    }

    public void OnKeyboardPress()
    {
        OpenKeyboardMenu();
    }

    private void OpenKeyboardMenu()
    {
        _mainMenuCanvasGO.SetActive(false);
        _interMainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_keyboardMenuFirst);
    }

    public void OpenSettingsMoreBack()
    {
        OpenSettingsMenu();
    }
}
