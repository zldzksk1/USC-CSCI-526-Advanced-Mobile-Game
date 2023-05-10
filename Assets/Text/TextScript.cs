using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    GameObject[] Child = new GameObject[100];
    public string EnterTextHere;
    int SizeOfString;

    public GameObject TextAppearingPosRot;
    public float GapBetweenCharacters;
    public float RotationInX;
    public float RotationInY;
    public float RotationInZ;

    float Half = .8f;
    float More = 1.35f;

    GameObject[] gameObjectInstantiated;

    private Vector3 initialPosition;
    private bool showText;


    // Start is called before the first frame update
    void Start()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///if You want to show any integer as 3D text then just use below line 
        //int speed=459;
        //EnterTextHere = speed.ToString();
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        Debug.Log("Entering TextScript");

        if(!TextManager.Instance)
        {
            Debug.LogError("THIS SCENE NEEDS A TEXT MANAGER!");
        }


        for (int i = 0; i <= 93; i++)
        {
            Child[i] = TextManager.Instance.textList.transform.GetChild(i).gameObject;
        }

        initialPosition = TextAppearingPosRot.transform.position;

        showText = true;

        ChangeText(EnterTextHere);
        Debug.Log("Ending TextScript");

    }

    public void ShowText(bool value)
    {
        showText = value;
        foreach (GameObject gObject in gameObjectInstantiated)
        {
            //Debug.Log(gObject);
            gObject.GetComponent<Renderer>().enabled = value;
            //gObject.SetActive(value);
        }
    }

    public void ChangeText(string text)
    {
        TextAppearingPosRot.transform.position = initialPosition;

        EnterTextHere = text;
        
        CreateText(text);
        int num;
    }

    void CreateText(string text)
    {
        if(gameObjectInstantiated != null)
        {
            foreach(GameObject gObject in gameObjectInstantiated)
            {
                Destroy(gObject);
            }
        }

        SizeOfString = text.Length;
        //Debug.Log(SizeOfString);
        gameObjectInstantiated = new GameObject[SizeOfString];
        for (int j = 0; j < SizeOfString; j++)
        {
            char Character = EnterTextHere[j];

            switch (Character)
            {
                case '0':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[0], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '1':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[1], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '2':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[2], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '3':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[3], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '4':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[4], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;

                case '5':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[5], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '6':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[6], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '7':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[7], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '8':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[8], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '9':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[9], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'a':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[10], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'b':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[11], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'c':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[12], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'd':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[13], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'e':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[14], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'f':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[15], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'g':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[16], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'h':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[17], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;

                case 'i':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[18], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half * .8f;
                    }
                    break;
                case 'j':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[19], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'k':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[20], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'l':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[21], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'm':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[22], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'n':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[23], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'o':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[24], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'p':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[25], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'q':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[26], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'r':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[27], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 's':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[28], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 't':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[29], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'u':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[30], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'v':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[31], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'w':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[32], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'x':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[33], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'y':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[34], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'z':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[35], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '}':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[36], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'A':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[37], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'B':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[38], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'C':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[39], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'D':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[40], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'E':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[41], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'F':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[42], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'G':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[43], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'H':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[44], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'I':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[45], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'J':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[46], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case 'K':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[47], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'L':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[48], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'M':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[49], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'N':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[50], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'O':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[51], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'P':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[52], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'Q':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[53], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'R':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[54], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'S':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[55], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case 'T':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[56], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'U':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[57], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'V':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[58], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'W':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[59], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More * 1.5f;
                    }
                    break;
                case 'X':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[60], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'Y':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[61], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case 'Z':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[62], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case '!':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[63], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '"':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[64], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '#':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[65], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '$':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[66], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '%':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[67], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More;
                    }
                    break;
                case '&':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[68], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '\'':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[69], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '(':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[70], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case ')':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[71], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '*':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[72], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '+':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[73], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case ',':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[74], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '-':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[75], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case ':':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[76], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case ';':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[77], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '<':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[78], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '=':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[79], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '>':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[80], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '?':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[81], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '@':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[82], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * More * 1.5f;
                    }
                    break;
                case '[':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[83], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '\\':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[84], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case ']':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[85], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '^':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[86], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '_':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[87], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '`':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[88], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '.':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[89], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '/':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[90], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '{':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[91], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case '|':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[92], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters * Half;
                    }
                    break;
                case '~':
                    {
                        gameObjectInstantiated[j] = Instantiate(Child[93], TextAppearingPosRot.transform.position, TextAppearingPosRot.transform.rotation);
                        gameObjectInstantiated[j].transform.Rotate(RotationInX, RotationInY + 180, RotationInZ);
                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;
                case ' ':
                    {

                        TextAppearingPosRot.transform.position += (TextAppearingPosRot.transform.right) * GapBetweenCharacters;
                    }
                    break;

            }
            gameObjectInstantiated[j].transform.SetParent(transform);
            gameObjectInstantiated[j].GetComponent<Renderer>().enabled = showText;
        }
    }

}
