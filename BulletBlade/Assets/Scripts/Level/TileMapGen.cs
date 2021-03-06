﻿using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TileMapGen : MonoBehaviour
{

    public TextAsset textFile;

    [HideInInspector]
    public int [][] map;

    [HideInInspector]
    public int columns;
    [HideInInspector]
    public int rows;

    public GameObject floorTile;   // 0
    public GameObject wallBase;    // 1
    public GameObject wallMid;     // 2
    public GameObject wallTop;     // 3

    public Vector3 boardTranslate;

    void Awake()
    {
        map = parseTextMap(textFile.text);

        boardSetup();

    }

    int [] [] parseTextMap(String textMap)
    {

        string[] lines = textMap.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        string[][] stringMap = new string[lines.Length][];

       

        for (int y=0; y< lines.Length; y++)
        {
            stringMap[y] = lines[y].Split(null);
        }


        rows = stringMap.Length;
        columns = stringMap[0].Length;


        int[][] numMap = new int[rows][];

        for (int y=0; y<numMap.Length; y++)
        {
            numMap[y] = new int[columns];
        }
        


        for (int y=0; y < stringMap.Length; y++)
        {
            for (int x=0; x<stringMap[0].Length; x++)
            {
                numMap[y][x] = int.Parse(stringMap[y][x]);
            }
        }

        return numMap;
    }

    void boardSetup()
    {


        gameObject.transform.position = (boardTranslate);

        GameObject toInstantiate = null;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {


                if (map[y][x] == 0)
                {
                    toInstantiate = floorTile;
                }

                else if (map[y][x] == 1)
                {
                    toInstantiate = wallBase;
                }

                else if (map[y][x] == 2)
                {
                    toInstantiate = wallMid;
                }

                else if (map[y][x] == 3)
                {
                    toInstantiate = wallTop;
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, -y + ((toInstantiate == wallTop) ? 0.25f : 0), 0.0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(gameObject.transform);

            }

        }
    }

    public Vector3 getBoardTranslate()
    {
        return boardTranslate;
    }

    void Update()
    {
        gameObject.transform.position = boardTranslate;
    }





}
