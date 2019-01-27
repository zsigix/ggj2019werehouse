using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrushAnimation : MonoBehaviour
{
    [SerializeField] private Sprite Frame1;
    [SerializeField] private Sprite Frame2;
    [SerializeField] private Sprite Frame3;
    [SerializeField] private Sprite Frame4;
    [SerializeField] private Sprite Frame5;
    [SerializeField] private Sprite Frame6;
    [SerializeField] private Sprite Frame7;
    [SerializeField] private Sprite Frame8;

    private SpriteRenderer renderer;
    private ParticleSystem bloodGush;
    private bool isCrushing;
    private float crushDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        bloodGush = GetComponent<ParticleSystem>();
        renderer.sprite = Frame1;
        isCrushing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isCrushing)
        {
            isCrushing = true;
            // Nervous changes
            foreach(Collider2D coll in Physics2D.OverlapCircleAll(gameObject.transform.position, 3))
            {
                if (coll.tag == "Victim")
                {
                    coll.gameObject.GetComponent<VictimControls>().SetNervous();
                }
            }
            // Spook changes
            foreach(Collider2D coll in Physics2D.OverlapCircleAll(gameObject.transform.position, 1))
            {
                if (coll.tag == "Victim")
                {
                    coll.gameObject.GetComponent<VictimControls>().SetSpook();
                }
            }
        }
        if (isCrushing)
        {
            crushDuration += Time.deltaTime;
            renderer.sprite = crushDuration > 0 && crushDuration <= 0.0625 ? Frame2
                            : crushDuration > 0.0625 && crushDuration <= 0.125 ? Frame3
                            : crushDuration > 0.125 && crushDuration <= 0.1875 ? Frame4
                            : crushDuration > 0.1875 && crushDuration <= 0.250 ? Frame5 
                            : crushDuration > 0.250 && crushDuration <= 0.3125 ? Frame6 
                            : crushDuration > 0.3125 && crushDuration <= 0.3750 ? Frame7 
                            : crushDuration > 0.3750 && crushDuration <= 0.4375 ? Frame8 : Frame1;
            if (crushDuration > 0.0625 && crushDuration < 0.125)
            {
                foreach(Collider2D coll in Physics2D.OverlapCircleAll(gameObject.transform.position, 1f))
                {
                    if (coll.tag == "Victim")
                    {
                        foreach(AudioSource sound in GetComponents<AudioSource>())
                        {
                            sound.Play();
                        }
                        bloodGush.Stop();
                        bloodGush.Play();
                        Object.Destroy(coll.gameObject);
                    }
                }
            }
            
            if (crushDuration > 0.4375)
            {
                isCrushing = false;
                crushDuration = 0;
            }
        }
    }
}
