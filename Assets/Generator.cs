using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Generator : MonoBehaviour
{
    int chunkNumber = 0; //Number of chunk for generate;
    const int chunkSize = 8; //Size of chunk;

    public GameObject[] TilesForGenerate;
    public Transform Player;

    Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();
    (Vector2, Vector2) lastVisitedChunk = (new Vector2(0, 0), new Vector2(chunkSize, chunkSize));

    void Start()
    {
        var i = GenerateNewChunk(new Vector2(0, 0));
        chunks.Add(i.Position, i);
        CreateNeighbors(i);
    }

    Chunk GenerateNewChunk(Vector2 chunkPosition)
    {
        chunkNumber += 1;
        return new Chunk(chunkPosition, TilesForGenerate, chunkSize, 1, chunkNumber);
    }

    Vector2[] CheckNeighborsPosition(Vector2 chunkPosition)
    {
        Vector2 left = new Vector2(chunkPosition.x - chunkSize, chunkPosition.y);
        Vector2 right = new Vector2(chunkPosition.x + chunkSize, chunkPosition.y);
        Vector2 top = new Vector2(chunkPosition.x, chunkPosition.y + chunkSize);
        Vector2 bottom = new Vector2(chunkPosition.x, chunkPosition.y - chunkSize);

        Vector2 leftTop = new Vector2(chunkPosition.x - chunkSize, chunkPosition.y + chunkSize);
        Vector2 rightTop = new Vector2(chunkPosition.x + chunkSize, chunkPosition.y + chunkSize);

        Vector2 leftBottom = new Vector2(chunkPosition.x - chunkSize, chunkPosition.y - chunkSize);
        Vector2 rightBottom = new Vector2(chunkPosition.x + chunkSize, chunkPosition.y - chunkSize);

        return new Vector2[8] { left, right, top, bottom, leftTop, rightTop, leftBottom, rightBottom };
    }

    bool CheckNeighbor(Vector2 neighborPosition)
    {
        return chunks.ContainsKey(neighborPosition);
    }

    //If a chunk have not neighbors, create new chunks;
    void CreateNeighbors(Chunk chunk)
    {
        foreach(var element in CheckNeighborsPosition(chunk.Position))
        {
            if(!CheckNeighbor(element))
            {
                chunks.Add(element, GenerateNewChunk(element));
            }
        }
    }

    void CurrentVisitingChunk(Transform player)
    {
        var xPosition = Mathf.RoundToInt(player.position.x);
        var yPosition = Mathf.RoundToInt(player.position.y);

        var getChunck = from values in chunks
                        where values.Key.x <= xPosition && values.Key.y <= yPosition && values.Value.EndPosition.x > xPosition && values.Value.EndPosition.y > yPosition
                        select values;
        if(getChunck != null)
        {
            foreach(var element in getChunck)
            {
                lastVisitedChunk.Item1 = element.Key;
                lastVisitedChunk.Item2 = element.Value.EndPosition;
                CreateNeighbors(element.Value);
                Debug.Log($"Current visited chunk: {element.Value.Number}, lastVisitedChunk<{lastVisitedChunk.Item1}, {lastVisitedChunk.Item2}>");
                break;
            }
        }
    }

    void Update()
    {
        var xPosition = Mathf.RoundToInt(Player.position.x);
        var yPosition = Mathf.RoundToInt(Player.position.y);

        if (xPosition < lastVisitedChunk.Item1.x || yPosition < lastVisitedChunk.Item1.y || xPosition > lastVisitedChunk.Item2.x || yPosition > lastVisitedChunk.Item2.y)
        {
            CurrentVisitingChunk(Player);
        }
    }
}
