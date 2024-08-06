using UnityEngine;

public class FencePlacer : MonoBehaviour
{
    public GameObject fencePrefab;  
    public Vector3 startPoint = new Vector3(348.41f, 181.92f, 481.54f); 
    public Vector3 endPoint = new Vector3(411.6222f, 123.6481f, 845.1461f);
    public float spacing = 5f;      
    public float slopeWidth = 15f; 
    public Vector3 fenceScale = new Vector3(3f, 4.5f, 3f); 

    void Start()
    {
        PlaceFences();
    }

    void PlaceFences()
    {
        Vector3 slopeDirection = (endPoint - startPoint).normalized;
        Vector3 perpendicularDirection = Vector3.Cross(slopeDirection, Vector3.up).normalized;

        float distance = Vector3.Distance(startPoint, endPoint);
        int numberOfFences = Mathf.FloorToInt(distance / spacing);

        for (int i = 0; i <= numberOfFences; i++)
        {
            Vector3 basePosition = startPoint + slopeDirection * (i * spacing);
            basePosition.y = Terrain.activeTerrain.SampleHeight(basePosition);

            Vector3 leftPosition = basePosition - perpendicularDirection * (slopeWidth / 2);
            Vector3 rightPosition = basePosition + perpendicularDirection * (slopeWidth / 2);

            leftPosition.y = Terrain.activeTerrain.SampleHeight(leftPosition);
            rightPosition.y = Terrain.activeTerrain.SampleHeight(rightPosition);

            Quaternion leftRotation = Quaternion.LookRotation(-perpendicularDirection, Vector3.up);
            Quaternion rightRotation = Quaternion.LookRotation(perpendicularDirection, Vector3.up);

            GameObject leftFence = Instantiate(fencePrefab, leftPosition, leftRotation);
            GameObject rightFence = Instantiate(fencePrefab, rightPosition, rightRotation);

            leftFence.transform.localScale = fenceScale;
            rightFence.transform.localScale = fenceScale;

            if (!leftFence.GetComponent<Collider>())
            {
                leftFence.AddComponent<BoxCollider>(); 
            }
            if (!rightFence.GetComponent<Collider>())
            {
                rightFence.AddComponent<BoxCollider>(); 
            }
        }
    }
}

