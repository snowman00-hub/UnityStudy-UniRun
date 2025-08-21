using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
            return;

        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
