using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private Vector3 velocity;
    private Transform cameraTransform;
    private float xRotation = 0f;

    void Start()
    {
        // Получаем доступ к CharacterController и камере
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Скрываем курсор и закрепляем его по центру экрана
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Управление камерой (вращение по осям X и Y)
        RotateCamera();

        // Передвижение персонажа
        MovePlayer();
    }

    void RotateCamera()
    {
        // Получаем данные по мыши
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Поворачиваем персонажа по оси Y (влево и вправо)
        transform.Rotate(Vector3.up * mouseX);

        // Поворачиваем камеру по оси X (вверх и вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем вращение по вертикали
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void MovePlayer()
    {
        // Получаем направление движения
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Двигаем персонажа по направлению движения
        characterController.Move(move * speed * Time.deltaTime);

        // Применение гравитации
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Чуть больше нуля, чтобы персонаж касался земли
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Применяем гравитацию к персонажу
        characterController.Move(velocity * Time.deltaTime);
    }
}
