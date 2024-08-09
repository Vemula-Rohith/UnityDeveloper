using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float rotationSpeed = 700f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
    public float mouseSensitivity = 100f;

    private bool isGrounded;
    private Vector3 moveDirection;
    private CharacterController controller;
    private float xRotation = 0f;
    public Transform playerCamera;
    Animator animator;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MoveCharacter();
        CheckGroundStatus();
        RotatePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void MoveCharacter()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveZ) > 0.1f;
        animator.SetBool("isRunning", isMoving);

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move = move.normalized * moveSpeed;

        if (!isGrounded)
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }

        moveDirection.x = move.x;
        moveDirection.z = move.z;

        controller.Move(moveDirection * Time.deltaTime);
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Jump()
    {
        moveDirection.y = jumpForce;
    }

    void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

}
