using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DFSTest : MonoBehaviour
{
    public GameObject DFS_ROOT;
    public Material Red;
    public Material White;
    public Material Green;
    public Material Yellow;
    private GameObject[,] tile;

    private List<DFSNode> path;

    RaycastHit hit;


    Vector2 startpos;
    Vector2 endpos;
    Vector2 temppos;
    public Text StartText;
    public Text EndText;
    public Text TempText;
    public void ResetBtn()
    {
        for (int i = 0; i < DFS_ROOT.transform.childCount; i++)
        {
            Destroy(DFS_ROOT.transform.GetChild(i).gameObject);
        }
        startpos = Vector2.zero;
        endpos = Vector2.zero;
        temppos = Vector2.zero;
        DFSManager.Instance.ReSet();
        DrawMap();
        StartText.text = null;
        EndText.text = null;
        TempText.text = null;
    }
    private void Start()
    {
        tile = new GameObject[12, 8];
        DFSManager.Instance.InitMap(12,8);
        path = new List<DFSNode>();
        DrawMap();
    }
    public void huisubtn()
    {

        StartCoroutine(guiji());

    }
    IEnumerator guiji()
    {
        for (int i = 0; i < DFSManager.Instance.oldpath.Count; i++)
        {
            tile[DFSManager.Instance.oldpath[i].x, DFSManager.Instance.oldpath[i].y].GetComponent<MeshRenderer>().material = Yellow;
            yield return new WaitForSeconds(0.2f);
        }

    }
    public void BFSFindPath()
    {

        path = DFSManager.Instance.DFSFindPath(startpos, endpos);
        if (path == null)
        {
            TempText.text = "死路";
        }
        else
        {
            for (int i = 1; i < path.Count; i++)
            {
                tile[path[i].x, path[i].y].GetComponent<MeshRenderer>().material = Green;

            }
            TempText.text = path[path.Count - 1].count.ToString();
        }


    }
    public void SetStart()
    {
        startpos = temppos;
        StartText.text = startpos.ToString();
    }
    public void SetEnd()
    {
        endpos = temppos;
        EndText.text = endpos.ToString();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                temppos = new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y);
                TempText.text = temppos.ToString();
                tile[(int)temppos.x, (int)temppos.y].GetComponent<MeshRenderer>().material = Yellow;
            }
        }

    }
    private void DrawMap()//绘制面板
    {
        int w = (int)DFSManager.Instance.MapW;
        int h = (int)DFSManager.Instance.MapH;
        DFSNode node;
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                node = DFSManager.Instance.nodes[i, j];
                GameObject obj = Resources.Load<GameObject>("Grid");
                GameObject go = Instantiate(obj, DFS_ROOT.transform);
                go.transform.position = new Vector3(node.x, node.y, 0);
                if (node.type == DFS_NODE_TYPE.walk)
                {
                    go.GetComponent<MeshRenderer>().material = White;
                }
                if (node.type == DFS_NODE_TYPE.block)
                {
                    go.GetComponent<MeshRenderer>().material = Red;
                }
                tile[i, j] = go;

            }
        }
    }
}
