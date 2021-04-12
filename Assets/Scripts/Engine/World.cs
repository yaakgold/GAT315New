using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFPS;
    public StringData fpsText;

    static World instance;
    static public World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();

    float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }

    float timeAccumulator = 0;

    private void Awake()
	{
        instance = this;
	}

	void Update()
    {
        Timer.Update();
        fpsText.value = "FPS: " + Timer.fps.ToString("F1") + " : " + (Timer.dt * 1000.0f).ToString("F1") + " ms";

        if (!simulate.value) return;

        GravitationalForce.ApplyForce(bodies, gravitation.value);

        timeAccumulator = timeAccumulator + Time.deltaTime;
        while (timeAccumulator >= fixedDeltaTime)
		{
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.SemiImplicitEuler(body, fixedDeltaTime));

            timeAccumulator = timeAccumulator - fixedDeltaTime;
		}

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
