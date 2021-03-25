using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Customisation : MonoBehaviour
{
    public enum BodyParts { Skin, Hair, Mouth, Eyes, Clothes, Armour };

    public List<Texture2D> skinTextures = new List<Texture2D>();
    public List<Texture2D> hairTextures = new List<Texture2D>();
    public List<Texture2D> mouthTextures = new List<Texture2D>();
    public List<Texture2D> eyesTextures = new List<Texture2D>();
    public List<Texture2D> clothesTextures = new List<Texture2D>();
    public List<Texture2D> armourTextures = new List<Texture2D>();

    private int currentTextureIndex = 0;
    //skinTextures[0] = Skin_0
    //skinTextures[1] = Skin_1
    //skinTextures[2] = Skin_2

    //Renderer for our character mesh
    public Renderer characterRenderer;

    private void Start()
    {
        GrabTextures();

        foreach(BodyParts part in Enum.GetValues(typeof(BodyParts)))
        {
            SetTexture(part, 0);
        }
    }

    void GrabTextures()
    {
        //loop through each body part
        foreach (BodyParts part in Enum.GetValues(typeof(BodyParts)))
        {
            Texture2D temporaryTexture;
            int textureCount = 0;

            do// loop through each texture for the body part
            {
                temporaryTexture = (Texture2D)Resources.Load("Warrior Babe/Character/" + part + "_" + textureCount);// as Texture2D;
                if (temporaryTexture != null)
                {
                    switch (part)
                    {
                        case BodyParts.Skin:
                            skinTextures.Add(temporaryTexture);
                            break;
                        case BodyParts.Hair:
                            hairTextures.Add(temporaryTexture);
                            break;
                        case BodyParts.Mouth:
                            mouthTextures.Add(temporaryTexture);
                            break;
                        case BodyParts.Eyes:
                            eyesTextures.Add(temporaryTexture);
                            break;
                        case BodyParts.Clothes:
                            clothesTextures.Add(temporaryTexture);
                            break;
                        case BodyParts.Armour:
                            armourTextures.Add(temporaryTexture);
                            break;
                    }
                }
                textureCount++;
            } while(temporaryTexture != null);
        }
    }

    void SetTexture(BodyParts bodyPart, int direction)
    {
        List<Texture2D> textures;
        switch (bodyPart)
        {
            case BodyParts.Skin:
                textures = skinTextures;
                break;
            case BodyParts.Hair:
                textures = hairTextures;
                break;
            case BodyParts.Mouth:
                textures = mouthTextures;
                break;
            case BodyParts.Eyes:
                textures = eyesTextures;
                break;
            case BodyParts.Clothes:
                textures = clothesTextures;
                break;
            case BodyParts.Armour:
                textures = armourTextures;
                break;
            default:
                return;
        }


        int partsIndex = (int)bodyPart;
        partsIndex++;//add one to skip the default material
        int partsCount = textures.Count;

        //currentTexture

        //allows the choice
        currentTextureIndex += direction;
        if(currentTextureIndex < 0)
        {
            currentTextureIndex = partsCount - 1;//go to the end
        }
        else if(currentTextureIndex > partsCount - 1)
        {
            currentTextureIndex = 0;//go to the start
        }


        //we are currently at this skin texture;
        //skinTextures[currentTextureIndex]

        Material[] mats = characterRenderer.materials;
        mats[partsIndex].mainTexture = textures[currentTextureIndex];
        characterRenderer.materials = mats;
    }


    private void OnGUI()
    {
        CustomiseOnGUI();
    }

    private void CustomiseOnGUI()
    {
        float currentY = 40;
        //int i = 0;

        GUI.Box(new Rect(10, 10, 110, 210), "Visuals");

        foreach(BodyParts part in Enum.GetValues(typeof(BodyParts)))
        {
            if (GUI.Button(new Rect(20, currentY, 20, 20), "<"))
            {
                SetTexture(part, -1);
            }
            GUI.Label(new Rect(45, currentY, 60, 20), part.ToString());
            if (GUI.Button(new Rect(90, currentY, 20, 20), ">"))
            {
                SetTexture(part, 1);
            }
            currentY += 30;
        }
    }
}
