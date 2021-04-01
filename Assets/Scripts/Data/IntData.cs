using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Int", menuName = "Variables/Int")]
public class IntData : ScriptableObject
{
	public int value;

	//public int value { get => data; set => data = value; }
	public static implicit operator int(IntData data) { return data.value; }
}
