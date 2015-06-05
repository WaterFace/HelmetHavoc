using UnityEngine;
using System.Collections;

public class DestroyableWall : MonoBehaviour {

    public GameObject particle;
    public GameObject[] extraParticles;


    void HitByPlayer()
    {
        var ps = Instantiate<GameObject>(particle);
        ps.transform.position = this.transform.position;

        Destroy(ps, ps.GetComponent<ParticleSystem>().duration);
        Destroy(this.gameObject);
        
        foreach (var part in extraParticles)
        {
            var eps = Instantiate<GameObject>(part);
            eps.transform.position = this.transform.position;

            Destroy(eps, eps.GetComponent<ParticleSystem>().duration);
        }
    }
}
