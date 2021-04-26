using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : Action
{
	bool action { get; set; } = false;
	Body source { get; set; } = null;

	public override void StartAction()
	{
		Body body = Utilities.GetBodyFromPosition(Input.mousePosition);
		if (body != null)
		{
			source = body;
			action = true;
		}
	}

	public override void StopAction()
	{
		if (source != null)
		{
			Body destination = Utilities.GetBodyFromPosition(Input.mousePosition);
			if (destination != null && destination != source)
			{
				float restLength = (source.position - destination.position).magnitude;
				Create(source, destination, restLength, 0.5f);
			}
		}

		action = false;
	}

	void Update()
	{
		if (source != null)
		{
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//m_lineRenderer.SetPosition(0, bodyAnchor.position);
			//m_lineRenderer.SetPosition(1, position);
		}
	}

	void Create(Body bodyA, Body bodyB, float restLength, float k)
	{
		Spring spring = new Spring();
		spring.bodyA = bodyA;
		spring.bodyB = bodyB;
		spring.restLength = restLength;
		spring.k = k;

		World.Instance.springs.Add(spring);
	}
}
