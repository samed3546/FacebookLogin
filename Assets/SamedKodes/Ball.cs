using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed;
    float radius;
    Vector2 direction;

    Text ScoreText;

    int ScoreA;
    int ScoreB;
    int MaxScore = 5;
    void Start()
    {
        direction = Vector2.one.normalized;
        radius = transform.localScale.x / 2;
        ScoreText = GameObject.Find("ScoreTxt").GetComponent<Text>();
    }

 
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        // yükseklik konturol ve yön degiştirme..
        if (transform.position.y < GameManager.Left.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }

        if (transform.position.y > GameManager.Right.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        // sag sol oyun puan kaybı
        if (transform.position.x < GameManager.Left.x + radius && direction.x < 0)
        {
            Debug.Log("Sag Kazandı");
            ScoreB++;
            if (ScoreB >= 5)
            {
                SceneManager.LoadScene(0);
            }
            transform.position = new Vector2(0, 0);
            ScoreText.text = ScoreA.ToString() + " - " + ScoreB.ToString();
        }
        if (transform.position.x > GameManager.Right.x - radius && direction.x > 0)
        {
            Debug.Log("Sol kazandı");
            ScoreA++;
            if (ScoreA >= 5)
            {
                SceneManager.LoadScene(0);
            }
            transform.position = new Vector2(0, 0);
            ScoreText.text = ScoreA.ToString() + " - " + ScoreB.ToString();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if(collision.tag == "")
        bool isRihgt = collision.GetComponent<Wall>().isRight;

        if(isRihgt == true && direction.x > 0)
        {
            direction.x = -direction.x;
        }

        if (isRihgt == false && direction.x < 0)
        {
            direction.x = -direction.x;
        }
    }
}
