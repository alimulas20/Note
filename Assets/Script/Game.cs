using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game : MonoBehaviour
{
    public Porte porte;
    public GameObject porteArea;
    public Timer slider;
    List<Porte> portes=new List<Porte>();
    Note[] noteList = new Note[15];
    public Note note;
    int level;
    int[] levelCount = {6,7,8,9,7,7, 7, 7, 7, 7, 7, 7, 10, 10, 10, 10, 9, 9 };
    int[] typeCount = { 1,2,2,2,1,3,2,3,1,4,2,4,1,4,2,4,2,5};
    int[] colorCount = {2,1,2,2,3,1,3,2,4,1,4,2, 4, 1, 4, 2 ,5,2};
    int[,] newPos = { {-20,15}, {20,15},{ -5,40}, { -10, 0 }, { 20, 20 } };
    int[] addedNotes;
    int selected=-1;
    int solution;
    float height = Screen.height;
    float width = Screen.width;
    bool type;
    // Start is called before the first frame update
    void Start()
    {

        porteCreator(false);
        
        /*noteList = new Note[15];
        for (int i = 0; i < 15; i++)
        {
            int localNoteNumber = i % 5;
            int porteNumber = i / 5;
            Vector2 notePos = new Vector2((120 + localNoteNumber * 150) * (width / 800), portes[porteNumber].transform.position.y);
            noteList[i] = Instantiate(note, notePos, portes[porteNumber].transform.rotation, portes[porteNumber].transform);
            noteList[i].setImage(1);
            positioner(i, localNoteNumber);

        }*/
        noteGenerator();
        
        

    }
    void positioner(int i)
    {
        Note newNote = noteList[i];
        Vector2 position = newNote.transform.position;
        if(newNote.getImage()!=1)
            position.y = position.y + newPos[newNote.getLocalNoteNumber(), 1];
        else
            position.y = position.y -30;
        noteList[i].transform.DOMove(position,1f).SetAutoKill();
        newNote.transform.DORotate(new Vector3(0, 0, newPos[noteList[i].getLocalNoteNumber(), 0]), 1f).SetAutoKill();
    }
    // Update is called once per frame
    void Update()
    {
        if (slider.stopTimer)
        {
            finish();
        }
    }
    void noteGenerator()
    {
        float width = Screen.width;
        noteList = new Note[15];
        addedNotes = new int[levelCount[level]];
        for (int i = 0; i < levelCount[level]; i++)
        {
            int noteNumber = Random.Range(1, 15);
           
            
            bool isLocated = false;
            while (!isLocated)
            {
                int localNoteNumber = noteNumber % 5;
                int porteNumber = noteNumber / 5;
                if (noteList[noteNumber] == null)
                {
                    Vector2 notePos = new Vector2((120 + localNoteNumber * 150)*(width/800), portes[porteNumber].transform.position.y);
                    noteList[noteNumber] = Instantiate(note, notePos, portes[porteNumber].transform.rotation, portes[porteNumber].transform);
                    addedNotes[i] = noteNumber;
                    noteList[noteNumber].setLocalNoteNumber(localNoteNumber);
                        
                    isLocated = true;
                }
                else
                {
                    noteNumber=(noteNumber+1)%15;
                   
                }
            }

        }
        levelGenerator();
        if (type)
            for (int i = 0; i < addedNotes.Length; i++)
            {
                positioner(addedNotes[i]);
            }
    }
    void levelGenerator()
    {
        List<int> type=new List<int>();
        List<int> color=new List<int>();
      
      

        for(int i = 0; i < typeCount[level]; i++)
        {
            int typeNew = Random.Range(0, 5);
            while (type.Contains(typeNew))
            {
                typeNew = Random.Range(0, 5);
            }
            type.Add(typeNew);
        }
        for (int i = 0; i < colorCount[level]; i++)
        {
            int colorNew = Random.Range(1, 7);
            while (color.Contains(colorNew))
            {
                colorNew = Random.Range(1, 7);
            }
            color.Add(colorNew);
        }
       
        switch (level)
        {
            case 0:
                for(int i = 0; i < addedNotes.Length; i++)
                {
                    noteList[addedNotes[i]].setImage(type[0]);
                    noteList[addedNotes[i]].setColor(color[0]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[1]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 1:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    noteList[addedNotes[i]].setImage(type[0]);
                    noteList[addedNotes[i]].setColor(color[0]);
                    
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[1]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 2:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if(i<addedNotes.Length/2)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else
                        noteList[addedNotes[i]].setImage(type[1]);
                    noteList[addedNotes[i]].setColor(color[0]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[1]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 3:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if(i<addedNotes.Length/2)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else
                        noteList[addedNotes[i]].setColor(color[1]);
                    noteList[addedNotes[i]].setImage(type[0]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[1]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 4:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    noteList[addedNotes[i]].setImage(type[0]);
                    if(i<3)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else 
                        noteList[addedNotes[i]].setColor(color[1]);
                   
                      
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[2]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 5:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if(i<3)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else
                        noteList[addedNotes[i]].setImage(type[1]);
                    noteList[addedNotes[i]].setColor(color[0]);

                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[2]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 6:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i%2==0)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else
                        noteList[addedNotes[i]].setImage(type[1]);
                    if(i<3)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else
                        noteList[addedNotes[i]].setColor(color[1]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[2]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 7:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i%2==0)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else
                        noteList[addedNotes[i]].setColor(color[1]);
                    if(i<3)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else
                        noteList[addedNotes[i]].setImage(type[1]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[2]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 8:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    noteList[addedNotes[i]].setImage(type[0]);
                    if (i < 2)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else if(i<4)
                        noteList[addedNotes[i]].setColor(color[1]);
                    else
                        noteList[addedNotes[i]].setColor(color[2]);

                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[3]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 9:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i < 2)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else if(i<4)
                        noteList[addedNotes[i]].setImage(type[1]);
                    else
                        noteList[addedNotes[i]].setImage(type[2]);
                    noteList[addedNotes[i]].setColor(color[0]);

                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[3]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 10:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i % 2 == 0)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else
                        noteList[addedNotes[i]].setImage(type[1]);
                    if (i < 2)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else if(i<4)
                        noteList[addedNotes[i]].setColor(color[1]);
                    else
                        noteList[addedNotes[i]].setColor(color[2]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[3]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 11:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i % 2 == 0)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else
                        noteList[addedNotes[i]].setColor(color[1]);
                    if (i < 2)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else if(i<4)
                        noteList[addedNotes[i]].setImage(type[1]);
                    else
                        noteList[addedNotes[i]].setImage(type[2]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[3]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 12:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    noteList[addedNotes[i]].setImage(type[0]);
                    if (i < 3)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else if (i < 6)
                        noteList[addedNotes[i]].setColor(color[1]);
                    else
                        noteList[addedNotes[i]].setColor(color[2]);

                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[3]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 13:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i < 3)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else if (i < 6)
                        noteList[addedNotes[i]].setImage(type[1]);
                    else
                        noteList[addedNotes[i]].setImage(type[2]);
                    noteList[addedNotes[i]].setColor(color[0]);

                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[3]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 14:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i % 2 == 0)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else
                        noteList[addedNotes[i]].setImage(type[1]);
                    if (i < 3)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else if (i < 6)
                        noteList[addedNotes[i]].setColor(color[1]);
                    else
                        noteList[addedNotes[i]].setColor(color[2]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[3]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 15:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i % 2 == 0)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else
                        noteList[addedNotes[i]].setColor(color[1]);
                    if (i < 3)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else if (i < 6)
                        noteList[addedNotes[i]].setImage(type[1]);
                    else
                        noteList[addedNotes[i]].setImage(type[2]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[3]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 16:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i % 2 == 0)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else
                        noteList[addedNotes[i]].setImage(type[1]);
                    if (i < 2)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else if (i < 4)
                        noteList[addedNotes[i]].setColor(color[1]);
                    else if(i<6)
                        noteList[addedNotes[i]].setColor(color[2]);
                    else
                        noteList[addedNotes[i]].setColor(color[3]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].setColor(color[4]);
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            case 17:
                for (int i = 0; i < addedNotes.Length; i++)
                {
                    if (i % 2 == 0)
                        noteList[addedNotes[i]].setColor(color[0]);
                    else
                        noteList[addedNotes[i]].setColor(color[1]);
                    if (i < 2)
                        noteList[addedNotes[i]].setImage(type[0]);
                    else if (i < 4)
                        noteList[addedNotes[i]].setImage(type[1]);
                    else if(i<6)
                        noteList[addedNotes[i]].setImage(type[2]);
                    else
                        noteList[addedNotes[i]].setImage(type[3]);
                }
                solution = addedNotes.Length - 1;
                noteList[addedNotes[solution]].select();
                noteList[addedNotes[solution]].setImage(type[4]);
                noteList[addedNotes[solution]].GetComponent<Button>().onClick.AddListener(Picker);
                selected = addedNotes[solution];
                return;
            default:
                return;
        }
    }
    void Picker()
    {
        noteList[addedNotes[solution]].GetComponent<Button>().onClick.RemoveListener(Picker) ;

        for (int i = 0; i < addedNotes.Length; i++)
        {
            noteList[addedNotes[i]].noteImage.DOFade(0, .5f).SetAutoKill();
        }

        StartCoroutine(newLevel());
        
    }
    IEnumerator newLevel()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < addedNotes.Length; i++)
        {
            Destroy(noteList[addedNotes[i]].gameObject);

        }
        if (level == 2||level==8||level==14)
        {
            for (int i = 0; i < portes.ToArray().Length; i++)
                Destroy(portes[i].gameObject);
            porteCreator(true);
        }else if (level == 5 || level == 11)
        {
            for (int i = 0; i < portes.ToArray().Length; i++)
                Destroy(portes[i].gameObject);
            porteCreator(false);
        }
        level++;
        if (level < 18)
            noteGenerator();
        else
            finish();
    }
    void finish()
    {
        Debug.Log("finish");
    }
    void porteCreator(bool curve)
    {
        portes = new List<Porte>();
        int first;
        int second;
        if (curve)
        {
            type = true;
            first = 1;
            second = 3;
        }
        else
        {
            type = false;
            first = 0;
            second = 2;
        }
        for (int i = 0; i < 3; i++)
        {
            portes.Add(Instantiate(porte, new Vector2(porteArea.transform.position.x, porteArea.transform.position.y + (1 - i) * 100 * (height / 480)), porteArea.transform.rotation, porteArea.transform));
            portes[i].setImage(second);
        }
        portes[0].setImage(first);

    }
}
