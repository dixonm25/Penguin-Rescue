using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FirePowerUp : MonoBehaviour
{
    [SerializeField] private float _slopeIncreaseAmount;
    [SerializeField] private float _powerupDuration = 20f;


    [SerializeField] private GameObject _artToDisable = null;

    [SerializeField] private AudioClip firePickUp;

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
        SoundFXManager.instance.PlaySoundFXClip(firePickUp, transform, 1f);

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
        playerMovement.SetSlopeLimit(_slopeIncreaseAmount);
        playerMovement.SetFireTrail(true);
    }

    private void DeactivatePowerup(PlayerMovement playerMovement)
    {
        playerMovement.SetSlopeLimit(-_slopeIncreaseAmount);
        playerMovement.SetFireTrail(false);
        playerMovement._fireParticles.Stop();
    }

}
