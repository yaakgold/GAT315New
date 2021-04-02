using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public GameObject original;
    public FloatData speed;
    public FloatData damping;
    public FloatData size;
    public FloatData density;

    void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
		{
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Create(position);
		}
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl))
		{
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Create(position);
        }
    }

    void Create(Vector2 position)
	{
        GameObject gameObject = Instantiate(original, position, Quaternion.identity);
        if (gameObject.TryGetComponent<Body>(out Body body))
        {
            Vector2 force = Random.insideUnitSphere.normalized * speed.value;
            body.AddForce(force);
            body.damping = damping.value;
            World.Instance.bodies.Add(body);
        }
    }
}
