using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] asteroidsPrefabs;
    [SerializeField] float secondsBetweenAstroids = 1.5f;
    [SerializeField] Vector2 forceRange;

    Camera mainCamera;

    float timer;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnAstroid();

            timer += secondsBetweenAstroids;
        }
    }

    void SpawnAstroid()
    {
        int side = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch(side)
        {
            case 0:
                //left
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                //right
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                // bottom
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0f;

        GameObject selectedAstroid = asteroidsPrefabs[Random.Range(0, asteroidsPrefabs.Length)];

        GameObject astroidInstance = Instantiate(selectedAstroid, worldSpawnPoint, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        
        Rigidbody rb = astroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized * Random.Range(forceRange.x , forceRange.y);
    }
}
