using UnityEngine;

public class StarCollide : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter " + other.gameObject.tag);
        if(other.gameObject.tag == "Rocket") {
            RocketMovement otherRocket = other.gameObject.GetComponent<RocketMovement>();
            if (otherRocket != null)
            {
                otherRocket.SetTemperature(otherRocket.GetTemperature() + 15f);
                uiController.ChangeProgressBarValue(15f);
            }
        }
    }
}
