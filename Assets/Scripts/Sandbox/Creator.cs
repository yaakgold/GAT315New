using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public GameObject original;
    public float speed = 10;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameObject = Instantiate(original, position, Quaternion.identity);
            if (gameObject.GetComponent<Body>() != null)
            {
                Body body = gameObject.GetComponent<Body>();
                
                Vector2 velocity = Random.insideUnitCircle.normalized * speed;
                body.AddForce(velocity);

                World.Instance.bodies.Add(body);
            }
        }
    }
}
