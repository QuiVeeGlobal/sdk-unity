using UnityEngine;
using System.Collections.Generic;
using Roar;

public class RoarWidgetBar : RoarUIWidget
{

	private List<Transform> buttons;
	
	protected override void DrawGUI(int windowId)
	{
		float x_coordinate = contentBounds.x;
		foreach (Transform button in buttons)
		{
			if (GUI.Button (new Rect (x_coordinate, contentBounds.y, 180, contentBounds.height - contentBounds.y), button.name))
			{
				button.active = !button.active;
			}
			x_coordinate += 200;
		}
	}
	
	protected override void Awake()
	{
		base.Awake();
		buttons = new List<Transform>();
		foreach (Transform child in transform.parent)
		{
			if (child.transform != this.transform)
			{
				buttons.Add(child);
			}
		}
	}
}
