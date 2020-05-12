using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 5f;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GameOver)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float velocity = inputX * speed;
            transform.Translate(Vector2.right * velocity * Time.deltaTime);

            Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, transform.position.y));
            if (playerPos.x > Screen.width + transform.localScale.x * 2)
            {
                transform.position = new Vector2(-screenToWorld.x, transform.position.y);
            }
            if (playerPos.x < 0 - transform.localScale.x * 2)
            {
                transform.position = new Vector2(screenToWorld.x, transform.position.y);
            }
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            ps.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            GameManager.GameOver = true;
        }
    }
}
