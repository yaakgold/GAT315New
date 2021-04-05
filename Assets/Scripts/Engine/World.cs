using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData fixedFPS;
    public FloatData gravity;

    static World instance;
    static public World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();
        
    float timeAccumulator = 0.0f;
    float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }

	private void Awake()
	{
        instance = this;
	}

	void Update()
    {
        if (!simulate.value) return;

        timeAccumulator = timeAccumulator + Time.deltaTime;

        while (timeAccumulator > fixedDeltaTime)
		{
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.ExplicitEuler(body, fixedDeltaTime));

            timeAccumulator = timeAccumulator - fixedDeltaTime;
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
