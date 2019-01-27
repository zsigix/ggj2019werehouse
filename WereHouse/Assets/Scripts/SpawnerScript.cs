using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject VictimPrefab;

    private float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnVictims();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 5)
        {
            currentTime = 0;
            SpawnVictims();
        }
    }

    void SpawnVictims()
    {
        var totalToSpawn = Random.Range(0, 5);

        for (var i = 0; i < totalToSpawn; i++)
        {
            Instantiate(VictimPrefab, transform.position, Quaternion.identity);
        }
    }
}
