using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] InputManager _input;

    [SerializeField] private SpriteRenderer _interactSprite;

    private Transform _playerTransform;

    private const float _interactRange = 5f;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    } 

    private void Update()
    {
        if(_input.InteractionWasPressedThisFrame && IsWithinInteractDistance())
        {
            Interact();
        }

        if (_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            _interactSprite.gameObject.SetActive(false);
        }
        else if(!_interactSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            _interactSprite.gameObject.SetActive(true);
        }
    }

    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        if (Vector3.Distance(_playerTransform.position, transform.position) < _interactRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
