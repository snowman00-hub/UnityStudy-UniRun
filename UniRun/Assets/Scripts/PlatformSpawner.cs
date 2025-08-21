using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public float resetPosX = -16f;
    public float minY = -4f;
    public float maxY = 1.5f;
    public float minInterval = 0.7f;
    public float maxInterval = 2f;

    private GameManager gameManager;
    private GameObject[] platformPool;
    public int PlatformCount = 10;
    private int PlatformIndex = 0;

    private float interval;
    private float timer = 0f;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        platformPool = new GameObject[PlatformCount];
        for (int i = 0; i < PlatformCount; i++)
        {
            platformPool[i] = Instantiate(platform, transform);
            platformPool[i].transform.position = new Vector3(0, Random.Range(minY, maxY), 0) + transform.position;
            platformPool[i].SetActive(false);
        }

        interval = 0.2f;
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
            return;

        timer += Time.deltaTime;
        if (timer > interval)
        {
            timer = 0;
            interval = Random.Range(minInterval, maxInterval);

            platformPool[PlatformIndex].SetActive(true);
            PlatformIndex = (PlatformIndex + 1) % PlatformCount;
        }

        foreach(var platform in platformPool)
        {
            if (platform.activeSelf && platform.transform.position.x < resetPosX)
            {
                platform.transform.position = new Vector3(0, Random.Range(minY, maxY), 0) + transform.position;
                platform.SetActive(false);                
            }
        }
    }
}
