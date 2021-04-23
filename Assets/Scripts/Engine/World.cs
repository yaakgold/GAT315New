using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public BoolData collision;
    public BoolData wrap;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFPS;
    public StringData fpsText;

    static World instance;
    static public World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity); } }
    public List<Body> bodies { get; set; } = new List<Body>();

    Vector2 size;
    float fixedDeltaTime { get { return 1.0f / fixedFPS; } }
    float timeAccumulator = 0;

    private void Awake()
	{
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

	void Update()
    {
        Timer.Update();
        fpsText.value = "FPS: " + Timer.fps.ToString("F1") + " : " + (Timer.dt * 1000.0f).ToString("F1") + " ms";

        if (!simulate) return;

        GravitationalForce.ApplyForce(bodies, gravitation);

        timeAccumulator = timeAccumulator + Time.deltaTime;
        while (timeAccumulator >= fixedDeltaTime)
		{
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.SemiImplicitEuler(body, fixedDeltaTime));

            bodies.ForEach(body => body.shape.color = Color.white);
            if (collision)
			{
                Collision.CreateContacts(bodies, out List<Contact> contacts);
                contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; });
                ContactSolver.Resolve(contacts);
			}

            timeAccumulator = timeAccumulator - fixedDeltaTime;
		}

        if (wrap)
		{
            bodies.ForEach(body => body.position = Utilities.Wrap(body.position, -size, size));
        }
        bodies.ForEach(body => { body.force = Vector2.zero; body.acceleration = Vector2.zero; } );
    }
}
