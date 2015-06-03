using UnityEngine;
using System.Collections;

public class TileTextureX : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        var mat = GetComponent<MeshRenderer>().material;
        mat.mainTextureScale = new Vector2(transform.localScale.x, 1f);
	}
}
