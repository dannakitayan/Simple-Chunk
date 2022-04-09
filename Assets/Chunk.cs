using UnityEngine;
using Random = UnityEngine.Random;

public class Chunk 
{
    Vector2 startPosition;

    GameObject[] content;
    GameObject chunkParent;

    int size;
    int tileSize;
    int number;

    public int Number => number;
    public bool ChunkStatus => chunkParent.activeSelf;
    public Vector2 Position => startPosition;
    public Vector2 EndPosition => new Vector2(startPosition.x + size, startPosition.y + size);

    public Chunk(Vector2 chunkPosition, GameObject[] chunkContent, int chunkSize, int tileSize, int Number = 0)
    {
        startPosition = chunkPosition;
        content = chunkContent;
        size = chunkSize;
        this.tileSize = tileSize;
        number = Number;

        GenerateChunk();
    }

    public void ControlChunk(bool value)
    {
        chunkParent.SetActive(value);
    }

    void GenerateChunk()
    {
        chunkParent = new GameObject($"Chunk:{number.ToString()}");
        chunkParent.transform.position = startPosition;
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                GameObject.Instantiate(content[Random.Range(0, content.Length - 1)], new Vector2(chunkParent.transform.position.x + i * tileSize, chunkParent.transform.position.y + j * tileSize), Quaternion.identity, chunkParent.transform);
            }
        }
    }
}
