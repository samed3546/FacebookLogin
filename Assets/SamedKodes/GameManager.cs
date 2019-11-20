using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Wall wall;

    public static Vector2 Left;
    public static Vector2 Right;

    private Vector3 position;
    private float width;
    private float height;
    void Start()
    {
        Left = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Wall walla = Instantiate(wall) as Wall;
        Wall wallb = Instantiate(wall) as Wall;
        walla.Init(true);
        wallb.Init(false);

        Instantiate(ball);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

}

 