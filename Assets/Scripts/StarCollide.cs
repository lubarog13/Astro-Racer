using UnityEngine;

public class StarCollide : MonoBehaviour
{
    [SerializeField] private RocketMovement rocket;
    [SerializeField] private UIController uiController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rocket = GetComponent<RocketMovement>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter " + other.gameObject.tag);
        if(other.gameObject.tag == "Rocket") {
            rocket.temperature -= 15f;
            uiController.ChangeProgressBarValue(15f);
        }
    }
}
