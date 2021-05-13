using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadtree : BroadPhase
{
	public int capacity { get; set; } = 4;
	QuadtreeNode rootNode = null;

	public override void Build(AABB aabb, List<Body> bodies)
	{
		numberOfTests = 0;
		rootNode = new QuadtreeNode(aabb, capacity, 0);
		bodies.ForEach(body => { rootNode.Insert(body); });
	}

	public override void Query(AABB aabb, List<Body> bodies)
	{
		rootNode.Query(aabb, bodies);
	}

	public override void Query(Body body, List<Body> bodies)
	{
		rootNode.Query(body.shape.aabb, bodies);
	}

	public override void Draw()
	{
		if (rootNode != null)
		{
			rootNode.Draw();
		}
	}
}
