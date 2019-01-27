using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using rho;

public class BearNomAnimation : MonoBehaviour
{
    [SerializeField] private Sprite Frame1;
    [SerializeField] private Sprite Frame2;


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
        if (Input.GetKeyDown(KeyCode.S) && !isChomping)
        {
            isChomping = true;
            // Nervous changes
            foreach (Collider2D coll in Physics2D.OverlapCircleAll(gameObject.transform.position, 3))
            {
                if (coll.tag == "Victim")
                {
                    coll.gameObject.GetComponent<VictimControls>().SetNervous();
                }
            }
            // Spook changes
            foreach (Collider2D coll in Physics2D.OverlapCircleAll(gameObject.transform.position, 1))
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
            renderer.sprite = chompDuration > 0 && chompDuration <= 0.25 ? Frame2 : Frame1;
            if (chompDuration > 0.0 && chompDuration < 0.250)
            {
                foreach (Collider2D coll in Physics2D.OverlapCircleAll(gameObject.transform.position, 0.5f))
                {
                    if (coll.tag == "Victim")
                    {
                        foreach (AudioSource sound in GetComponents<AudioSource>())
                        {
                            sound.Play();
                        }
                        bloodGush.Stop();
                        bloodGush.Play();
                        Object.Destroy(coll.gameObject);
                        GlobalEventHandler.SendEvent(new GameEvents.VictimDied { });
                    }
                }
            }

            if (chompDuration > 0.27)
            {
                isChomping = false;
                chompDuration = 0;
            }
        }
    }
}
