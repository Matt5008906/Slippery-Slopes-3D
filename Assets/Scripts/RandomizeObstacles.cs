using UnityEngine;

public class RandomObstaclePlacer : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; 
    public int numberOfObstacles = 70;   
    public Vector3 startPoint = new Vector3(348.41f, 181.92f, 481.54f); 
    public Vector3 endPoint = new Vector3(411.6222f, 123.6481f, 845.1461f);
    public float spacing = 10f;      
    public float slopeWidth = 15f; 

    public Vector3 areaMin = new Vector3(340f, 0f, 470f);
    public Vector3 areaMax = new Vector3(420f, 0f, 860f);

    void Start()
    {
        PlaceObstacles();
    }

    void PlaceObstacles()
    {
        Vector3 slopeDirection = (endPoint - startPoint).normalized;
        float distance = Vector3.Distance(startPoint, endPoint);
        int numberOfFences = Mathf.FloorToInt(distance / spacing);

        for (int i = 0; i < numberOfObstacles; i++)
        {
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            float t = Random.Range(0f, 1f); 
            Vector3 basePosition = Vector3.Lerp(startPoint, endPoint, t);

            Vector3 perpendicularDirection = Vector3.Cross(slopeDirection, Vector3.up).normalized;
            Vector3 offset = perpendicularDirection * Random.Range(-slopeWidth / 2, slopeWidth / 2);
            Vector3 randomPosition = basePosition + offset;

            randomPosition.y = Terrain.activeTerrain.SampleHeight(randomPosition);

            GameObject placedObstacle = Instantiate(obstacle, randomPosition, Quaternion.LookRotation(-slopeDirection, Vector3.up));

            Collider collider = placedObstacle.GetComponent<Collider>();
            if (collider == null)
            {
                collider = placedObstacle.AddComponent<BoxCollider>();
            }
            collider.isTrigger = true;

            placedObstacle.tag = "Obstacle";
        }
    }
}
