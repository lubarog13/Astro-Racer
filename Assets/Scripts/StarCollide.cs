using UnityEngine;

public class StarCollide : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private float temperatureIncrease = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Rocket") {
            RocketMovement otherRocket = other.gameObject.GetComponent<RocketMovement>();
            if (otherRocket != null)
            {
                otherRocket.SetTemperature(otherRocket.GetTemperature() + temperatureIncrease);
                uiController.ChangeProgressBarValue(temperatureIncrease);
            }
        }
    }
}
