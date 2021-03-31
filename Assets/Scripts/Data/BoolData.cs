using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool", menuName = "Variables/Bool")]
public class BoolData : ScriptableObject
{
	public bool value;

	//public bool value { get => data; set => data = value; }
	public static implicit operator bool(BoolData data) { return data.value; }
}
