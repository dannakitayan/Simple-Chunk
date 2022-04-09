using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerMovement : MonoBehaviour
{
    public Sprite[] Directions;
    public float speed;

    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            renderer.sprite = Directions[3];
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            renderer.sprite = Directions[0];
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            renderer.sprite = Directions[2];
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            renderer.sprite = Directions[1];
        }
    }
}
