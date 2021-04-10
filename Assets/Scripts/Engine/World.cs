using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData fixedFPS;
    public StringData fpsText;

    static World instance;
    static public World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();

    float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }

    float timeAccumulator = 0;

    // fps
    float fps;
    // fps frame count
    int frame = 0;
    const int frameMax = 100;
    float time = 0;
    // fps smoothing
    float fpsAverage = 0;
    float smoothing = 0.975f;


    private void Awake()
	{
        instance = this;
	}

	void Update()
    {
        // fps
        //fps = (1.0f / Time.deltaTime);
        
        // fps frame count
        frame++;
        time = time + Time.deltaTime;
        if (frame == frameMax)
        {
            fps = frameMax / time;
            fpsText.value = "FPS: " + fps.ToString("F1");
            frame = 0;
            time = 0;
        }

        // smoothing
        //fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));
        //fpsText.value = "FPS: " + fpsAverage.ToString("F1");

        if (!simulate.value) return;

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
