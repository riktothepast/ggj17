using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public float scale;
    public string assetsFile;
    public List<Vector3> dollies;
    public Vector2 mapSize;
    public GameObject vampireHearth;
    public List<Transform> elements = new List<Transform>();

    void Start()
    {
        string[][] levelMatrix = readFile(assetsFile);
        for (int y = 0; y < levelMatrix.Length; y++)
        {
            for (int x = 0; x < levelMatrix[y].Length; x++)
            {
                Vector3 position = transform.position + new Vector3(x, -y, 0) * scale;
                if (levelMatrix[y][x] == "d")  // dolly position
                {
                    dollies.Add(position);
                } else if (levelMatrix[y][x] == "e")
                {
                    mapSize = position;
                } else if (levelMatrix[y][x] == "h")
                {
                    GameObject go = Instantiate(vampireHearth, position, Quaternion.identity);
                    go.transform.parent = this.GetComponent<Transform>();
                } else
                {
                    int index;
                    if (int.TryParse(levelMatrix[y][x], out index))
                    {
                        Transform go = Instantiate(elements[index], position, Quaternion.identity);
                        go.parent = this.GetComponent<Transform>();
                    }
                }
            }
        }
        Camera.main.GetComponent<CameraMovement>().SetMapSize(mapSize);
    }

    private string[][] readFile(string file)
    {
        string text = System.IO.File.ReadAllText(file);
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;
        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] intLine = Regex.Split(lines[i], "");
            levelBase[i] = intLine;
        }
        return levelBase;
    }
}
