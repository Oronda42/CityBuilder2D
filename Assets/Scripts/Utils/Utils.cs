using System.Collections;
using UnityEngine;
using TMPro;

public static class Utils
{


    public static TextMeshPro CreateWorldText(string name,string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontsize = 40, Color color = default, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignmentOptions textAlignment = TextAlignmentOptions.Center, int sortingOrder = 5000)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(name,parent, text, localPosition, fontsize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
    public static TextMeshPro CreateWorldText(string name,Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignmentOptions textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject(name, typeof(TextMeshPro));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        //Quaternion rotation = Quaternion.Euler(90f, 0, 0);
       // transform.rotation = rotation;
        TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
        //textMesh. anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
  
    public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera)
    {
        return worldCamera.ScreenToWorldPoint(screenPosition);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        return GetMouseWorldPosition(Input.mousePosition,Camera.main);
    }

    public static void GetTileCoordinatesWithRotation(int sqaureGridSize,int X, int Y, out int futurX, out int futurY, Testing.MapOrientation orientation)
    {
        if (orientation == Testing.MapOrientation.NORTH)
        {
            futurX = X;
            futurY = Y;
            return;
        }

        if (orientation == Testing.MapOrientation.EAST)
        {
            futurX = Y;
            futurY = sqaureGridSize - X - 1;
            return;
        }

        if (orientation == Testing.MapOrientation.SOUTH)
        {
            futurX = sqaureGridSize - X - 1;
            futurY = sqaureGridSize - Y - 1;
            return;
        }

        if (orientation == Testing.MapOrientation.WEST)
        {
            futurX = sqaureGridSize - Y - 1;
            futurY = X;
            return;
        }

        futurX = -1;
        futurY = -1;
    }

   

}
