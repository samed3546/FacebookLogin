using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{   
    [SerializeField]
    private float speed;
    float height;

    string input;
    public bool isRight;
    void Start()
    {
        height = transform.localScale.y;
        speed = 5f;
    }

    public void Init(bool IsRightWall)
    {
        isRight = IsRightWall;
        Vector2 pos = Vector2.zero;

        if (IsRightWall)
        {
            pos = new Vector2(GameManager.Right.x, 0);
            pos -= Vector2.right * transform.localScale.x;
            input = "Vertical";
        }
        else {
            pos = new Vector2(GameManager.Left.x, 0);
            pos += Vector2.right * transform.localScale.x;
            input = "Horizontal";
        }

        transform.position = pos;
    }

    
    void Update()
    {
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        if (transform.position.y < GameManager.Left.y + height / 2 && move < 0) //yükseklik sınırlaması için
        {
            move = 0;
        }

        if (transform.position.y > GameManager.Right.y - height / 2 && move > 0) 
        {
            move = 0;
        }
        transform.Translate(move * Vector2.up);
    }
}
