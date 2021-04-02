using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
	struct State
	{
		public Vector2 position;
		public Vector2 velocity;
	}

	struct Derivative
	{
		public Vector2 velocity;
		public Vector2 acceleration;
	}

	static Vector2 Acceleration(State state, double t)
    {
		const float k = 15.0f;
		const float b = 0.1f;
        return -k * state.position - b * state.velocity;
	}

	static Derivative Evaluate(State initial, double t, float dt, Derivative derivative)
    {
        State state;
		state.position = initial.position + derivative.velocity * dt;
		state.velocity = initial.velocity + derivative.acceleration * dt;

		Derivative output;
		output.velocity = state.velocity;
		output.acceleration = derivative.acceleration;

		return output;
    }

	public static void ExplicitEuler(Body body, float dt)
	{
		body.position = body.position + (body.velocity * dt);
		body.velocity = body.velocity + (body.acceleration * dt);
		body.velocity = body.velocity * (1f / (1f + body.damping * dt));
	}

	public static void SemiImplicitEuler(Body body, float dt)
	{
		body.velocity = body.velocity + (body.acceleration * dt);
		body.position = body.position + (body.velocity * dt);
		body.velocity = body.velocity * (1f / (1f + (body.damping * dt)));
	}

	public static void RK4(Body body, float dt)
	{
		Derivative a, b, c, d;

		State state;
		state.position = body.position;
		state.velocity = body.velocity;

		Derivative derivative;
		derivative.velocity = body.velocity;
		derivative.acceleration = body.acceleration;

		a = Evaluate(state, body.time, 0.0f, derivative);
		b = Evaluate(state, body.time, dt * 0.5f, a);
		c = Evaluate(state, body.time, dt * 0.5f, b);
		d = Evaluate(state, body.time, dt, c);

		Vector2 velocity = 1.0f / 6.0f * (a.velocity + 2.0f * (b.velocity + c.velocity) + d.velocity);
		Vector2 acceleration = 1.0f / 6.0f * (a.acceleration + 2.0f * (b.acceleration + c.acceleration) + d.acceleration);

		body.position = state.position + velocity * dt;
		body.velocity = state.velocity + acceleration * dt;
		body.time += dt;
	}
}
