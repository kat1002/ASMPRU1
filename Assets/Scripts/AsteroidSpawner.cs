using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] public int SpawnAmount { get; set; }
    [SerializeField] private float spawnDistance = 11f;
    [SerializeField] private float trajectoryVariance = 11f;
    [SerializeField] private GameObject[] asteroids;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnAmount = 1;
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.SpawnAmount; ++i)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(asteroids[Random.Range(0, asteroids.Length)], spawnPoint, rotation).GetComponent<Asteroid>();
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
