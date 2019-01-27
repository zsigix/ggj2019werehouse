using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using rho;

public class TheChompAnimation : MonoBehaviour
{
    [SerializeField] private Sprite Frame1;
    [SerializeField] private Sprite Frame2;
    [SerializeField] private Sprite Frame3;
    [SerializeField] private Sprite Frame4;
    [SerializeField] private Sprite Frame5;

    private SpriteRenderer renderer;
    private ParticleSystem bloodGush;
    private bool isChomping;
    private float chompDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        bloodGush = GetComponent<ParticleSystem>();
        renderer.sprite = Frame1;
        isChomping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isChomping)
        {
            isChomping = true;
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
        if (isChomping)
        {
            chompDuration += Time.deltaTime;
            renderer.sprite = chompDuration > 0 && chompDuration <= 0.0625 ? Frame2
                            : chompDuration > 0.0625 && chompDuration <= 0.125 ? Frame3
                            : chompDuration > 0.125 && chompDuration <= 0.1875 ? Frame4
                            : chompDuration > 0.1875 && chompDuration <= 0.250 ? Frame5 : Frame1;
            if (chompDuration > 0.1875 && chompDuration < 0.250)
            {
                foreach(Collider2D coll in Physics2D.OverlapCircleAll(gameObject.transform.position, 0.5f))
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
                        GlobalEventHandler.SendEvent(new GameEvents.VictimDied{});
                    }
                }
            }
            
            if (chompDuration > 0.25)
            {
                isChomping = false;
                chompDuration = 0;
            }
        }
    }
}
