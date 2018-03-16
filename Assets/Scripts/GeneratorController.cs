using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour {

    private float waitTime;
    public float BPM = 115f;
    public float beat;
    float thisHeight, platHeight;
    public bool isTreble = true;
    public GameObject platGO;
    private Renderer thisRend;
    private Collider2D platCol;

    public string path = "Audio/Notes";

    private struct Platform {
        public string note;//name of note to be played
        public float timeToNext;//time until next note in seconds
        public float height;//position to generate note on the screen

        public Platform(string n, float t, float h)
        {
            note = n;
            timeToNext = t;
            height = h;
        }
    }

    private List<Platform> notesList;

    // Use this for initialization
    void Start () {
        //calculate dimensions of the platform generator
        thisRend = GetComponent<Renderer>();
        thisHeight = thisRend.bounds.extents.y;
        platCol = platGO.GetComponent<Collider2D>();
        platHeight = platCol.bounds.max.y - platCol.bounds.min.y;

        beat = 60/BPM;

        //get arbitrary start time
        waitTime = beat;

        notesList = new List<Platform>();
        //prepare the song
        getSong(isTreble);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Time.time >= waitTime && notesList.Count != 0)
            GenerateNote();
	}

    void GenerateNote()
    {
        //dequeue next element in list
        Platform platvars = notesList[0];
        notesList.Remove(platvars);
        float yCoord = transform.position.y - thisHeight;

        //create new platform at a y-coordiante determined by platvar.height and make a handle for it
        GameObject plat = Instantiate(platGO, new Vector3(transform.position.x, 
            yCoord+((platHeight*2f)*platvars.height)), transform.rotation) as GameObject;
        //grab handle to AudioSource component of new platform
        AudioSource source = plat.GetComponent<AudioSource>();
        //set its audio clip to the note named in the list element
        source.clip = Resources.Load(path+"/"+platvars.note) as AudioClip;

        //set reset timer
        waitTime = platvars.timeToNext;
    }

    void getSong(bool treb)
    {
        if (treb)
        {
            notesList.Add(new Platform("E4", beat, 4));
            notesList.Add(new Platform("E4", beat * 2, 4));
            notesList.Add(new Platform("E4", beat * 3, 4));
        }
        else
        {
            notesList.Add(new Platform("E3", beat, 4));
            notesList.Add(new Platform("E3", beat * 2, 4));
            notesList.Add(new Platform("E3", beat * 3, 4));
        }
    }
}
