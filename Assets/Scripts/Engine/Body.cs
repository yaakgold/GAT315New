using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
	public enum eType
	{
		Static,
		Kinematic,
		Dynamic
	}
	
	public enum eForceMode
	{
		Force,
		Acceleration,
		Velocity
	}

	public eType type { get; set; } = eType.Dynamic;
	public Vector2 force { get; set; } = Vector2.zero;
	public Vector2 acceleration { get; set; } = Vector2.zero;
	public Vector2 velocity { get; set; } = Vector2.zero;
	public Vector2 position { get { return transform.position; } set { transform.position = value; } }

	public float mass { get; set; } = 1;
	public float damping { get; set; } = 0;
	public float time { get; set; } = 0;

	public void AddForce(Vector2 force)
	{
		this.force += force;
	}

	public void Step(float dt)
	{
		acceleration = new Vector2(0, -9.81f) + (force / mass);
	}
}
