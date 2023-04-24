using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public enum State
{
    WALK,
    RUN,
}
public class PlayerController : MonoBehaviour
{
    private ContinuousMoveProviderBase _continuousMoveProvider;
    private State _playerState;
    #region Ω∫≈»
    private float _walkSpeed;
    private float _runSpeed;
    #endregion

    public InputActionProperty leftControllerThumbstickClick;
    

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRunState();

        switch (_playerState)
        {
            case State.WALK:
                UpdateWalk();
                break;
            case State.RUN:
                UpdateRun();
                break;
        }
    }

    private void Init()
    {
        _continuousMoveProvider = GetComponent<ContinuousMoveProviderBase>();
        _playerState = State.WALK;

        _walkSpeed = _continuousMoveProvider.moveSpeed;
        _runSpeed = _walkSpeed * 3;
    }

    private void CheckRunState()
    {
        if (leftControllerThumbstickClick.action.ReadValue<float>() > 0)
        {
            Debug.Log("Clicked");
            if (_playerState != State.RUN)
                _playerState = State.RUN;
        }

        else
        {
            if(_playerState != State.WALK)
                _playerState = State.WALK;
        }
    }

    private void UpdateRun()
    {
        if(_continuousMoveProvider.moveSpeed < _runSpeed)
            _continuousMoveProvider.moveSpeed = _runSpeed;
    }

    private void UpdateWalk()
    {
        if (_continuousMoveProvider.moveSpeed >= _runSpeed)
            _continuousMoveProvider.moveSpeed = _walkSpeed;
    }
}
