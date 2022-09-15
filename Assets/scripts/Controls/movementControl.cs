using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float speedRun;
    [SerializeField] private float wallRunSpeed;

    [SerializeField] private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    [Header("Ground Check")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask _whatIsGround;
    [HideInInspector] public bool isGrounded;
    private bool allowJump;
    private RaycastHit floor;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplayer;
    [SerializeField] private float timerToJumpIfStartFalling;
    private float timerToJumpIfStartFallingDefaultValue;
    private bool readyToJump = true;
    [Header("Camera")]
    [SerializeField] private float cameraRunFOV;
    private Camera _camera;
    private float cameraBaseFOV;


    [Header("Sounds")]
    [SerializeField] private AudioClip[] _audioClips = new AudioClip[2];
    private AudioSource _audioSource;

    private Vector3 moveDirection;
    private Rigidbody _rigidBody;
    [HideInInspector] public bool wallrunning = false;
    [Header("ButtonToSaveProgress")]
    public Button RewardButton;
    void Start()
    {
        timerToJumpIfStartFallingDefaultValue = timerToJumpIfStartFalling;
        _rigidBody = GetComponent<Rigidbody>();
        
        _audioSource = GetComponent<AudioSource>();

        _camera = Camera.main;
        cameraBaseFOV = _camera.fieldOfView;
    }

    // Update is called once per frame
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out floor, playerHeight / 2 + .2f, _whatIsGround);
        RewardButton.interactable = isGrounded;

        if (isGrounded == true)
        {
            allowJump = true;
            timerToJumpIfStartFalling = timerToJumpIfStartFallingDefaultValue;
        }
        else if (timerToJumpIfStartFalling > 0)
        {
            timerToJumpIfStartFalling -= Time.deltaTime;
            allowJump = true;
        }
        
        else if (timerToJumpIfStartFalling < 0)
        {
            allowJump = false;
        }

        MyInput();

        if (isGrounded) _rigidBody.drag = groundDrag;
        else _rigidBody.drag = 0;
    }
    private void FixedUpdate()
    {
        MovePlayer();
        SoundPlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && allowJump && readyToJump)
        {
            readyToJump = false;
            jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection = direction(moveDirection);
        float speedInput;
        if (isGrounded)
        {
                speedInput = speed;
        }
        else if (wallrunning)
            speedInput = wallRunSpeed;
        else
        {
                speedInput = speed * airMultiplayer;
        }

        _rigidBody.AddForce(moveDirection.normalized * speedInput * 100f * Time.deltaTime, ForceMode.Force);
        
    }

    private Vector3 direction(Vector3 forward) {

        Vector3 normal = floor.normal;
        return forward - Vector3.Dot(forward, normal) * normal; 
    }

    private void jump()
    {
        _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void SoundPlayer()
    {

        if (Input.GetKey(KeyCode.LeftShift) && !wallrunning) // регулировка скорости воспроизведени€ звукџа
            _audioSource.pitch = speedRun / speed;
        else
            _audioSource.pitch = 1;

        if (wallrunning && Time.timeScale == 1 && moveDirection.magnitude != 0) //проверка на включение звука бега по стене
        {
            _audioSource.clip = _audioClips[1];
            if (!_audioSource.isPlaying  )
                _audioSource.Play();
        }
        else if (isGrounded && Time.timeScale == 1 && moveDirection.magnitude != 0) //проверка на включение звука бега земле
        {
            _audioSource.clip = _audioClips[0];
            if (!_audioSource.isPlaying  )
                _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
    private void ResetJump() => readyToJump = true;
}
