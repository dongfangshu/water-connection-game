using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxX;
    public int maxY;
    public Transform mapPoint;
    public Button FindPath;
    private int[,] pos = new int[,] {
            {1, 1, 0},
            {0, 1, 0},
            {0, 1, 1}
    };
    private Dictionary<int, Node> nodeMap = new Dictionary<int, Node>();
    void Start()
    {
        PrintMap();
        DrawMap();
        FindPath.onClick.AddListener(OnFindPathClick);
    }

    private void OnFindPathClick()
    {
        
    }

    private void PrintMap()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < pos.GetLength(0); i++)
        {
            for (int j = 0; j < pos.GetLength(1); j++)
            {
                sb.Append(pos[i, j]);
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }
    private void DrawMap()
    {
        GameObject origin = Resources.Load<GameObject>("model/node");
        for (int i = 0; i < pos.GetLength(0); i++)
        {
            for (int j = 0; j < pos.GetLength(1); j++)
            {
                /*
                 * [0,0] [0,1] [0,2]
                 * [1,0] [1,1] [1,2]
                 * [2,0] [2,1] [2,2]
                 * int[3,3]pos = int[3,3]
                 */
                if (pos[i,j] > 0)
                {
                    List<DirectionEnum> dir = new List<DirectionEnum>();
                    if (j>0 && pos[i, j - 1] > 0)
                    {
                        //◊Û±ﬂ
                        dir.Add(DirectionEnum.Left);
                    }
                    if (i > 0 && pos[i - 1, j] > 0)
                    {
                        //…œ±ﬂ
                        dir.Add(DirectionEnum.Up);
                    }
                    if (j < pos.GetLength(1) - 1 && pos[i , j + 1] > 0)
                    {
                        //”“±ﬂ
                        dir.Add(DirectionEnum.Right);
                    }
                    if ( i < pos.GetLength(0) - 1 && pos[i + 1,j] > 0 )
                    {
                        //œ¬±ﬂ
                        dir.Add(DirectionEnum.Down);
                    }
                    var obj = GameObject.Instantiate(origin, mapPoint);
                    var node = obj.GetComponent<Node>();
                    node.Init(dir);
                    //i -> -2
                    //j -> -2
                    Vector3 worldPos = new Vector3(i*-2,0,j*-2);
                    obj.transform.localPosition = worldPos;
                    int instanceId = i * pos.GetLength(0) + j;
                    obj.name = $"{instanceId}:{i} - {j}";
                    Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                    var renders = obj.GetComponentsInChildren<Renderer>(true);
                    foreach (var render in renders)
                    {
                        render.material.color = randomColor;
                    }
                   
                    nodeMap.Add(instanceId, node);
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
