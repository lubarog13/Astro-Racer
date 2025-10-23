using UnityEngine;
using UnityEngine.AI;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 10f;
    [SerializeField] UIController uiController;
    private ControllerColliderHit _contact;
    private CharacterController _charController;
    private float angle = 0f;
    private bool isAngleChanged = false;
    public float temperature = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _charController.enableOverlapRecovery = true;
        temperature = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float zdirection = Input.GetAxis("Jump");
        if (Input.GetKeyDown(KeyCode.A) && speed < 50f)
        {
            speed += 3f;
        } 
        if (Input.GetKeyDown(KeyCode.D) && speed > 0f)
        {
            speed -= 3f;
        } 
        if(vertical != 0 || horizontal < 0 || zdirection!=0 || isAngleChanged)
        {
            movement.z = vertical;
            movement.x = -horizontal;
            movement.y = zdirection;
            if (isAngleChanged)
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
                isAngleChanged = false;
            }
            movement = movement.normalized * speed * Time.deltaTime;
            _charController.Move(movement);
            // angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        temperature -= 5f;
        if (temperature < 0f) {
            temperature = 0f;
        }
        uiController.ChangeProgressBarValue(-5f);
        
    }
}
