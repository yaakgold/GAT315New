using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Variables/Float")]
public class FloatData : ScriptableObject
{
	public float value;

	//public float value { get => data; set => data = value; }
	public static implicit operator float(FloatData data) { return data.value; }
}
