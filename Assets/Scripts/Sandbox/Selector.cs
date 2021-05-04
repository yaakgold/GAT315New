using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        Body body = Utilities.GetBodyFromPosition(Input.mousePosition);
        if (body != null)
		{
            spriteRenderer.enabled = true;
            transform.position = body.position;
            transform.rotation = Quaternion.AngleAxis(Time.time * 90, Vector3.forward);
            transform.localScale = Vector3.one * body.shape.size * 1.2f;

            if (Input.GetMouseButton(2))
			{
                if (body.type == Body.eType.Static)
				{
                    Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    body.position = position;
				}
			}
        }
        else
		{
            spriteRenderer.enabled = false;
		}
    }
}
