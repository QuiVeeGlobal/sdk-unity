using UnityEngine;
using System.Collections;

public class ToggleFBShop: MonoBehaviour {
	
	public GameObject target = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		Debug.Log("OnMouseDown called!");
		target.GetComponent<RoarFacebookShopWidget>().enabled = ! target.GetComponent<RoarFacebookShopWidget>().enabled;
	}
}
