using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Notes : int { Whole, Half, Quarter, Eighth, HalfDot, QuarterDot, EighthDot, WholeRest };

public class NewGeneratorController : MonoBehaviour
{

    private float waitTime;
    public float BPM = 64f;
    public float beat;
    float thisHeight, platHeight;
    public bool isTreble = true;
    public GameObject platGO;
    private Renderer thisRend;
    private Collider2D platCol;
    private SpriteRenderer platRend;

    public string path = "Audio/Notes";

    private struct Platform
    {
        public List<string> note;//name of note to be played
        public float nextBeat;//the time of the next beat
        public List<float> height;//position to generate note on the screen
        public int length;//duration of the note

        public Platform(List<string> n, int l, float t, List<float> h)
        {
            note = n;
            length = l;
            nextBeat = t;
            height = h;
        }
    }

    private List<Platform> notesList;

    // Use this for initialization
    void Start()
    {
        //calculate dimensions of the platform generator
        thisRend = GetComponent<Renderer>();
        thisHeight = thisRend.bounds.extents.y;
        platCol = platGO.GetComponent<Collider2D>();
        platHeight = platCol.bounds.max.y - platCol.bounds.min.y;

        beat = 60 / BPM;

        //get arbitrary start time
        waitTime = beat;

        notesList = new List<Platform>();
        //prepare the song
        getSong(isTreble);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.time >= waitTime && notesList.Count != 0)
            GenerateNote();
    }

    void GenerateNote()
    {
        //dequeue next element in list
        Platform platvars = notesList[0];
        notesList.Remove(platvars);
        float yCoord = transform.position.y - thisHeight;

        for (int i = 0; i < platvars.note.Count; i++) { 
            //create new platform at a y-coordiante determined by platvar.height and make a handle for it
            GameObject plat = Instantiate(platGO, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), transform.rotation) as GameObject;
            //set note sprite based on type
            platRend = plat.GetComponent<SpriteRenderer>();
            NewNoteBehavior script = plat.GetComponent<NewNoteBehavior>();
            if (platvars.length == 7)
            {
                platRend.sprite = script.noteTypes[14];
                script.curSprite = 14;
            }
            else {
                platRend.sprite = script.noteTypes[platvars.length * 2 + 1];
                script.curSprite = platvars.length * 2 + 1;
            }
            //grab handle to AudioSource component of new platform
            AudioSource source = plat.GetComponent<AudioSource>();
            //set its audio clip to the note named in the list element
            source.clip = Resources.Load(path + "/" + platvars.note[i]) as AudioClip;
    }

        //set reset timer
        waitTime = platvars.nextBeat;
    }

    void getSong(bool treb)
    {
        if (treb)
        {
            //1
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Quarter, beat * 2, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Quarter, beat * 3, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Quarter, beat * 4, new List<float>() { 0 }));

            //2
            notesList.Add(new Platform(new List<string>() { "F4" }, (int)Notes.Eighth, beat * 4.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E5" }, (int)Notes.Eighth, beat * 5, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E5" }, (int)Notes.HalfDot, beat * 9, new List<float>() { 0 }));

            //3
            notesList.Add(new Platform(new List<string>() { "D5" }, (int)Notes.Eighth, beat * 9.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E5" }, (int)Notes.Eighth, beat * 10, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D5" }, (int)Notes.Eighth, beat * 10.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "C5" }, (int)Notes.Eighth, beat * 11, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Eighth, beat * 11.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Eighth, beat * 12, new List<float>() { 0 }));

            //4
            notesList.Add(new Platform(new List<string>() { "G4" }, (int)Notes.Eighth, beat * 12.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Eighth, beat * 13, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D5" }, (int)Notes.HalfDot, beat * 17, new List<float>() { 0 }));

            //5
            notesList.Add(new Platform(new List<string>() { "C5" }, (int)Notes.Eighth, beat * 17.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D5" }, (int)Notes.Eighth, beat * 18, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "C5" }, (int)Notes.Eighth, beat * 18.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Eighth, beat * 19, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Eighth, beat * 19.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D4" }, (int)Notes.Eighth, beat * 20, new List<float>() { 0 }));

            //6
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Eighth, beat * 20.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "C5" }, (int)Notes.Eighth, beat * 21, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "C5" }, (int)Notes.HalfDot, beat * 25, new List<float>() { 0 }));

            //7
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Eighth, beat * 25.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "C5" }, (int)Notes.Eighth, beat * 26, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D5" }, (int)Notes.Eighth, beat * 26.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "C5" }, (int)Notes.Eighth, beat * 27, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Eighth, beat * 27.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Eighth, beat * 28, new List<float>() { 0 }));

            //8
            notesList.Add(new Platform(new List<string>() { "G4" }, (int)Notes.Eighth, beat * 28.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Eighth, beat * 29, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.HalfDot, beat * 32, new List<float>() { 0 }));

            //9
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Half, beat * 34, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Quarter, beat * 34.66f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Quarter, beat * 35.33f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Quarter, beat * 36, new List<float>() { 0 }));
        }
        else
        {
            //1
            notesList.Add(new Platform(new List<string>() { "E6" }, (int)Notes.WholeRest, beat * 4, null));

            //2
            notesList.Add(new Platform(new List<string>() { "D3" }, (int)Notes.Eighth, beat * 4.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A3" }, (int)Notes.Eighth, beat * 5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D4" }, (int)Notes.Eighth, beat * 5.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "F4" }, (int)Notes.Eighth, beat * 6f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Half, beat * 8f, new List<float>() { 0 }));

            //3
            notesList.Add(new Platform(new List<string>() { "G2" }, (int)Notes.Eighth, beat * 8.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "G3" }, (int)Notes.Eighth, beat * 9f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D4" }, (int)Notes.Eighth, beat * 9.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "G4" }, (int)Notes.Eighth, beat * 10f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Half, beat * 12f, new List<float>() { 0 }));

            //4
            notesList.Add(new Platform(new List<string>() { "E3" }, (int)Notes.Eighth, beat * 12.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B3" }, (int)Notes.Eighth, beat * 13f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Eighth, beat * 13.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "G4" }, (int)Notes.Eighth, beat * 14f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Half, beat * 16f, new List<float>() { 0 }));

            //5
            notesList.Add(new Platform(new List<string>() { "A2" }, (int)Notes.Eighth, beat * 16.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A3" }, (int)Notes.Eighth, beat * 17f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Eighth, beat * 17.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Eighth, beat * 18f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "C5" }, (int)Notes.Half, beat * 20f, new List<float>() { 0 }));

            //6
            notesList.Add(new Platform(new List<string>() { "D3" }, (int)Notes.Eighth, beat * 20.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A3" }, (int)Notes.Eighth, beat * 21f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D4" }, (int)Notes.Eighth, beat * 21.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "F4" }, (int)Notes.Eighth, beat * 22f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A4" }, (int)Notes.Half, beat * 24f, new List<float>() { 0 }));

            //7
            notesList.Add(new Platform(new List<string>() { "G2" }, (int)Notes.Eighth, beat * 24.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "G3" }, (int)Notes.Eighth, beat * 25f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "D4" }, (int)Notes.Eighth, beat * 25.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "G4" }, (int)Notes.Eighth, beat * 26f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Half, beat * 28f, new List<float>() { 0 }));

            //8
            notesList.Add(new Platform(new List<string>() { "E3" }, (int)Notes.Eighth, beat * 28.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B3" }, (int)Notes.Eighth, beat * 29f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Eighth, beat * 29.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "G4" }, (int)Notes.Eighth, beat * 30f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "B4" }, (int)Notes.Half, beat * 32f, new List<float>() { 0 }));

            //9
            notesList.Add(new Platform(new List<string>() { "A2" }, (int)Notes.Eighth, beat * 32.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "A3" }, (int)Notes.Eighth, beat * 33f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "C#-Db4" }, (int)Notes.Eighth, beat * 33.5f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "E4" }, (int)Notes.Eighth, beat * 34f, new List<float>() { 0 }));
            notesList.Add(new Platform(new List<string>() { "G4" }, (int)Notes.Half, beat * 36f, new List<float>() { 0 }));

        }
    }
}
