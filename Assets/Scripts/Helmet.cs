using UnityEngine;
using System.Collections;

public class Helmet : MonoBehaviour
{

    public enum HelmetType
    {
        None,
        Helmet,
        HardHat,
        Spiked
    }

    public Sprite helmetSprite;
    public Sprite hardHatSprite;
    public Sprite spikedSprite;

    private HelmetType helmet;

    public float damageMult
    {
        get
        {
            if (helmet == HelmetType.Spiked) { return 1.5f; }
            else { return 1f; }
        }
    }

    public float damageTakenMult
    {
        get
        {
            if (helmet == HelmetType.HardHat) { return 0.5f; }
            else { return 1f; }
        }
    }

    // Use this for initialization
    void Start()
    {
        helmet = HelmetType.None;
    }

    public void SetHelmet(HelmetType helm)
    {
        helmet = helm;
        var renderer = GetComponent<SpriteRenderer>();

        switch (helm)
        {
            case HelmetType.None:
                break;
            case HelmetType.Helmet:
                renderer.sprite = helmetSprite;
                break;
            case HelmetType.HardHat:
                renderer.sprite = hardHatSprite;
                break;
            case HelmetType.Spiked:
                renderer.sprite = spikedSprite;
                break;
        }
    }
}
