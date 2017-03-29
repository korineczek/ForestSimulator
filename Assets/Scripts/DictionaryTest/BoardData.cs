﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestSimulator
{
    public enum WeatherState
    {
        Sunny, Overcast, Raining, HeavyWind, SunClouds, Earthquake
    }   

    internal static class BoardData
    {
        public static readonly int BOARDSIZE = 15;
        public static Tile[,] Map;

        public static void GenerateBoardData(float perlinScale)
        {
            Map = new Tile[BOARDSIZE, BOARDSIZE];
            //generate board data
            for (int i = 0; i < BOARDSIZE; i++)
            {
                for (int j = 0; j < BOARDSIZE; j++)
                {
                    //get random height for tiles based on noise
                    float randomHeight = (Mathf.PerlinNoise(i / 5f, j / 5f) * perlinScale + (UnityEngine.Random.Range(-0.1f, 0.1f) / 2)) * 0.25f;
                    Map[i,j] = new Tile(i,j,randomHeight);
                }
            }
            //generate slope data
            for (int i = 1; i < BOARDSIZE - 1; i++)
            {
                for (int j = 1; j < BOARDSIZE - 1; j++)
                {
                    Map[i, j].Slope = GetMaxSlope(i, j);
                    //Debug.Log(TileArray[i, j].Slope);
                    //TODO: NORMALIZE SLOPES SO THAT THE COLOR DOENST OVERFLOW
                    Map[i, j].BaseResource = (int)(Map[i, j].BaseResource * (1 - (Map[i, j].Slope * 2))) + (int)(10 - Map[i, j].WorldCoordinates.y);
                    Map[i, j].Resource = Map[i, j].BaseResource;
                }
            }
        }

        /// <summary>
        /// get max slope for each tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static float GetMaxSlope(int x, int y)
        {
            //variant 2 average slope
            return (HexCoords.HexSlope(Map[x + 0, y - 1], Map[x, y]) + HexCoords.HexSlope(Map[x + 1, y - 1], Map[x, y]) +HexCoords.HexSlope(Map[x + 1, y + 0], Map[x, y]) + HexCoords.HexSlope(Map[x, y], Map[x + 1, y + 1]) +HexCoords.HexSlope(Map[x, y], Map[x + 0, y + 1]) + HexCoords.HexSlope(Map[x, y], Map[x - 1, y + 0])) / 6;
        }
    }
}