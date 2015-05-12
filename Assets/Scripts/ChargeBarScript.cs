using UnityEngine;
using System.Collections;

public class ChargeBarScript : MonoBehaviour {

    public Transform full;

    public void SetAngle(float angle)
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        transform.Rotate(new Vector3(0f, 0f, angle - currentAngle));
    }

    public void SetValue(float value)
    {
        full.localScale = new Vector3(value,
                                      full.localScale.y,
                                      full.localScale.z);
    }
}
