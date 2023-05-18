using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    public bool Grounded { private set; get; }
    public bool Standing { private set; get; }

    [SerializeField] private bool _isWalking;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField][Range(0f, 1f)] private float _runstepLenghten;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _stickToGroundForce;
    [SerializeField] private float _gravityMultiplier;
    [SerializeField] private bool _useFovKick;
    [SerializeField] private bool _useHeadBob;
    [SerializeField] private float _stepInterval;
    [SerializeField] private AudioClip[] _footstepSounds;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip _jumpSound;           // the sound played when character leaves the ground.
    [SerializeField] private AudioClip _landSound;           // the sound played when character touches back on ground.

    private Camera _mainCamera;
    private bool _jumpedThisFrame;
    private Vector2 _inputVector;
    private Vector3 _moveDir = Vector3.zero;
    private CharacterController _controller;
    public CharacterController GetCharacterController() { return _controller; }

    private CollisionFlags _collisionFlags;
    private bool _previouslyGrounded;
    private float _stepCycle;
    private float _nextStep;
    private bool _jumping;
    private AudioSource _audioSource;

    public Vector3 GetMoveDirection() { return _moveDir; }

    private void Start()
    {
        Instance = this;

        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        _stepCycle = 0f;
        _nextStep = _stepCycle / 2f;
        _jumping = false;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_jumpedThisFrame)
        {
            _jumpedThisFrame = Input.GetKeyDown(KeyCode.Space);
        }

        if (!_previouslyGrounded && _controller.isGrounded)
        {
            PlayLandingSound();
            _moveDir.y = 0f;
            _jumping = false;
        }
        if (!_controller.isGrounded && !_jumping && _previouslyGrounded)
        {
            _moveDir.y = 0f;
        }

        Grounded = _controller.isGrounded;
        _previouslyGrounded = _controller.isGrounded;
    }

    private void PlayLandingSound()
    {
        _audioSource.clip = _landSound;
        _audioSource.Play();
        _nextStep = _stepCycle + .5f;
    }

    private void FixedUpdate()
    {
        GetInput(out float speed);
        Vector3 desiredMove = _mainCamera.transform.forward * _inputVector.y + _mainCamera.transform.right * _inputVector.x;

        Standing = desiredMove == Vector3.zero;
        if (desiredMove != Vector3.zero) transform.rotation = Quaternion.LookRotation(desiredMove);

        Physics.SphereCast(transform.position, _controller.radius, Vector3.down, out RaycastHit hitInfo,
                           _controller.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        _moveDir.x = desiredMove.x * speed;
        _moveDir.z = desiredMove.z * speed;

        if (_controller.isGrounded)
        {
            _moveDir.y = -_stickToGroundForce;

            if (_jumpedThisFrame)
            {
                _moveDir.y = _jumpSpeed;
                PlayJumpSound();
                _jumpedThisFrame = false;
                _jumping = true;
            }
        }
        else
        {
            _moveDir += _gravityMultiplier * Time.fixedDeltaTime * Physics.gravity;
        }
        _collisionFlags = _controller.Move(_moveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
    }

    private void PlayJumpSound()
    {
        _audioSource.clip = _jumpSound;
        _audioSource.Play();
    }

    private void ProgressStepCycle(float speed)
    {
        if (_controller.velocity.sqrMagnitude > 0 && (_inputVector.x != 0 || _inputVector.y != 0))
        {
            _stepCycle += (_controller.velocity.magnitude + (speed * (_isWalking ? 1f : _runstepLenghten))) *
                         Time.fixedDeltaTime;
        }

        if (!(_stepCycle > _nextStep))
        {
            return;
        }

        _nextStep = _stepCycle + _stepInterval;

        PlayFootStepAudio();
    }

    private void PlayFootStepAudio()
    {
        if (!_controller.isGrounded)
        {
            return;
        }
        int n = UnityEngine.Random.Range(1, _footstepSounds.Length);
        _audioSource.clip = _footstepSounds[n];
        _audioSource.PlayOneShot(_audioSource.clip);
        _footstepSounds[n] = _footstepSounds[0];
        _footstepSounds[0] = _audioSource.clip;
    }

    private void GetInput(out float speed)
    {
        _isWalking = !Input.GetKey(KeyCode.LeftShift);

        speed = _isWalking ? _walkSpeed : _runSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _inputVector  = new Vector2(horizontal, vertical);

        if (_inputVector.sqrMagnitude > 1)
        {
            _inputVector.Normalize();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (_collisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(_controller.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}
