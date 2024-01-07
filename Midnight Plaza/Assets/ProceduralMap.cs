using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMap : MonoBehaviour
{
    [SerializeField] private int chunkSize = 50;
    
    [SerializeField] private GameObject chunk;
    [SerializeField] private Transform buildings;

    private List<Rect> chunks;
    private List<GameObject> chunkObjs;
    private GameObject player;
    private Transform playerTrans;
    private Rect currentChunk;

    // Start is called before the first frame update
    void Start()
    {
        chunks = new List<Rect>();
        chunkObjs = new List<GameObject>();
        player = GameObject.FindWithTag("Player");
        playerTrans = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentChunk = GetPlayerChunk();

        if (PlayerOutsideChunk(currentChunk)) {
            GenerateNewChunk(currentChunk);
            chunks.Add(currentChunk);
            Debug.Log(currentChunk);
        }
    }

    void GenerateNewChunk(Rect chunk_) {
        Vector3 pos = new Vector3(chunk_.x * chunkSize, transform.position.y, chunk_.y * chunkSize);
        var c = Instantiate(chunk, pos, Quaternion.identity);
        c.transform.parent = buildings;
        chunkObjs.Add(c);
    }

    bool PlayerOutsideChunk(Rect chunk) {
        return !chunks.Contains(chunk);
    }

    Rect GetPlayerChunk() {

        Vector3 v = player.GetComponent<PlayerBehaviour>().GetVelocity() * 5f;
        float px = playerTrans.position.x + v.x, pz = playerTrans.position.z + v.z;
        Vector3 vec = new Vector3(px, 0, pz);

        vec = Quaternion.Euler(new Vector3(0, 0, 0)) * vec; // was using it before, might use it again, dont remove!!!
        px = vec.x;
        pz = vec.z;

        Rect currentChunk = new Rect(
            Mathf.Floor(px / chunkSize), Mathf.Floor(pz / chunkSize), chunkSize, chunkSize);
        return currentChunk;
    }
}
