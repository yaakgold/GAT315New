using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadtreeNode
{
	AABB aabb;
	int capacity;
	List<Body> bodies = null;
	bool subdivided = false;
	int level = 0;

	QuadtreeNode northeast = null;
	QuadtreeNode northwest = null;
	QuadtreeNode southeast = null;
	QuadtreeNode southwest = null;

	public QuadtreeNode(AABB aabb, int capacity, int level)
	{
		this.aabb = aabb;
		this.capacity = capacity;
		this.level = level;

		bodies = new List<Body>();
	}

	public void Insert(Body body)
	{
		// check if within node boundary, if not exit
		if (!aabb.Contains(body.shape.aabb)) return;

		// check if under node capacity, if so add to node bodies
		if (bodies.Count < capacity)
		{
			bodies.Add(body);
		}
		else
		{
			// exceeded capacity
			// subdivide node if not subdivided
			if (!subdivided)
			{
				Subdivide();
			}

			// insert into subdivided containing boundary
			northeast.Insert(body);
			northwest.Insert(body);
			southeast.Insert(body);
			southwest.Insert(body);
		}
	}

	public void Query(AABB aabb, List<Body> bodies)
	{
		// check if aabb is within node boundary, if not exit
		if (!this.aabb.Contains(aabb)) return;

		// check if aabb intersects node bodies aabb
		// add intersecting node bodies into bodies
		foreach (Body body in this.bodies)
		{
			//Lines.Instance.AddLine(aabb.center, body.position, Color.white, 0.1f);
			BroadPhase.numberOfTests++;
			if (body.shape.aabb.Contains(aabb))
			{
				bodies.Add(body);
			}
		}

		// if subdivided, check child nodes
		if (subdivided)
		{
			northeast.Query(aabb, bodies);
			northwest.Query(aabb, bodies);
			southeast.Query(aabb, bodies);
			southwest.Query(aabb, bodies);
		}
	}

	void Subdivide()
	{
		float xo = aabb.extents.x * 0.5f;
		float yo = aabb.extents.y * 0.5f;

		northeast = new QuadtreeNode(new AABB(new Vector2(aabb.center.x - xo, aabb.center.y + yo), aabb.extents), capacity, level + 1);
		northwest = new QuadtreeNode(new AABB(new Vector2(aabb.center.x + xo, aabb.center.y + yo), aabb.extents), capacity, level + 1);
		southeast = new QuadtreeNode(new AABB(new Vector2(aabb.center.x - xo, aabb.center.y - yo), aabb.extents), capacity, level + 1);
		southwest = new QuadtreeNode(new AABB(new Vector2(aabb.center.x + xo, aabb.center.y - yo), aabb.extents), capacity, level + 1);

		subdivided = true;
	}

	public void Draw()
	{
		Color color = BroadPhase.colors[level % BroadPhase.colors.Length];
		AABB aabb = new AABB(this.aabb.center, this.aabb.size * 0.99f);
		aabb.Draw(color);

		if (subdivided)
		{
			northeast.Draw();
			northwest.Draw();
			southeast.Draw();
			southwest.Draw();
		}
	}
}
