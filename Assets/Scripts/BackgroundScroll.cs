using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

    public float scrollSpeed;

    private Material mat;
    private float xOffset = 0f;

	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        xOffset += scrollSpeed * Time.deltaTime;
        mat.mainTextureOffset = new Vector2(xOffset, 0f);
	}
}
