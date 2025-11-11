using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float minDirectionChangeInterval = 1f;
    [SerializeField] float maxDirectionChangeInterval = 4f;
    [SerializeField] public UIController uiController;
    
    private Rigidbody rb;
    private Vector3 currentDirection;
    private float nextDirectionChangeTime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
            return;
        }
        SetRandomDirection();
        
        SetNextDirectionChangeTime();
    }
    
    void Update()
    {
        if (rb == null || uiController.IsStartPanelActive()) return;
        
        if (Time.time >= nextDirectionChangeTime)
        {
            SetRandomDirection();
            SetNextDirectionChangeTime();
        }
        
        MovePlanet();
    }
    
    void SetRandomDirection()
    {
        currentDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
    
    void SetNextDirectionChangeTime()
    {
        float randomInterval = Random.Range(minDirectionChangeInterval, maxDirectionChangeInterval);
        nextDirectionChangeTime = Time.time + randomInterval;
    }
    
    void MovePlanet()
    {
        rb.linearVelocity = currentDirection * speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
