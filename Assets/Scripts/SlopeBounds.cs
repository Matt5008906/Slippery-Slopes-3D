using UnityEngine;

public class SlopeBounds : MonoBehaviour
{
    public Vector3 startPoint = new Vector3(348.41f, 181.92f, 481.54f); 
    public Vector3 endPoint = new Vector3(411.6222f, 123.6481f, 845.1461f);
    public float spacing = 6f;      
    public float slopeWidth = 14.5f; 
    public float wallHeight = 50;  
    public float wallThickness = 0.5f; 

    public PhysicMaterial wallMaterial; 

    void Start()
    {
        PlaceBoundaryWalls();
    }

    void PlaceBoundaryWalls()
    {
        Vector3 slopeDirection = (endPoint - startPoint).normalized;
        Vector3 perpendicularDirection = Vector3.Cross(slopeDirection, Vector3.up).normalized;

        float distance = Vector3.Distance(startPoint, endPoint);
        int numberOfWalls = Mathf.FloorToInt(distance / spacing);

        for (int i = 0; i <= numberOfWalls; i++)
        {
            Vector3 basePosition = startPoint + slopeDirection * (i * spacing);

            // Create left wall
            Vector3 leftPosition = basePosition - perpendicularDirection * (slopeWidth / 2);
            CreateBoundaryWall(leftPosition, Quaternion.LookRotation(slopeDirection, Vector3.up));

            // Create right wall
            Vector3 rightPosition = basePosition + perpendicularDirection * (slopeWidth / 2);
            CreateBoundaryWall(rightPosition, Quaternion.LookRotation(slopeDirection, Vector3.up));
        }
    }

    void CreateBoundaryWall(Vector3 position, Quaternion rotation)
    {
        GameObject wall = new GameObject("BoundaryWall");
        BoxCollider collider = wall.AddComponent<BoxCollider>();

        // Adjust wall size and position
        wall.transform.position = position;
        wall.transform.rotation = rotation;
        wall.transform.localScale = new Vector3(wallThickness, wallHeight, spacing);

        if (wallMaterial != null)
        {
            collider.material = wallMaterial;
        }

        collider.isTrigger = false;

        wall.transform.rotation = Quaternion.Euler(0, 0, 0); 
    }

}