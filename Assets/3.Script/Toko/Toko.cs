using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toko : MonoBehaviour
{
    public enum TokoState { Run, Jumping, Die }

    [SerializeField] private TokoState _tokoState;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;

    private float _jumpForce = 1000f;
    private Animator _animator;
    [SerializeField] private LayerMask _tileLayer;
    private int _jumpCount = 1;

    [SerializeField] private CheckCol _checkCol;

    private void Awake()
    {
        TryGetComponent(out _rigidbody);
        TryGetComponent(out _animator);
        TryGetComponent(out _audioSource);
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnUpdate()
    {
        switch (_tokoState)
        {
            case TokoState.Run:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(TokoState.Jumping);
                }
                break;
            case TokoState.Jumping:
                GroundCheck();
                break;
        }
    }


    private void OnEnter()
    {
        switch (_tokoState)
        {
            case TokoState.Run:
                _jumpCount++;
                break;
            case TokoState.Jumping:
                _animator.SetTrigger("Jump");
                Jump();
                break;
            case TokoState.Die:
                Die();
                break;
            default:
                break;
        }
    }

    public void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.8f, _tileLayer);
        _animator.SetFloat("Fall", _rigidbody.velocity.y);
        if (_checkCol.canJump! && _jumpCount.Equals(0))
        {
            ChangeState(TokoState.Run);
        }

    }

    private void Die()
    {
        _animator.SetTrigger("Die");
        _audioSource.PlayOneShot(AudioManager.Instance._SfxClips[1]);
        Time.timeScale = 0f;
    }

    private void Jump()
    {
        _checkCol.canJump = false;
        _audioSource.PlayOneShot(AudioManager.Instance._SfxClips[0]);
        _jumpCount--;
        _rigidbody.AddForce(Vector2.up * _jumpForce);

    }

    public void ChangeState(TokoState startstate)
    {
        if (startstate.Equals(_tokoState)) return;

        _tokoState = startstate;
        OnEnter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            ChangeState(TokoState.Die);
        }
    }

}
