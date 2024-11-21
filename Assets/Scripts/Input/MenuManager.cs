using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] InputManager _input;

    public PlayerMovement playerMovementScript;
    public CameraController cameraControllerScript;

    [SerializeField] private GameObject _mainMenuCanvasGO;
    [SerializeField] private GameObject _settingsMenuCanvasGO;
    [SerializeField] private GameObject _gamepadMenuCanvasGO;
    [SerializeField] private GameObject _keyboardMenuCanvasGO;
    [SerializeField] public GameObject _respawnMenuCanvasGO;

    //first button selected on open
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _gamepadMenuFirst;
    [SerializeField] private GameObject _keyboardMenuFirst;
    [SerializeField] public GameObject _respawnMenuFirst;

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(false);
        _respawnMenuCanvasGO.SetActive(false);

        playerMovementScript.enabled = true;
        cameraControllerScript.enabled = true;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.MenuOpenCloseWasPressedThisFrame)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        playerMovementScript.enabled = false;
        cameraControllerScript.enabled = false;
        Time.timeScale = 0f;

        OpenMainMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        playerMovementScript.enabled = true;
        cameraControllerScript.enabled = true;
        Time.timeScale = 1f;

        CloseAllMenus();
    }

    private void OpenMainMenu()
    {
        _mainMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(false);
        _respawnMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    private void CloseAllMenus()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(false);
        _respawnMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnSettingsPress()
    {
        OpenSettingsMenu();
    }

    private void OpenSettingsMenu()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(true);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(false);
        _respawnMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);
    }

    public void OnGamepadPress()
    {
        OpenGamepadMenu();
    }

    private void OpenGamepadMenu()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(true);
        _keyboardMenuCanvasGO.SetActive(false);
        _respawnMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_gamepadMenuFirst);
    }

    public void OnKeyboardPress()
    {
        OpenKeyboardMenu();
    }

    private void OpenKeyboardMenu()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        _gamepadMenuCanvasGO.SetActive(false);
        _keyboardMenuCanvasGO.SetActive(true);
        _respawnMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_keyboardMenuFirst);
    }

    public void OnExitGamePress()
    {
        ExitGameButton();
    }

    private void ExitGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnResumePress()
    {
        Unpause();
    }

    public void OpenSettingsBack()
    {
        OpenMainMenu();
    }

    public void OpenSettingsMoreBack()
    {
        OpenSettingsMenu();
    }
}
