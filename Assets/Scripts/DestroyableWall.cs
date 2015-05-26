using UnityEngine;
using System.Collections;

public class DestroyableWall : MonoBehaviour {

    public GameObject particle;

    void HitByPlayer()
    {
        var ps = Instantiate<GameObject>(particle);
        ps.transform.position = this.transform.position;

        Destroy(ps, ps.GetComponent<ParticleSystem>().duration);
        Destroy(this.gameObject);
    }
}
