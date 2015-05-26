using UnityEngine;
using System.Collections;

public class ChargeBarScript : MonoBehaviour {

    public Transform scalar;
    public GameObject full;
    public Color color0;
    public Color color1;

    private Material mat;

    void Start()
    {
        mat = full.GetComponent<MeshRenderer>().material;
    }

    public void SetAngle(float angle)
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        transform.Rotate(new Vector3(0f, 0f, angle - currentAngle));
    }

    public void SetValue(float value)
    {
        scalar.localScale = new Vector3(value,
                                      scalar.localScale.y,
                                      scalar.localScale.z);

        mat.color = Color.Lerp(color0, color1, value);
    }
}
