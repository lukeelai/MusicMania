using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour {

	[HideInInspector]public bool activated;

	public float flyspeed = 1f;
    private AudioSource source;
    private bool destroyed;

    public Sprite[] noteTypes;

	// Use this for initialization
	void Start () {
		activated = false;
		transform.Rotate (Vector3.forward * 180);
        source = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
		if(activated)
		    transform.Translate (-flyspeed/100, 0, 0);
		else
			transform.Translate (flyspeed/100, 0, 0);
        if (destroyed && !source.isPlaying)
            Destroy(this.gameObject);

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player") && !activated) {
			activated = true;
			transform.Rotate (Vector3.forward * 180);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Reg") && activated)
        {
            if (source != null)
                source.Play();
            transform.Translate(new Vector3(500, 0, 0));
            destroyed = true;
        }
	}
}
