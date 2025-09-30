using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Transform cam;
    public float horizontalRotation;
    public float verticalRotation;
    public float movementSpeed;
    public float mouseSensivity;

    Rigidbody rb;

    public Image staminaBar;

    public float stamina;
    public float maxStamina;

    public float sprintCost;
    public float staminaCharge;

    public float timeForCharge;
    public float timeSinceSprint;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>().transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        Camera();
        Stamina();

    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Translate(0, 0, verticalInput * movementSpeed);
        transform.Translate(horizontalInput * movementSpeed, 0, 0);
    }

    void Camera()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(verticalRotation, -50, 90);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, horizontalRotation, transform.localEulerAngles.z);
        cam.transform.localEulerAngles = new Vector3(verticalRotation, cam.localEulerAngles.y, cam.localEulerAngles.z);
    }
    void Stamina()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            stamina -= sprintCost * Time.deltaTime;
            if (stamina > 0f)
            {
                movementSpeed = 3f;
            }
            else
            {
                stamina = 0f;
                movementSpeed = 2f;
            }

            timeSinceSprint = 0f;
        }
        else
        {
            timeSinceSprint += Time.deltaTime;

            if(timeSinceSprint >= timeForCharge)
            {
                stamina += staminaCharge * Time.deltaTime;
                if(stamina > maxStamina)
                {
                    stamina = maxStamina;
                }
            }

            movementSpeed = 2f;
        }

        staminaBar.fillAmount = stamina / maxStamina;
    }
}
