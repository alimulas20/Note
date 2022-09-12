using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Note : MonoBehaviour
{
    public Sprite []image;
    public Image noteImage;
    bool isSeleceted;
    bool picked;
    int imageNumber;
    int localNoteNumber;
    // Start is called before the first frame update
    void Start()
    {

       
        
    }

    // Update is called once per frame
    void Update()
    {
    }
  
    public void setImage(int index)
    {
        noteImage.sprite = image[index];
        imageNumber = index;
        if (index == 2)
        {
            noteImage.rectTransform.sizeDelta = new Vector2(50, 120);
        }
        else if (index < 3)
        {
            noteImage.rectTransform.sizeDelta = new Vector2(30, 120);
        }
        else if (index < 4)
        {
            noteImage.rectTransform.sizeDelta = new Vector2(140, 120);
        }
        else
        {
            noteImage.rectTransform.sizeDelta = new Vector2(80, 120);
        }
    }
    public void setColor(int colorValue)
    {
        switch (colorValue)
        {
            case 1:
                noteImage.color = new Color32(0xD4,0x66,0xA3,0xFF);
                return;
            case 2:
                noteImage.color = new Color32(0x7E,0x83,0x1A,0xFF);
                return;
            case 3:
                noteImage.color = new Color32(0x83,0x00,0x0C,0xFF);
                return;
            case 4:
                noteImage.color = new Color32(0x83,0x41,0x82,0xFF);
                return;
            case 5:
                noteImage.color = new Color32(0x47, 0x81, 0x83, 0xFF);
                return;
            case 6:
                noteImage.color = new Color32(0xA0,0x87,0x6D,0xFF);
                return;
            default:
                return;
        }
            
    }
    public void select()
    {
        isSeleceted = true;
    }
    public bool isPicked()
    {
        return picked;
    }
    public int getImage()
    {
        return imageNumber;
    }
    public void setLocalNoteNumber(int i)
    {
        localNoteNumber = i;
    }
    public int getLocalNoteNumber()
    {
        return localNoteNumber;
    }
}
