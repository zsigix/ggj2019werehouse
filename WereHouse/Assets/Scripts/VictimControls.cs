using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpookStatus
{
    Normal = 0,
    Nervous = 1,
    Spooked = 2
}

public class VictimControls : MonoBehaviour
{
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite nervousSprite;
    [SerializeField] private Sprite scaredSprite;
    private SpookStatus status = SpookStatus.Normal;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1, 3), Random.Range(1, 3));
        renderer = GetComponent<SpriteRenderer>();
    }

    public void SetNervous()
    {
        if (status == SpookStatus.Normal)
            status = SpookStatus.Nervous;
    }

    public void SetSpook()
    {
        status = SpookStatus.Spooked;
    }

    // Update is called once per frame
    void Update()
    {
        renderer.sprite = status == SpookStatus.Nervous ? nervousSprite
                        : status == SpookStatus.Spooked ? scaredSprite
                        : baseSprite;
    }
}
