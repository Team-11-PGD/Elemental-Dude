using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElementMain : MonoBehaviour
{
	[HideInInspector]
	public float DamagePercentage = 1.5f;
	public ElementType currentType = ElementType.None;

	//Do we still need this now?
	/*public Dictionary<ElementType, Color> ElementColor = new Dictionary<ElementType, Color>
	{
		{ ElementType.None, Color.white},
		{ ElementType.Water, Color.blue},
		{ ElementType.Fire, new Color(1, 0.4f, 0)},
		{ ElementType.Air, Color.grey},
		{ ElementType.Earth, new Color(0.59f, 0.38f, 0.21f)}
	};*/

	public enum ElementType
	{
		None,
		Water,
		Fire,
		Air,
		Earth
	}

	public float ElementDmgPercentage(ElementType thisElement, ElementType otherElement)
	{
		switch (thisElement)
		{
			case ElementType.Water:
				if (otherElement == ElementType.Fire)
				{
					return DamagePercentage;
				}
				break;

			case ElementType.Fire:
				if (otherElement == ElementType.Air)
				{
					return DamagePercentage;
				}
				break;

			case ElementType.Air:
				if (otherElement == ElementType.Earth)
				{
					return DamagePercentage;
				}
				break;

			case ElementType.Earth:
				if (otherElement == ElementType.Water)
				{
					return DamagePercentage;
				}
				break;
		}
		return 1;
	}
}
