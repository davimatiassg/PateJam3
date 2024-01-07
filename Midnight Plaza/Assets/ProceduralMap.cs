using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMap : MonoBehaviour
{
    [SerializeField] private int chunkSize = 50;
    
    [SerializeField] private GameObject chunk;

    private List<Rect> chunks;
    private Transform player;
    private Rect currentChunk;

    // Start is called before the first frame update
    void Start()
    {
        chunks = new List<Rect>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentChunk = GetPlayerChunk();

        if (PlayerOutsideChunk(currentChunk)) {
            GenerateNewChunk(currentChunk);
            chunks.Add(currentChunk);
            Debug.Log("man");
        }
    }

    void GenerateNewChunk(Rect chunk_) {
        Vector3 pos = new Vector3(chunk_.x, transform.position.y, chunk_.y);
        var c = Instantiate(chunk, pos, Quaternion.identity);
        c.transform.parent = transform.parent;
    }

    bool PlayerOutsideChunk(Rect chunk) {
        return !chunks.Contains(chunk);
    }

    Rect GetPlayerChunk() {
        Rect currentChunk = new Rect(
            Mathf.Floor(player.position.x / chunkSize), Mathf.Floor(player.position.z / chunkSize), chunkSize, chunkSize);
        return currentChunk;
    }
}
