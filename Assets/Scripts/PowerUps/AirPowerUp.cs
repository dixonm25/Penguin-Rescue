using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AirPowerUp : MonoBehaviour
{
    [SerializeField] private float _speedIncreaseAmount = 25f;
    [SerializeField] private float _powerupDuration = 5;


    [SerializeField] private GameObject _artToDisable = null;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider> ();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        if(playerMovement != null)
        {
            StartCoroutine(PowerupSequence(playerMovement));
        }
    }

    public IEnumerator PowerupSequence(PlayerMovement playerMovement)
    {
        // disable art
        _collider.enabled = false;
        _artToDisable.SetActive(false);

      
        ActivatePowerup(playerMovement);
        //wait to deactivate
        yield return new WaitForSeconds(_powerupDuration);
        DeactivatePowerup(playerMovement);

        _collider.enabled = true;
        _artToDisable.SetActive(true);

        // Destroy(gameObject);
    }

    private void ActivatePowerup(PlayerMovement playerMovement)
    {
        playerMovement.SetMoveSpeed(_speedIncreaseAmount);
        playerMovement.SetAirTrail(true);
    }

    private void DeactivatePowerup(PlayerMovement playerMovement)
    {
        playerMovement.SetMoveSpeed(-_speedIncreaseAmount);
        playerMovement.SetAirTrail(false);
        playerMovement._airParticles.Stop();
    }

}
