using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassBurner : MonoBehaviour
{
    public BoxCollider2D gridArea1;

    void Start()
    {
          MassReducer();
    }
    private void MassReducer()
    {
        Bounds bounds2 = this.gridArea1.bounds;

        float x = Random.Range(bounds2.min.x, bounds2.max.x);
        float y = Random.Range(bounds2.min.y, bounds2.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MassReducer();
        }
    }

}
