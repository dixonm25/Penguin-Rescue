using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;

    public PlayerMovement playerMovementScript;

    [SerializeField] public GameObject _finishLevelMenuCanvasFirst;

    [SerializeField] private Transform _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartLevelCanvas();
        }
    }

    private void StartLevelCanvas()
    {
        playerMovementScript.enabled = false;
        menuManager._finishLevelMenuCanvasGO.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_finishLevelMenuCanvasFirst);
    }

    public void OnNextLevelPress()
    {
        NextLevelButton();
    }

    public void OnStartOverPress()
    {
        StartOverButton();
    }

    public void OnSecondStartOverPress()
    {
        SecondStartOverButton();
    }

    private void NextLevelButton()
    {
        SceneManager.LoadScene(1);
    }

    private void StartOverButton()
    {
        SceneManager.LoadScene(2);
    }

    private void SecondStartOverButton()
    {
        SceneManager.LoadScene(1);
    }

    private void ExitGameButton()
    {
        SceneManager.LoadScene(0);
    }
}
