using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    private bool shaking = false;

    public void StartShaking(float duration, float magnitude)
    {
        if (!shaking) { StartCoroutine(Shake(duration, magnitude)); }
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        shaking = true;

        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position - transform.parent.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            Camera.main.transform.position = new Vector3(transform.parent.position.x + originalCamPos.x + x,
                                                         transform.parent.position.y + originalCamPos.y + y,
                                                         originalCamPos.z);

            yield return null;
        }
        transform.position = originalCamPos + transform.parent.position;
        shaking = false;
    }
}
