using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private int spawnAmount = 1;
    [SerializeField] private float spawnDistance = 11f;
    [SerializeField] private float trajectoryVariance = 11f;
    [SerializeField] private Star pStar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
        Vector3 spawnPoint = this.transform.position + spawnDirection;

        float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        for (int i = 0; i < this.spawnAmount; ++i)
        {
            Star star = Instantiate(pStar, spawnPoint, rotation);
            star.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
