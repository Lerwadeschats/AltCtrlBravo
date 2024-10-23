using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;

public static class GridDetection
{
    
    

    

    //Set a grid on the texture
    public static GridSquare[,] SetTextureGrid(Texture2D texture, int _unitSize)
    {
        GridSquare[,] textureGrid = new GridSquare[texture.width/_unitSize, texture.height/ _unitSize];
        for (int x = 0; x < texture.width/ _unitSize; x ++)
        {
            for (int y = 0; y < texture.height/_unitSize; y ++)
            {
                textureGrid[x, y] = new GridSquare(x, y, _unitSize);

            }
        }
        return textureGrid;
    }



    //Get black cases on texture
    public static List<GridSquare> GetAllBlackCases(Texture2D runeTexture, int _unitSize)
    {
        GridSquare[,] textureGrid = SetTextureGrid(runeTexture, _unitSize);
        List<GridSquare> blackCases = new List<GridSquare>();
        Color[] pixels = runeTexture.GetPixels();

        for (int x = 0; x < textureGrid.GetLength(0); x++)
        {
            for (int y = 0; y < textureGrid.GetLength(1); y++)
            {
                GridSquare square = textureGrid[x, y];

                if (runeTexture.GetPixel(square._posX * _unitSize,square._posY * _unitSize) == Color.black)
                {

                    blackCases.Add(square);
                }
            }
        }
        return blackCases;
    }

    public static bool IsDrawingInBlackCases(List<Vector2> points, Texture2D texture, float newSquareSize, Vector2 originPoint, float margin = 0.1f)
    {
        
        List<GridSquare> blackCases = GetAllBlackCases(texture, 50);
        foreach (Vector2 point in points)
        {
            for (int i = 0; i < blackCases.Count; i++)
            {
                GridSquare square = blackCases[i];
                if (square.DoesContainPoint(point, margin, newSquareSize, originPoint))
                {
                    if (!blackCases[i].isOccupied)
                    {
                        //Debug.Log($"Square occupied {square._posX} {square._posY}");
                        square.isOccupied = true;
                    }
                    break;
                }
                else
                {
                    //Debug.Log(square._posX + " , " + square._posY + " est hors du chemin");
                    if (i == blackCases.Count - 1)
                    {

                        return false;
                    }
                }


            }
            
        }
        foreach (GridSquare square in blackCases)
        {
            if (!square.isOccupied)
            {
                //Debug.Log("Dessin pas fini");
                return false;
            }
        }

        return true;
    }

    
}

public class GridSquare
{
    //PosX & posY sont égaux à 1, 2, 3... pas à 50, 100, 150
    public int _posX;
    public int _posY;
    int _size;
    public bool isOccupied;
    public GridSquare(float x, float y, int size)
    {
        _posX = (int)x;
        _posY = (int)y;
        _size = size;
    }

    public bool DoesContainPoint(Vector2 point, float margin, float newSquareSize, Vector2 originPoint)
    {
        float minX = (_posX /*- margin*/) * newSquareSize + originPoint.x;
        float maxX = (_posX + 1/* + margin*/) * newSquareSize + originPoint.x;
        
        float minY = (_posY/* - margin*/) * newSquareSize + originPoint.y;
        float maxY = ((_posY + 1 /*+ margin*/) * newSquareSize +  originPoint.y);

        

        if (point.x > minX && 
            point.x < maxX && 
            point.y > minY && 
            point.y < maxY)
        {
            return true;
        }
        //Debug.Log("Cube " + new Vector2(_posX, _posY) + " : \n " + minX + " < " + point.x + " < " + maxX + " \n -------------------------------------------- \n " + minY + " < " + point.y + " < " + maxY);
        return false;
    }

    
}


