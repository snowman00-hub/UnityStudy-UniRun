using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;

    private bool stepped = false;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        stepped = false;

        foreach (var obstacle in obstacles)
        {
            obstacle.SetActive(Random.value < 0.3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!stepped && collision.collider.CompareTag("Player"))
        {
            stepped = true;
            gameManager.AddScore(1);
        }
    }
}