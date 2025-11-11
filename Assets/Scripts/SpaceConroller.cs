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
    [SerializeField] GameObject bigPlanetPrefab;
    [SerializeField] int planetCount = 40;
    [SerializeField] UIController uiController;
    [SerializeField] List<GameObject> planets = new List<GameObject>();
    [SerializeField] GameObject starColliderPrefab;
    [SerializeField] GameObject starExplosionPrefab;

    List<GameObject> stars = new List<GameObject>();
    GameObject bigPlanet = null;
    private float centerX = 0f;
    private float centerY = 0f;
    private float centerZ = 0f;
    private float radius = 100f;
    private float bigPlanetRadius = 100f;
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
            if (Vector3.Distance(randomPosition, new Vector3(centerX, centerY, centerZ)) > 15) {
                GameObject star = Instantiate(starPrefab, randomPosition, Quaternion.identity);
                float scale = Random.Range(0.5f, 3f);
                star.transform.localScale = new Vector3(scale, scale, scale);
                star.GetComponent<StarCollide>().explosionPrefab = starExplosionPrefab;
                star.GetComponent<StarCollide>().colliderPrefab = starColliderPrefab;
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
         if (bigPlanet != null && Vector3.Distance(bigPlanet.transform.position, new Vector3(centerX, centerY, centerZ)) > bigPlanetRadius)
            {
                Destroy(bigPlanet);
                bigPlanet = null;
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
                planet.GetComponent<PlanetMovement>().uiController = uiController;
                planets.Add(planet);
            }
        }
        if (bigPlanet == null) {
            Vector3 randomPosition = new Vector3(centerX, centerY + 30f, centerZ);
            bigPlanet = Instantiate(bigPlanetPrefab, randomPosition, Quaternion.identity);
            float scale = Random.Range(10f, 30f);
            bigPlanet.transform.localScale = new Vector3(scale, scale, scale);
            bigPlanetRadius = Random.Range(100f, 200f);
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
