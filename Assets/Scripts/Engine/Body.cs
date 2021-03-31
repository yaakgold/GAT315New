using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
	public enum eForceMode
	{
		Force,
		Acceleration,
		Velocity
	}

	public Vector2 force { get; set; } = Vector2.zero;
	public Vector2 acceleration { get; set; } = Vector2.zero;
	public Vector2 velocity { get; set; } = Vector2.zero;
	public Vector2 position { get { return transform.position; } set { transform.position = value; } }
	public float mass { get; set; } = 1;

	public void AddForce(Vector2 force, eForceMode mode = eForceMode.Force)
	{
		switch (mode)
		{
			case eForceMode.Acceleration:
				this.acceleration = force;
				break;
			case eForceMode.Velocity:
				this.velocity = force;
				break;
			case eForceMode.Force:
				this.force = this.force + force;
				break;
			default:
				break;
		}
	}

	public void Step(float dt)
	{
		acceleration = acceleration + World.Instance.gravity + (force / mass);
	}

}
