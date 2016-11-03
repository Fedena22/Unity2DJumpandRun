using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public string seed;
    public bool useRandomSeed;
    public GameObject platfoemTile;

    public int quantity = 10;
    public int minLength = 3;
    public int maxLength = 5;
    public int minHeight = 0;
    public int maxHeight = 5;

    [Range(0,1)]
    public float complexity = 0.5F;

    [Tooltip("The size must be then quantity.")]
    public List<GameObject> addOnObjects;


    private System.Random pseudoRandom;

    private List<Platform> platforms = new List<Platform>();
    // Use this for initialization
    void Start() {
        //Platform p = new Platform(1, 5, 2, true);
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        //Random generator
        InitRandomObject();
        //Die Plattform ertellen (Start of Platform End of Platfoem...
        CreatDefaultPlatforms();
        //Which platform is danger
        SetIsDanger();
        //addons erzeugen
        Debug.Log("Addpns:" + addOnObjects.Count);
        //SetAddOnObjects();
        //Lelvel Erzeugen
        BuildLevel();
    }


    public void BuildLevel()
    {
        //PLatform erstellen
        PLacePlatforms();

        //Spieler placen
        //Ziel

    }

    void PLacePlatforms()
    {
        foreach (Platform current in platforms)
        {
            for (int i = current.startIndex; i < current.startIndex + current.length; i ++)
            {
                Vector3 pos = new Vector3(i, current.heigth, 0);
                if (!(i == current.startIndex + current.length -1 && current.isDanger))
                {
                    GameObject curTile = (GameObject)Instantiate(platfoemTile, pos, Quaternion.identity);
                }
            }
        }
    }

    void InitRandomObject()
    {
        if (useRandomSeed)
            seed = Time.time.ToString();
        pseudoRandom = new System.Random(seed.GetHashCode());

    }

    void CreatDefaultPlatforms()
    {
        int currentStartIndex = 0;
        platforms.Clear();

        int height = GetRandomNumber(minHeight, maxHeight + 1);

        for (int i = 0; i < quantity; i++)
        {
            int heightDirection = GetRandomNumber(0, 3);
            switch(heightDirection)
            {
                case 0:
                    height --;
                    break;
                case 2:
                    height++;
                    break;
            }

            height = Mathf.Max(height, minHeight);
            height = Mathf.Min(height, maxHeight);

            Platform current = new Platform(currentStartIndex, GetRandomNumber(minLength, maxLength + 1), height, false);
            platforms.Add(current);
            currentStartIndex += current.length;

        }
    }

    void SetIsDanger()
    {
        List<int> indices = GetRandomNumbers(0, quantity, Mathf.RoundToInt(complexity * quantity));

        foreach(int current in indices)
        {
            platforms[current].isDanger = true;
            Debug.Log(current);
        }
    }

    void SetAddOnObjects ()
    {
        List<int> indices = GetRandomNumbers(1, quantity, addOnObjects.Count);
        int counter = 0;
        foreach (int current in indices)
        {
            platforms[current].addOn = addOnObjects[counter];
            Debug.Log(current + " " + addOnObjects[counter].name);
            counter ++;
        }

    }

    List<int> GetRandomNumbers(int min, int max, int quatity)
    {
        List<int> result = new List<int>();
        List<int> buffer = new List<int>();

        for (int i = min; i < max; i++) 
        {
            buffer.Add(i);
        }

        for (int i = 0; i < quantity; i++) 
        {
            int temp = GetRandomNumber(0, buffer.Count);
            result.Add(buffer[temp]);
            buffer.RemoveAt(temp);
         }

        return result;

    }

    int GetRandomNumber (int min, int max)
    {
        return pseudoRandom.Next(min, max);

    }

}
