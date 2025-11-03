using UnityEngine;

public class BigPlanet : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Rocket") {
            RocketMovement otherRocket = other.gameObject.GetComponent<RocketMovement>();
            Debug.Log("BigPlanet trigger entered");
            if (otherRocket != null)
            {
                otherRocket.SetRotateAngle(90f);
                otherRocket.SetTemperature(otherRocket.GetTemperature() + 15f);
                uiController.ChangeProgressBarValue(15f);
            }
        }
    }
}
