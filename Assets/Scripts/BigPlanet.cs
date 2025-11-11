using UnityEngine;

public class BigPlanet : MonoBehaviour
{
    private bool isTriggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (isTriggered) return;
        if(other.gameObject.tag == "Rocket") {
            RocketMovement otherRocket = other.gameObject.GetComponent<RocketMovement>();
            if (otherRocket != null)
            {
                otherRocket.SetTemperature(30f);
                isTriggered = true;
            }
        }
    }
}
