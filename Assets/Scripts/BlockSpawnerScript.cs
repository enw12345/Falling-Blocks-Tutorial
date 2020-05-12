using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerScript : MonoBehaviour
{
    public Block[] BlockPool;
    public static GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        BlockPool = FindObjectsOfType<Block>();
        player = FindObjectOfType<PlayerBehavior>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RespawnBlock());
    }

    IEnumerator RespawnBlock()
    {
        for (int i = 0; i < BlockPool.Length; i++)
        {
            if (!BlockPool[i].alive && BlockPool[i].RespawnTimer <= 0)
            {
                BlockPool[i].Respawn();
                yield return new WaitForSeconds(BlockPool.Length * (GameManager.difficulty + 0.5f));
            }
            yield return new WaitForSeconds(BlockPool.Length * (GameManager.difficulty + 0.5f));
        }
    }
}
