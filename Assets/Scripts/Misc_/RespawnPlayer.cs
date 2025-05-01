using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;

    [SerializeField] float waitForCanvasDelay = 2f;
    [SerializeField] float respawnDelay = 2f;

    public PlayerMovement playerMovementScript;

    [SerializeField] public GameObject _respawnMenuFirst;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _respawnPoint;

    [SerializeField] private AudioClip loseSound;


    public float TimeLeft = 10f;
    public bool TimerOn = false;

    public TMP_Text TimerText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WaitForCanvas();
            SoundFXManager.instance.PlaySoundFXClip(loseSound, transform, 1f);
        }
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is up");
                TimeLeft = 0;
                TimerOn = false;
            }         
        }
    }

    private void WaitForCanvas()
    {
        playerMovementScript.enabled = false;
        EventSystem.current.SetSelectedGameObject(_respawnMenuFirst);
        Invoke("OpenRespawnCanvas", waitForCanvasDelay);
    }

    private void OpenRespawnCanvas()
    {
        menuManager._respawnMenuCanvasGO.SetActive(true);
        TimerOn = true;

        Invoke("Respawn", respawnDelay);
    }

    private void Respawn()
    {
        playerMovementScript.enabled = true;
        menuManager._respawnMenuCanvasGO.SetActive(false);
        TimeLeft = 10f;
        TimerOn = false;

        _player.transform.position = _respawnPoint.transform.position;
        Physics.SyncTransforms();
    }

    public void OnRespawnPress()
    {
        CloseRespawnCanvas();
        CancelInvoke();
    }

    private void CloseRespawnCanvas()
    {
        menuManager._respawnMenuCanvasGO.SetActive(false);
        playerMovementScript.enabled = true;

        EventSystem.current.SetSelectedGameObject(menuManager._respawnMenuFirst);
        _player.transform.position = _respawnPoint.transform.position;
        Physics.SyncTransforms();

        TimeLeft = 10f;
        TimerOn = false;
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00}", seconds);
    }


}