
using UnityEngine;

public class SnakeMovement : MonoBehaviour {


    public float snakeMoveDist;
    public float gameSpeed;

    float x, y;
    float xDir = 1, yDir;    
    float height, width;
    bool allowChangeDir = true;


    private void Start()
    {
        InvokeRepeating("Move",0,gameSpeed);

        x = transform.position.x;
        y = transform.position.y;

        height = Camera.main.orthographicSize-.5f;
        width = height * Camera.main.aspect;
    }

    private void Update()
    {

        transform.position = new Vector2(x, y);
        Movements();
        WallPass();
    }

    //move snake head
    void Move()
    {
        x += snakeMoveDist * xDir;
        y += snakeMoveDist * yDir;
        allowChangeDir = true;
    }

    //head movement
    void Movements()
    {
        if (!allowChangeDir) return;

        if (Input.GetKey(KeyCode.UpArrow) && yDir!=-1)
        {
            allowChangeDir = false;
            yDir = 1;
            xDir = 0;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && yDir != 1)
        {
            allowChangeDir = false;
            yDir = -1;
            xDir = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && xDir != -1)
        {
            allowChangeDir = false;
            yDir = 0;
            xDir = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && xDir != 1)
        {
            allowChangeDir = false;
            yDir = 0;
            xDir =-1;
        }
    }

    //reposition snake after snake passes the camera position
    void WallPass()
    {
        if (y > height)
        {
            y = -height;
        }
        else if (y < -height)
        {
            y = height;
        }
        else if (x < -width)
        {
            x=width;
        }
        else if (x > width)
        {
            x = -width;
        }

    }
}
