using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 _movement = Vector2.right;
    private List<Transform> _body = new List<Transform>();
    public Transform bodyPrefab;
    public int initialSize = 4;
    public GameObject Top, Buttom, Right, Left;
    public Camera cam;
    public GameObject gameOverScreen;
    private int _scoreText;


   
    private void Start()
    {
        ResetState();
        ScreenRatio(cam);
    }

    private void Update()
    {
        InputMethods();
    }

    private void InputMethods()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _movement = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _movement = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _movement = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _movement = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        for (int i = _body.Count - 1; i > 0; i--)
        {
            _body[i].position = _body[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x )+ _movement.x,
            Mathf.Round(this.transform.position.y) + _movement.y,
            0.0f);
    }
    public void Grow()
    {
       
        Transform segment = Instantiate(this.bodyPrefab);
        segment.position = _body[_body.Count - 1].position;

        _body.Add(segment);
    
    }
    public void Burner()
    {
        Transform segment1 = Instantiate(this.bodyPrefab);
        segment1.position = _body[_body.Count - 1].position;

        _body.Remove(segment1);
    }
 
    private void ResetState()
    {
        for (int i = 1; i < _body.Count; i++)
        {
            Destroy(_body[i].gameObject);
            gameOverScreen.SetActive(true);
            enabled = false;

        }
        _body.Clear();
        _body.Add(this.transform);
       

        for (int i = 1; i < this.initialSize; i++)
        {
            _body.Add(Instantiate(this.bodyPrefab));
        }
        
        this.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //score
        if (collision.tag == "Food")
        {
            AddScore();
            Grow();
        }
        if (collision.tag == "Food2")
        {
            RemoveScore();
            Burner();
        }
       else if(collision.tag == "Obstacle")
        {
            ResetState();
        }
        else if(collision.tag == "Walls")
        {
           ChangeDirection();
        }
        else if (collision.tag == "Player2")
        {
            Destroy(collision.gameObject);
        }
    }
    public void AddScore()
    {
        ScoreManager.scoreValue += 10;   
    }
    public void RemoveScore()
    {
        ScoreManager.scoreValue -= 5;
    }

    public void ChangeDirection()
    {
        if(_movement == Vector2.left)
        {
            this.transform.position = new Vector3(Mathf.Round(Right.transform.position.x) + _movement.x,
                                                  Mathf.Round(this.transform.position.y) +_movement.y, 
                                                  0.0f);
        }
        if (_movement == Vector2.right)
        {
            this.transform.position = new Vector3(Mathf.Round(Left.transform.position.x) + _movement.x,
                                                 Mathf.Round(this.transform.position.y) + _movement.y,
                                                 0.0f);

        }
        if (_movement == Vector2.up)
        {
            this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _movement.x,
                                                 Mathf.Round(Buttom.transform.position.y) + _movement.y,
                                                 0.0f);

        }
        if (_movement == Vector2.down)
        {
            this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _movement.x,
                                                 Mathf.Round(Top.transform.position.y) + _movement.y,
                                                 0.0f);
        }
    }

    public void ScreenRatio(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;

        Bounds bounds = new Bounds(camera.transform.position,new Vector3(cameraHeight * screenAspect, cameraHeight,0));

        Top.transform.position = new Vector3(0, bounds.max.y, 0);
        Buttom.transform.position = new Vector3(0, bounds.min.y, 0);
        Left.transform.position = new Vector3(bounds.min.x, 0, 0);
        Right.transform.position = new Vector3(bounds.max.x, 0, 0);

    }
}
