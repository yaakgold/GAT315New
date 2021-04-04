using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public GameObject original;
    public FloatData speed;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameObject = Instantiate(original, position, Quaternion.identity);
            if (gameObject.TryGetComponent<Body>(out Body body))
            {
                Vector2 force = Random.insideUnitSphere.normalized * speed.value;

                body.AddForce(force);
                World.Instance.bodies.Add(body);
            }
        }
    }
}
