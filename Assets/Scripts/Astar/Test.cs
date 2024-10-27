using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    public Material White;
    public Material Red;
    public Material Yellow;
    public Material Green;
    RaycastHit Hit;
    Vector2 starpos;
    Vector2 endpos;
    List<Nodes> path;
    public Text StartText;
    public Text EndText;
    public Text TempText;
    public GameObject root;
    private GameObject[,] tile;
    private Vector2 temppos;

    public void resetbtn()//重置按钮
    {
        for (int i = 0; i < root.transform.childCount; i++)
        {
            Destroy(root.transform.GetChild(i).gameObject);
        }
        starpos = Vector2.zero;
        endpos = Vector2.zero;
        temppos = Vector2.zero;
        AStarManager.Instance.ResetData();
        DrawMap();
        StartText.text = null;
        EndText.text = null;
        TempText.text = null;
    }
    void Start()
    {
        tile = new GameObject[12,8];
        AStarManager.Instance.initMap(12, 8);
        DrawMap();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out Hit, 100))
            {
                temppos = new Vector2(Hit.collider.gameObject.transform.position.x, Hit.collider.gameObject.transform.position.y);
                TempText.text = temppos.ToString();
                tile[(int)temppos.x, (int)temppos.y].GetComponent<MeshRenderer>().material = Yellow;
            }
        }
    }
    public void AStarFindPath()//寻路按钮
    {
        path = AStarManager.Instance.FindPath(starpos, endpos);
        if (path==null)
            TempText.text = "死路";
        else
        for (int i = 1; i < path.Count; i++)
        {
            tile[path[i].x, path[i].y].GetComponent<MeshRenderer>().material = Green;
        }
        
    }
    public void CalculusCloseList()//回溯关闭列表结点
    {
        StartCoroutine(DrawClose());
    }
    IEnumerator DrawClose()
    {
        for (int i = 0; i < AStarManager.Instance.CloseList.Count; i++)
        {
            tile[AStarManager.Instance.CloseList[i].x, AStarManager.Instance.CloseList[i].y].GetComponent<MeshRenderer>().material = Yellow;
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    public void SetStart()//设置起点
    {
        starpos = temppos;
        StartText.text = starpos.ToString();
    }
    public void SetEnd()//设置终点
    {
        endpos = temppos;
        EndText.text = endpos.ToString();
    }
    private void DrawMap()//绘制面板
    {
        int w = (int)AStarManager.Instance.mapW;
        int h = (int)AStarManager.Instance.mapH;
        Nodes node;
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                node = AStarManager.Instance.nodes[i, j];
                GameObject obj = Resources.Load<GameObject>("Grid");
                GameObject go = Instantiate(obj,root.transform);
                go.transform.position = new Vector3(node.x, node.y, -1);
                if (node.type == Node_Type.walk)
                {
                    go.GetComponent<MeshRenderer>().material = White;
                }
                if (node.type == Node_Type.block)
                {
                    go.GetComponent<MeshRenderer>().material = Red;
                }
                tile[i, j] = go;

            }
        }
    }

}
