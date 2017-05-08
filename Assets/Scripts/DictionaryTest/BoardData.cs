using System.Collections;
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
        public static readonly int[] BOARDSIZE = new int[]{ 4, 5, 5, 6, 7, 8};
        public static int CURRENTBOARD = 0;
        public static Tile[,] Map;
        public static Gradient TileGradient = new Gradient();
        public static int MaxResource = 0;

        public static void SetGradient()
        {
            Debug.Log("GRADIENT GENERATED");
            GradientColorKey[] gck = new GradientColorKey[3];
            gck[0].time = 0f;
            gck[0].color = Color.red;
            gck[1].time = 0.5f;
            gck[1].color = Color.yellow;
            gck[2].time = 1f;
            gck[2].color = Color.green;
            GradientAlphaKey[] gak = new GradientAlphaKey[3];
            gak[0].time = 0f;
            gak[0].alpha = 1f;
            gak[1].time = 0.5f;
            gak[1].alpha = 1f;
            gak[2].time = 1f;
            gak[2].alpha = 1f;
            TileGradient.SetKeys(gck,gak);
        }

        public static void GenerateBoardData(float perlinScale, bool manualOverride)
        {
            //set gradient for use in tiles by the board;
            SetGradient();
            Map = new Tile[BOARDSIZE[CURRENTBOARD], BOARDSIZE[CURRENTBOARD]];
            //generate board data
            //if overrride off
            if (!manualOverride)
            {
                for (int i = 0; i < BOARDSIZE[CURRENTBOARD]; i++)
                {
                    for (int j = 0; j < BOARDSIZE[CURRENTBOARD]; j++)
                    {
                        //get random height for tiles based on noise
                        float randomHeight = (Mathf.PerlinNoise(i/7f, j/4f)*perlinScale +
                                              (UnityEngine.Random.Range(-0.1f, 0.1f)/2))*0.7f;
                        Map[i, j] = new Tile(i, j, randomHeight);
                    }
                }
            }
            else
            {
                switch (CURRENTBOARD)
                {
                    case 3:
                        Debug.Log("GENERATING CUSTOM TERRAIN");
                        //load specific array
                        for (int i = 0; i < BOARDSIZE[CURRENTBOARD]; i++)
                        {
                            for (int j = 0; j < BOARDSIZE[CURRENTBOARD]; j++)
                            {
                                //get random height for tiles based on noise
                                float randomHeight = (Mathf.PerlinNoise(i/7f, j/4f)*perlinScale +
                                              (UnityEngine.Random.Range(-0.1f, 0.1f)/2))*0.7f;
                                Map[i, j] = new Tile(i, j, randomHeight);
                                if (i < BOARDSIZE[CURRENTBOARD]/2)
                                {
                                    Map[i, j].BaseResource = 0;
                                    Map[i, j].Resource = 0;
                                }

                            }
                        }
                        break;

                    default:
                        //load specific array
                        for (int i = 0; i < BOARDSIZE[CURRENTBOARD]; i++)
                        {
                            for (int j = 0; j < BOARDSIZE[CURRENTBOARD]; j++)
                            {
                                //get random height for tiles based on noise
                                float randomHeight = 0; //TODO: REMOVE DUMMY VARIABLE WITH SPECIFIC HEIGHT
                                Map[i, j] = new Tile(i, j, randomHeight);

                            }
                        }
                        break;
                }
                
            }
            //generate slope data
            //Only generate when manual override is off
            if (!manualOverride)
            {
                for (int i = 0; i < BOARDSIZE[CURRENTBOARD] ; i++)
                {
                    for (int j = 0; j < BOARDSIZE[CURRENTBOARD] ; j++)
                    {
                        Map[i, j].Slope = GetMaxSlope(i, j);
                        //Debug.Log(TileArray[i, j].Slope);
                        //TODO: NORMALIZE SLOPES SO THAT THE COLOR DOENST OVERFLOW
                        Map[i, j].BaseResource = (int) (Map[i, j].BaseResource*(1 - (Map[i, j].Slope*2))) +
                                                 (int) ((10 - Map[i, j].WorldCoordinates.y)/5);
                        Map[i, j].Resource = Map[i, j].BaseResource;
                    }
                }
            }

            //figure out max resource for map
            for (int i = 0; i < BOARDSIZE[CURRENTBOARD]; i++)
            {
                for (int j = 0; j < BOARDSIZE[CURRENTBOARD]; j++)
                {
                    if (Map[i, j].BaseResource > MaxResource)
                    {
                        MaxResource = Map[i, j].BaseResource;
                    }
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
            float slopeSum = 0;
            int slopeCount = 0;
            if (y - 1 >= 0)
            {
                slopeSum += HexCoords.HexSlope(Map[x + 0, y - 1], Map[x, y]);
                slopeCount++;
            }
            if (x + 1 < BOARDSIZE[CURRENTBOARD] && y - 1 >= 0)
            {
                slopeSum += HexCoords.HexSlope(Map[x + 1, y - 1], Map[x, y]);
                slopeCount++;
            }
            if (x + 1 < BOARDSIZE[CURRENTBOARD])
            {
                slopeSum += HexCoords.HexSlope(Map[x + 1, y + 0], Map[x, y]);
                slopeCount++;
            }
            if (x + 1 < BOARDSIZE[CURRENTBOARD] && y + 1 < BOARDSIZE[CURRENTBOARD])
            {
                slopeSum += HexCoords.HexSlope(Map[x, y], Map[x + 1, y + 1]);
                slopeCount++;
            }
            if (y + 1 < BOARDSIZE[CURRENTBOARD])
            {
                slopeSum += HexCoords.HexSlope(Map[x, y], Map[x + 0, y + 1]);
                slopeCount++;
            }
            if (x - 1 >= 0)
            {
                slopeSum += HexCoords.HexSlope(Map[x, y], Map[x - 1, y + 0]);
                slopeCount++;
            }

            return slopeSum/slopeCount;
        }
    }
}
