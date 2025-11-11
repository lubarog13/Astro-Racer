using UnityEngine;
using UnityEngine.AI;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 10f;
    [SerializeField] UIController uiController;
    [SerializeField] VolumetricFire volumetricFire;

    private ControllerColliderHit _contact;
    private CharacterController _charController;
    private float angle = 0f;
    private bool isAngleChanged = false;
    private float temperature = 0f;
    private float twoSecondTimer = 0f;
    private float halfSecondTimer = 0f;
    private float rotateAngle = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _charController = GetComponent<CharacterController>();
                // _charController.enableOverlapRecovery = true;
        temperature = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!uiController.IsGameActive() || uiController.IsStartPanelActive()) {
            return;
        }
        Vector3 movement = Vector3.zero;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float zdirection = Input.GetAxis("Jump");
        if (horizontal < 0 && speed < 200f)
        {
            speed += Mathf.Abs(horizontal) * 0.3f;
        } 
        if (horizontal > 0 && speed > 1f)
        {
            speed -= Mathf.Abs(horizontal) * 0.1f;
        } 
        movement.y = 1f;
        rotateAngle -= Time.deltaTime * 10f;
        rotateAngle = Mathf.Max(rotateAngle, 0f);

        if(horizontal != 0 || vertical != 0 || zdirection!=0 || isAngleChanged)
        {
            movement.z = -vertical;
            movement.x = zdirection;
           
            
            volumetricFire.thickness += Mathf.Max(1, (int)(speed * 0.01f));
            if (volumetricFire.thickness > 20) {
                volumetricFire.thickness = 20;
            }
            // angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0, angle, 0);
        } 
        else if (volumetricFire.thickness > 0 && halfSecondTimer >= 0.5f) {
            volumetricFire.thickness -= 3;
            if (volumetricFire.thickness < 1) {
                volumetricFire.thickness = 1;
            }
            halfSecondTimer = 0f;
        }
         movement = movement.normalized * speed * Time.deltaTime;
         
            _charController.Move(movement);
        halfSecondTimer += Time.deltaTime;
        twoSecondTimer += Time.deltaTime;
        // rotateAngle += Time.deltaTime * 10f;
        // transform.rotation = Quaternion.Euler(0, rotateAngle, 0);
        if (twoSecondTimer >= 2f)
        {
            // Change temperature every 2 seconds, for example by +10
            if (temperature > 0) {
                SetTemperature(-5f);
            }
            twoSecondTimer = 0f;
        }
        
        
    }

    public float GetTemperature()
    {
        return temperature;
    }

    public void SetTemperature(float value)
    {
        temperature += value;
        uiController.ChangeProgressBarValue(value);
        if (temperature > 100f) {
            speed = 10f;
            volumetricFire.thickness = 1;
            temperature = 0f;
            return;
        }
        if (temperature < 0f) {
            temperature = 0f;
        }
    }
}
