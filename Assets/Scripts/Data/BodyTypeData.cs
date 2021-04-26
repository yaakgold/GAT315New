using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BodyType", menuName = "Data/Enum/BodyType")]
public class BodyTypeData : EnumData
{
	public enum eType
	{
		Static,
		Kinematic,
		Dynamic
	}

	public eType value;

	public override int index { get => (int)value; set => this.value = (eType)value; }
	public override string[] names => Enum.GetNames(typeof(eType));
}
