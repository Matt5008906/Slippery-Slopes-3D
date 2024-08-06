using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject mainCamera;
    public Terrain terrain;
    public Vector3 cameraOffset = new Vector3(-1, 6, -12); 
    public Vector3 playerStartPosition = new Vector3(348.41f, 181.92f, 481.54f); 

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            player = Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
        }
        else
        {
            player.transform.position = playerStartPosition;
            player.transform.position = new Vector3(player.transform.position.x, 
                Mathf.Max(terrain.SampleHeight(playerStartPosition) + 1.0f, player.transform.position.y), 
                player.transform.position.z);
        }

        UpdateCameraPosition(player);
    }

    void Update()
    {
        if (mainCamera.transform.parent != null)
        {
            UpdateCameraPosition(mainCamera.transform.parent.gameObject);
        }
    }

    void UpdateCameraPosition(GameObject player)
    {
        if (player != null)
        {
            Vector3 cameraPosition = player.transform.position + cameraOffset;
            mainCamera.transform.position = cameraPosition;
            mainCamera.transform.LookAt(player.transform);

            mainCamera.transform.parent = player.transform;
        }
    }
}
