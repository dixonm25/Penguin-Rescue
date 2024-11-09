using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndMove : MonoBehaviour
{
    Rigidbody _rigidbody = null;

    [SerializeField] bool _moveEnabled = true;
    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _platformDistance = 5.0f;
    Vector3 _startPosition = Vector3.zero;
    Vector3 _endPosition = Vector3.zero;

    Vector3 _platformPositionLastFrame = Vector3.zero;
    float _timeScale = 0.0f;

    Dictionary<Rigidbody, float> RBsOnPlatformAndTime = new Dictionary<Rigidbody, float>();
    [SerializeField] List<Rigidbody> RBsOnPlatform = new List<Rigidbody> ();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _startPosition = _rigidbody.position;
        _endPosition = new Vector3(_startPosition.x + _platformDistance, _startPosition.y + _platformDistance, _startPosition.z);
    }

    private void FixedUpdate()
    {

        if (RBsOnPlatform.Count != RBsOnPlatformAndTime.Count)
        {
            RBsOnPlatformAndTime.Clear();
            foreach (Rigidbody rb in RBsOnPlatform)
            {
                RBsOnPlatformAndTime.Add(rb, 1.0f);
            }
        }

        if(_moveEnabled)
        {
            _platformPositionLastFrame = _rigidbody.position;
            _timeScale = _speed / Vector3.Distance(_startPosition, _endPosition);

            _rigidbody.position = Vector3.Lerp(_endPosition, _startPosition, Mathf.Abs(Time.time * _timeScale % 2 - 1));
        }

        foreach (Rigidbody rb in RBsOnPlatform)
        {
            RBsOnPlatformAndTime.TryGetValue(rb, out float timer);
            if (timer < 1.0f)
            {
                RBsOnPlatformAndTime[rb] += Time.deltaTime * 4.0f;
            }
            else if (timer > 1.0f)
            {
                RBsOnPlatformAndTime[rb] = 1.0f;
            }
            RotateAndMoveRBOnPlatform(rb, timer);
        }
    }

    private void RotateAndMoveRBOnPlatform(Rigidbody rb, float timer)
    { 
        if (_moveEnabled)
        {
            rb.position += (_rigidbody.position - _platformPositionLastFrame) * timer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.attachedRigidbody == null) && !(other.attachedRigidbody.isKinematic))
        {
            if (!(RBsOnPlatform.Contains(other.attachedRigidbody)))
            {
                RBsOnPlatform.Add(other.attachedRigidbody);
                RBsOnPlatformAndTime.Add(other.attachedRigidbody, 0.0f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.attachedRigidbody == null))
        {
            if (RBsOnPlatform.Contains(other.attachedRigidbody))
            {
                RBsOnPlatform.Remove(other.attachedRigidbody);
                RBsOnPlatformAndTime.Remove(other.attachedRigidbody);
            }
        }
    }
}
