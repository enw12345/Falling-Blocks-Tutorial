using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float fallSpeed = 5f;
    public bool alive;
    private Vector2 screenToWorld;
    private float respawnTimer = 1.5f;
    public float RespawnTimer => respawnTimer;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    private float spawnAngleMax = 5;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        screenToWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height + 1));
    }

    // Start is called before the first frame update
    void Start()
    {
        alive = false;
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector2.down * fallSpeed * Time.deltaTime;
        transform.Translate(velocity);

        Vector2 blockPos = Camera.main.WorldToScreenPoint(transform.position);

        if (blockPos.y < 0 && alive == true)
        {
            Despawn();
        }

        if(alive == false)
        {
            respawnTimer -= Time.deltaTime;
        }
    }

    public void Respawn()
    {
        alive = true;
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        SetRespawnSettings();
        respawnTimer = 1.5f;
    }

    private void SetRespawnSettings()
    {
        //Give the block a random position, speed, rotation, and size when it respawns
        float xPos = Random.Range(-screenToWorld.x + .5f, screenToWorld.x - .5f);
        transform.position = new Vector2(xPos, screenToWorld.y + (transform.localScale.magnitude));
        fallSpeed = Random.Range(7, 9) * GameManager.difficulty;
        float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
        transform.rotation = Quaternion.Euler(Vector3.forward * spawnAngle);
        transform.localScale = new Vector3(.7f, .7f, .7f) * Random.Range(1, 3);
    }

    public void Despawn()
    {
        alive = false;
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }
}
