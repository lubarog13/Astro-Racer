using UnityEngine;
using System.Collections.Generic;
public class SpaceConroller : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject starPrefab;
    [SerializeField] int starCount = 40;
    List<GameObject> stars = new List<GameObject>();
    private float centerX = 0f;
    private float centerY = 0f;
    private float centerZ = 0f;
    private float radius = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerX = rocket.transform.position.x;
        centerY = rocket.transform.position.y;
        centerZ = rocket.transform.position.z;
        GenerateStars();
    }

    // Update is called once per frame
    void Update()
    {
        if (rocket.transform.position.y != centerY || rocket.transform.position.x != centerX || rocket.transform.position.z != centerZ)
        {
            centerX = rocket.transform.position.x;
            centerY = rocket.transform.position.y;
            centerZ = rocket.transform.position.z;
            GenerateStars();
        }
    }

    void GenerateStars()
    {
        List<GameObject> starsNotInRadius = new List<GameObject>();
        foreach (GameObject star in stars)
        {
            if (Vector3.Distance(star.transform.position, new Vector3(centerX, centerY, centerZ)) > radius)
            {
                starsNotInRadius.Add(star);
            }
        }
        foreach (GameObject star in starsNotInRadius)
        {
            Destroy(star);
            stars.Remove(star);
        }
        while (stars.Count < starCount)
        {
            Vector3 randomPosition = new Vector3(Random.Range(centerX - radius, centerX + radius), Random.Range(centerY, centerY + radius), Random.Range(centerZ - radius, centerZ + radius));
            if (Vector3.Distance(randomPosition, new Vector3(centerX, centerY, centerZ)) > 5) {
                GameObject star = Instantiate(starPrefab, randomPosition, Quaternion.identity);
                stars.Add(star);
            }
        }
    }

}
