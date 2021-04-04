using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravityY;

    static World instance;
    static public World Instance { get { return instance; } }

    public Vector2 gravity { get { return new Vector2(0, gravityY.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();

	private void Awake()
	{
        instance = this;
	}

	void Update()
    {
        if (!simulate.value) return;

        float dt = Time.deltaTime;

        bodies.ForEach(body => body.Step(dt));
        bodies.ForEach(body => Integrator.ExplicitEuler(body, dt));

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
