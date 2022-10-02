using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour
{
    private Vector2 _movement = Vector2.right;
    private List<Transform> _body = new List<Transform>();
    public Transform bodyPrefab;
    public int initialSize = 4;
    public GameObject Top, Buttom, Right, Left;
    public Camera cam;
   
    private int _scoreText;


    private void Start()
    {
        ResetState();
        ScreenRatio(cam);
    
    }

    private void Update()
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
      for(int i = _body.Count - 1; i > 0; i--)
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
 
    private void ResetState()
    {
        for (int i = 1; i < _body.Count; i++)
        {
            Destroy(_body[i].gameObject);
        }
        _body.Clear();
        _body.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _body.Add(Instantiate(this.bodyPrefab));
        }
        this.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //score
        if (other.CompareTag("Food"))
        {
            ScoreManager.instance.AddScore();
            Grow();
        }
       else if(other.CompareTag("Obstacle"))
        {
            ResetState();
        }
        else if(other.CompareTag("Walls"))
        {
           ChangeDirection();
        }
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
