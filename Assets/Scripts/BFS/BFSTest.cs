using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BFSTest : MonoBehaviour
{
    public GameObject BFS_ROOT;
    public Material Red;
    public Material White;
    public Material Green;
    public Material Yellow;
    private GameObject[,] tile;

    private List<BFSNodes> path;
    
    RaycastHit hit;


    Vector2 startpos;
    Vector2 endpos;
    Vector2 temppos;
    public Text StartText;
    public Text EndText;
    public Text TempText;
    public void ReSetBtn()
    {
        for (int i = 0; i < BFS_ROOT.transform.childCount; i++)
        {
            Destroy(BFS_ROOT.transform.GetChild(i).gameObject);
        }
        startpos = Vector2.zero;
        endpos = Vector2.zero;
        temppos = Vector2.zero;
        BFSManager.Instance.ReSet();
        DrawMap();
        StartText.text = null;
        EndText.text = null;
        TempText.text = null;
    }
    void Start()
    {
        tile = new GameObject[12,8];
        BFSManager.Instance.InitMap(12,8);
        path = new List<BFSNodes>();
        DrawMap();
    }
    public void BFSFindPath()
    {
        
        path = BFSManager.Instance.FindPath(startpos, endpos);
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
    public  void huisubtn()
    {

        StartCoroutine(guiji());
        
    }
    IEnumerator guiji()
    {
        for (int i = 0; i < BFSManager.Instance.oldpath.Count; i++)
        {
            tile[BFSManager.Instance.oldpath[i].x,BFSManager.Instance.oldpath[i].y].GetComponent<MeshRenderer>().material = Yellow;
            yield return new WaitForSeconds(0.2f);
        }

    }
    private void DrawMap()//绘制面板
    {
        int w = (int)BFSManager.Instance.MapW;
        int h = (int)BFSManager.Instance.MapH;
        BFSNodes node;
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                node = BFSManager.Instance.nodes[i, j];
                GameObject obj = Resources.Load<GameObject>("Grid");
                GameObject go = Instantiate(obj, BFS_ROOT.transform);
                go.transform.position = new Vector3(node.x, node.y, 0);
                if (node.type == BFS_NODE_TYPE.walk)
                {
                    go.GetComponent<MeshRenderer>().material = White;
                }
                if (node.type == BFS_NODE_TYPE.block)
                {
                    go.GetComponent<MeshRenderer>().material = Red;
                }
                tile[i, j] = go;

            }
        }
    }

}
