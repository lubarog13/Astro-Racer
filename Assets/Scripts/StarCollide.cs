using UnityEngine;
using System.Collections;
public class StarCollide : MonoBehaviour
{
    [SerializeField] private float temperatureIncrease = 15f;
    [SerializeField] public GameObject explosionPrefab;
    [SerializeField] public GameObject colliderPrefab;

    private GameObject collider = null;
    private GameObject explosion = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // // Update is called once per frame
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Rocket") {
            RocketMovement otherRocket = other.gameObject.GetComponent<RocketMovement>();
            if (otherRocket != null)
            {
                otherRocket.SetTemperature(temperatureIncrease);
                //uiController.ChangeProgressBarValue(temperatureIncrease);
                if (colliderPrefab) {
                    collider = Instantiate(colliderPrefab, transform.position, Quaternion.identity);
                    SphereCollider sphereCollider = GetComponent<Collider>().GetComponent<SphereCollider>();
                    collider.transform.localScale = new Vector3(sphereCollider.radius * transform.localScale.x *2, sphereCollider.radius * transform.localScale.y *2, sphereCollider.radius * transform.localScale.z *2);
                }
                if (explosionPrefab) {
                    explosion = Instantiate(explosionPrefab, other.ClosestPoint(transform.position), Quaternion.identity);
                    StartCoroutine(RemoveExplosionAfterDelay(explosion, 1f));
                }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Rocket") {
            if (colliderPrefab) {
                StartCoroutine(RemoveColliderAfterDelay(collider, 1f));
                collider = null;
            }
            if (explosionPrefab) {
                explosion = Instantiate(explosionPrefab, other.ClosestPoint(transform.position), Quaternion.identity);
                StartCoroutine(RemoveExplosionAfterDelay(explosion, 1f));
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
        if(collision.gameObject.tag == "Rocket") {
            RocketMovement otherRocket = collision.gameObject.GetComponent<RocketMovement>();
            if (otherRocket != null)
            {
                otherRocket.SetTemperature(temperatureIncrease);
                if (explosionPrefab && colliderPrefab) {
                    Instantiate(explosionPrefab, collision.contacts[0].point, Quaternion.identity);
                    GameObject collider = Instantiate(colliderPrefab, transform.position, Quaternion.identity);
                    CapsuleCollider capsuleCollider = collider.GetComponent<CapsuleCollider>();
                    collider.transform.localScale = new Vector3(capsuleCollider.radius, capsuleCollider.height, capsuleCollider.radius);
                    StartCoroutine(RemoveColliderAfterDelay(collider, 2f));
                }
            }
        }
    }

    IEnumerator RemoveExplosionAfterDelay(GameObject explosion, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(explosion.gameObject);
        explosion = null;
    }

    IEnumerator RemoveColliderAfterDelay(GameObject collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(collider.gameObject);
        collider = null;
    }
}
