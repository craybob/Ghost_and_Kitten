using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject SafeZonePrefab;

    [HideInInspector] public GameManager gameManager;

    public void RenderLevel(float trackLength, int obstacleCount)
    {
        // Создаём дорожку
        var track = Instantiate(trackPrefab);
        track.transform.localScale = new Vector3(1, 1, trackLength);

        // Рандомная генерация препятствий
        for (int i = 0; i < obstacleCount; i++)
        {
            if(i == obstacleCount - 1)
            {
                GameObject safeZone = Instantiate(SafeZonePrefab);
                safeZone.transform.SetParent(track.transform);
                safeZone.transform.position = new Vector3(0,-3, trackLength);


                gameManager.GetSafeZone(safeZone.GetComponent<SafeZone>());
            }
            var obstacle = Instantiate(obstaclePrefab);
            obstacle.transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 0, Random.Range(1f, trackLength-1f));
        }
    }
}
