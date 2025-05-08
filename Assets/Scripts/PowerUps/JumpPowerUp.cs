using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    [SerializeField] private float _jumpIncreaseAmount = 25f;
    [SerializeField] private float _powerupDuration = 5;


    [SerializeField] private GameObject _artToDisable = null;

    [SerializeField] private AudioClip jumpPickUp;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            StartCoroutine(PowerupSequence(playerMovement));
        }
    }

    public IEnumerator PowerupSequence(PlayerMovement playerMovement)
    {
        // disable art
        _collider.enabled = false;
        _artToDisable.SetActive(false);

        // sound
        SoundFXManager.instance.PlaySoundFXClip(jumpPickUp, transform, 1f);


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
        playerMovement.SetJumpIncrease(_jumpIncreaseAmount);
        playerMovement.SetJumpTrail(true);
    }

    private void DeactivatePowerup(PlayerMovement playerMovement)
    {
        playerMovement.SetJumpIncrease(-_jumpIncreaseAmount);
        playerMovement.SetJumpTrail(false);
        playerMovement._airParticles.Stop();
    }

}
