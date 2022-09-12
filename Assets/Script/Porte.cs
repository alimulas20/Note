using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Porte : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> images;
    public Image image;
    int imageNumber;
    void Start()
    {
        imageNumber = 0;
    }
    // Update is called once per frame
    public void setImage(int i)
    {
        imageNumber = i;
        image.sprite = images[i];
    }
    public int getImage()
    {
        return imageNumber;
    }
}
