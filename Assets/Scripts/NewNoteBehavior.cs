using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNoteBehavior : MonoBehaviour
{

    [HideInInspector] public bool activated;

    public float flyspeed = 1f;
    private AudioSource source;
    private bool destroyed;
    SpriteRenderer rend;
    public int curSprite = -1;

    public Sprite[] noteTypes;

    // Use this for initialization
    void Start()
    {
        activated = true;
        source = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-flyspeed / 100, 0, 0);
        if (destroyed && !source.isPlaying)
            Destroy(this.gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Reg") && activated)
        {
            if (source != null)
                source.Play();
            transform.Translate(new Vector3(500, 0, 0));
            destroyed = true;
        }
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            rend.sprite = noteTypes[curSprite-1];
        }
    }
}
