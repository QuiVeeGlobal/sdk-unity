using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class WindowInfo
{
	public bool isWindowed = false;
	public bool supportsDragging = false;
	
	protected int windowId = 0;
	protected static int windowIdGenerator = 1;

	// We dont actually generate a windowId until asked for one.
	public int WindowId { get {
		if( isWindowed && windowId == 0 )
		{
			windowIdGenerator++;
			windowId = windowIdGenerator;
		}
			return windowId;
	} }
	
}

[Serializable]
public class Apearance
{
	public Color color = Color.white;
	public string boundingStyle = "WidgetBoundingStyle";
	public string boundingTitle = string.Empty;
	public Texture boundingImage = null;
}

//A simplified stripped down version of the RoarUIWidget - used to test out and demo the behaviour.
//Will probably need to pull most of this out into a common base class so that we can demo the behaviour i na more robust way.
public class TestWidget : MonoBehaviour
{
	protected bool fixedPosition = false;
	
	// Initial geometry
	//   These are just the initial settings
	//   Any programattic changes should occur to bounds and contentBounds instead.

	public bool syncBoundsPosition = true;
	public Rect initialBounds;
	public bool syncContentPosition = true;

	public Rect initialContentBounds;
	
	
	public GUISkin customGUISkin;
	
	public int depth;
	
	protected Rect bounds;
	protected Rect contentBounds;
	

	public Apearance apearance = new Apearance();
	public WindowInfo windowInfo = new WindowInfo();

	
	public AlignmentHorizontal horizontalAlignment = AlignmentHorizontal.Left;
	public AlignmentVertical verticalAlignment = AlignmentVertical.Top;
	public float horizontalOffset;
	public float verticalOffset;
	
	public bool useScrollView = false;
	public bool alwaysShowHorizontalScrollBar = false;
	public bool alwaysShowVerticalScrollBar = false;
	
	public bool autoEnableOnLogIn = false;
	public bool autoDisableOnLogout = true;
	
	protected GUISkin skin;
	protected DefaultRoar roar;
	
	// Whether the window should be fixed in place each render frame.
	// Easiest if this is set to true unless you have a dragable window
	protected bool RequiresSnap { get { return !windowInfo.supportsDragging; } }
	
	//Whether to render inside a window
	protected bool RequiresWindow { get { return windowInfo.isWindowed; } }

	private GUIContent boundingGUIContent;
	
	// scrolling
	private Vector2 scrollPosition = Vector2.zero;
	private Rect scrollViewRect;
	
	protected virtual void SnapBoundsRectIntoPosition()
	{

		// horizontal binding
		if( horizontalAlignment != AlignmentHorizontal.None )
		{
			switch (horizontalAlignment)
			{
			case AlignmentHorizontal.Left:
				bounds.x = 0;
				break;
			case AlignmentHorizontal.Right:
				bounds.x = Screen.width - bounds.width;
				break;
			case AlignmentHorizontal.Center:
				bounds.x = (Screen.width - bounds.width) / 2;
				break;
			}
			bounds.x += horizontalOffset;
		}
		
		if( verticalAlignment != AlignmentVertical.None )
		{
			// vertical binding
			switch (verticalAlignment)
			{
			case AlignmentVertical.Top:
				bounds.y = 0;
				break;
			case AlignmentVertical.Bottom:
				bounds.y = Screen.height - bounds.height;
				break;
			case AlignmentVertical.Center:
				bounds.y = (Screen.height - bounds.height) / 2;
				break;
			}
			bounds.y += verticalOffset;
		}
	}

	protected virtual void Awake()
	{
		bounds = initialBounds;
		contentBounds = initialContentBounds;
		
		skin = customGUISkin;
		
		boundingGUIContent = new GUIContent(apearance.boundingTitle, apearance.boundingImage);
		
		scrollViewRect = bounds;
		
		if (contentBounds.width == 0)
			contentBounds.width = bounds.width;
		if (contentBounds.height == 0)
			contentBounds.height = bounds.height;
		
		scrollViewRect.width = contentBounds.width;
		scrollViewRect.height = contentBounds.height;
		
		SnapBoundsRectIntoPosition();
		
	}
	
	protected virtual void OnEnable()
	{
		scrollPosition = Vector2.zero;
	}
	
	protected virtual void OnDisable()
	{}
	
	void Reset()
	{
		bounds = new Rect(0,0,512,386);
	}
	
	void OnGUI()
	{
		// rendering attributes
		//TODO: Move more of these into the apearance?
		GUI.skin = skin;
		GUI.depth = depth;
		GUI.color = apearance.color;
		
		if ( RequiresSnap )
		{
			if( syncBoundsPosition )
			{
				bounds = initialBounds;
			}
			SnapBoundsRectIntoPosition();
			
		}
		else
		{
			if( syncBoundsPosition )
			{
				bounds.width = initialBounds.width;
				bounds.height = initialBounds.height;
			}
		}
		if( RequiresWindow )
		{
			bounds = GUI.Window(windowInfo.WindowId, bounds, DrawWindow, boundingGUIContent, apearance.boundingStyle);
		}
		else
		{
			GUI.BeginGroup(bounds);
			Rect box = bounds;
			box.x = 0;
			box.y = 0;
			GUI.Box(box, boundingGUIContent, apearance.boundingStyle);
			StartContentRegion();
			DrawGUI(0);
			EndContentRegion();	
			GUI.EndGroup();
		}
	}

	protected void StartContentRegion()
	{
		if( syncContentPosition )
		{
			contentBounds = initialContentBounds;
		}
		if (useScrollView)
		{
			scrollPosition = GUI.BeginScrollView(contentBounds, scrollPosition, scrollViewRect, alwaysShowHorizontalScrollBar, alwaysShowVerticalScrollBar);
		}
		else
		{
			GUI.BeginGroup(contentBounds);
		}
	}

	protected void EndContentRegion()
	{
		if (useScrollView)
		{
			GUI.EndScrollView();
		}
		else
		{
			GUI.EndGroup();
		}
	}

	protected virtual void DrawWindow(int windowId)
	{
		StartContentRegion();
		DrawGUI(windowId);
		EndContentRegion();
		if( windowInfo.supportsDragging)
		{
			GUI.DragWindow();
		}
	}


	protected void DrawGUI(int windowId)
	{
		Color old_color = GUI.color;
		Color new_color = old_color;
		new_color.a *= 0.5f;
		
		GUI.color = new_color;
		Rect nbb = contentBounds;
		nbb.x=0;
		nbb.y=0;
		GUI.Box( nbb, "Content Goes Here!", "ContentStyle");
		GUI.color = old_color;
	}
	
	public void ResetScrollPosition()
	{
		scrollPosition = Vector3.zero;
	}
	
	public float ScrollViewContentWidth
	{
		set { scrollViewRect.width = value; }
	}

	public float ScrollViewContentHeight
	{
		set { scrollViewRect.height = value; }
	}
	
	public float ContentWidth
	{
		get
		{
			return contentBounds.width;
		}
	}

	public float ContentHeight
	{
		get
		{
			return contentBounds.height;
		}
	}
	
	public string Title
	{
		get { return apearance.boundingTitle; }
		set
		{
			apearance.boundingTitle = value;
			boundingGUIContent = new GUIContent(apearance.boundingTitle, apearance.boundingImage);
		}
	}

	#region Utility
	public virtual void ResetToDefaultConfiguration()
	{}
	#endregion
}
