
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    SnakeMovement instance;

    [Header("gameobjects")]
    public GameObject tailPart;
    public GameObject food;
    GameObject foodClone;

    [Header("List")]
    List<Vector2> tailPos = new List<Vector2>();
    List<GameObject> tail = new List<GameObject>();

    float x, y;
    float gridHeight, gridWidth;
    int totalEat;

    void Start () {

        instance = FindObjectOfType<SnakeMovement>();
        gridHeight = Mathf.Floor(Camera.main.orthographicSize-1);
        gridWidth  = Mathf.Floor(gridHeight * Camera.main.aspect-1);
       
        //spawn first food
        foodClone = Instantiate(food, Vector3.zero, transform.rotation);

        //allot food position
        FoodRelocate();

        //MakeGrid();

        //call function in perticular time interval
        InvokeRepeating("TailFollow", 0, instance.gameSpeed);
    }



    //make grid
    void MakeGrid()
    {
        for(float i = -gridWidth; i <= gridWidth; i += instance.snakeMoveDist)
        {
            for (float j = -gridHeight;j <= gridHeight; j += instance.snakeMoveDist)
            {
                Instantiate(tailPart, new Vector3(i, j), transform.rotation);
            }
        }
    }
	
	void Update () {
        RepositionTail();
	}

    //repositioning tails
    void RepositionTail()
    {
        for (int i = 0; i < tailPos.Count; i++)
        {
            tail[i].transform.position = tailPos[i];
        }
    }

    //change food position after food eaten
	public void FoodRelocate(){

        x = Mathf.Floor(Random.Range(-gridWidth, gridWidth)) * instance.snakeMoveDist;
        y = Mathf.Floor(Random.Range(-gridHeight, gridHeight)) * instance.snakeMoveDist;

        foreach(Vector2 pos in tailPos)
        {
            if(pos == new Vector2(x, y))
            {
                FoodRelocate();
                return;
            }
        }
        foodClone.transform.position = new Vector2(x, y);
    }

    //save head position in a vector2 array
    void TailFollow()
    {
        
        CheckFoodCollision();
        CheckBodyCollision();
        tailPos.Add(instance.transform.position);
        if (tailPos.Count > totalEat)
        {
            tailPos.RemoveAt(0);
        }
    }

    //check food eaten
    void CheckFoodCollision()
    {
        float dis = Mathf.Abs(Vector2.Distance(foodClone.transform.position, instance.transform.position));
        if (dis < 1)
        {
            totalEat++;
            FoodRelocate();
            tail.Add(Instantiate(tailPart , instance.transform.position,transform.rotation));
        }
    }

    //check head collide with body
    void CheckBodyCollision()
    {
        foreach (Vector3 pos in tailPos)
        {
            if (instance.transform.position == pos)
            {
                totalEat = 0;
                foreach(GameObject t in tail)
                {
                    Destroy(t);

                }
                tail.Clear();
                tailPos.Clear();
                return;
            }
        }
    }

}
