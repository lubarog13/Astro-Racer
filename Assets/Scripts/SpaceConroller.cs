using UnityEngine;
using System.Collections.Generic;
public class SpaceConroller : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject starPrefab;
    [SerializeField] int starCount = 40;
    [SerializeField] GameObject ceresPrefab;
    [SerializeField] GameObject marsPrefab;
    [SerializeField] GameObject venusPrefab;
    [SerializeField] int planetCount = 40;
    [SerializeField] List<GameObject> planets = new List<GameObject>();
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
        GeneratePlanets();
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
            GeneratePlanets();
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
                float scale = Random.Range(0.5f, 3f);
                star.transform.localScale = new Vector3(scale, scale, scale);
                stars.Add(star);
            }
        }
    }

    void GeneratePlanets()
    {
        List<GameObject> planetsNotInRadius = new List<GameObject>();
        foreach (GameObject planet in planets)
        {
            if (Vector3.Distance(planet.transform.position, new Vector3(centerX, centerY, centerZ)) > radius)
            {
                planetsNotInRadius.Add(planet);
            }
        }
        foreach (GameObject planet in planetsNotInRadius)
        {
            Destroy(planet);
            planets.Remove(planet);
        }
        while (planets.Count < planetCount)
        {
            Vector3 randomPosition = new Vector3(Random.Range(centerX - radius, centerX + radius), Random.Range(centerY, centerY + radius), Random.Range(centerZ - radius, centerZ + radius));
            if (Vector3.Distance(randomPosition, new Vector3(centerX, centerY, centerZ)) > 5) {
                GameObject planetPrefab = GetRandomPlanetPrefab();
                GameObject planet = Instantiate(planetPrefab, randomPosition, Quaternion.identity);
                float scale = Random.Range(0.5f, 3f);
                planet.transform.localScale = new Vector3(scale, scale, scale);
                planet.GetComponent<PlanetMovement>().SetSpeed(Random.Range(5f, 25f));
                planets.Add(planet);
            }
        }
    }

    GameObject GetRandomPlanetPrefab()
    {
        int randomIndex = Random.Range(0, 3);
        if (randomIndex == 0) {
            return ceresPrefab;
        } else if (randomIndex == 1) {
            return marsPrefab;
        } else {
            return venusPrefab;
        }
    }
}
