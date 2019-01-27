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
    [SerializeField] private AudioClip[] SpookSounds;
    [SerializeField] private AudioClip[] NervousSounds;
    private SpookStatus status = SpookStatus.Normal;
    private SpriteRenderer renderer;
    private AudioSource audio;
    private Vector3 _origPos;

    // Start is called before the first frame update
    void Start()
    {
        _origPos = transform.position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2, 3), Random.Range(-2, 3));
        renderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    public void SetNervous()
    {
        if (status == SpookStatus.Normal)
        {
            status = SpookStatus.Nervous;
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 2;
            audio.clip = NervousSounds[Random.Range(0, NervousSounds.Length)];
            audio.Play();
        }
    }

    public void SetSpook()
    {
        if (status != SpookStatus.Spooked)
        {
            status = SpookStatus.Spooked;
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 4;
            audio.clip = SpookSounds[Random.Range(0, SpookSounds.Length)];
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        renderer.sprite = status == SpookStatus.Nervous ? nervousSprite
                        : status == SpookStatus.Spooked ? scaredSprite
                        : baseSprite;
        Vector3 moveDirection = gameObject.transform.position - _origPos; 
        if (moveDirection != Vector3.zero) 
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
