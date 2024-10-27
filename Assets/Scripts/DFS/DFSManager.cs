using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DFSManager 
{
    private static DFSManager instance;
    public static DFSManager Instance
    {
        get {
            if (instance==null)
            {
                instance = new DFSManager();
            }
            return instance;
        }
    }
    public int MapW;
    public int MapH;
    public DFSNode[,] nodes;
    DFSNode START_NODE;
    DFSNode END_NODE;
    Stack<DFSNode> DFS_STACK;
    Vector2Int[] dir=new Vector2Int[4] {Vector2Int.up,Vector2Int.right,Vector2Int.down,Vector2Int.left };
    public List<DFSNode> oldpath;
    public void InitMap(int _W,int _H)
    {
        MapW = _W;
        MapH = _H;
        nodes = new DFSNode[_W,_H];
        DFS_STACK = new Stack<DFSNode>();
        oldpath = new List<DFSNode>();
        for (int i = 0; i < MapW; i++)
        {
            for (int j = 0; j < MapH; j++)
            {
                nodes[i, j] = new DFSNode(i, j, Random.Range(0, 100) < 10 ? DFS_NODE_TYPE.block : DFS_NODE_TYPE.walk);
            }
        }
    }
    public void ReSet()
    {
        DFS_STACK.Clear();
        START_NODE = null;
        END_NODE = null;
        oldpath.Clear();
        for (int i = 0; i < MapW; i++)
        {
            for (int j = 0; j < MapH; j++)
            {
                nodes[i, j] = new DFSNode(i, j, Random.Range(0, 100) < 10 ? DFS_NODE_TYPE.block : DFS_NODE_TYPE.walk);
            }
        }
    }
    public List<DFSNode> DFSFindPath(Vector2 StartPos,Vector2 EndPos)
    {
        if (StartPos.x < 0 || StartPos.x > MapW || StartPos.y < 0 || StartPos.y > MapH)
        {
            Debug.Log("起始点超出边界");
            return null;
        }
        if (EndPos.x < 0 || EndPos.x > MapW || EndPos.y < 0 || EndPos.y > MapH)
        {
            Debug.Log("终点超出边界");
            return null;
        }
        START_NODE = nodes[(int)StartPos.x, (int)StartPos.y];
        END_NODE = nodes[(int)EndPos.x, (int)EndPos.y];
        if (START_NODE.type == DFS_NODE_TYPE.block || END_NODE.type == DFS_NODE_TYPE.block)
        {
            Debug.Log("起点或者终点是障碍物");
            return null;
        }
        START_NODE.count = 0;
        START_NODE.visted = true;
        START_NODE.father = null;
        DFS_STACK.Push(START_NODE);
        while (DFS_STACK!=null)
        {
            DFSNode node = DFS_STACK.Pop();
            oldpath.Add(node);
            if (node==END_NODE)
            {
                List<DFSNode> path = new List<DFSNode>();
                path.Add(END_NODE);
                while (END_NODE != null)
                {
                    path.Add(END_NODE.father);
                    END_NODE = END_NODE.father;
                }
                path.Reverse();
                Debug.Log("找到了全部路径");
                return path;
            }
            FindNearNode(node);
        }
        
        return null;
    }
    private void FindNearNode(DFSNode father)
    {
        
        DFSNode temp;
        //笨比穷举方法
        #region
        //if (father.y + 1<MapH)
        //{
        //    temp = nodes[father.x, father.y + 1];
        //    if (temp.type != DFS_NODE_TYPE.block&&!temp.visted)
        //    {
        //        temp.count = father.count + 1;
        //        temp.visted = true;
        //        temp.father = father;
        //        DFS_STACK.Push(temp);
        //    }

        //}
        //if (father.x + 1<MapW)
        //{
        //    temp = nodes[father.x + 1, father.y];
        //    if (temp.type != DFS_NODE_TYPE.block && !temp.visted)
        //    {
        //        temp.count = father.count + 1;
        //        temp.visted = true;
        //        temp.father = father;
        //        DFS_STACK.Push(temp);
        //    }
        //}
        //if (father.y - 1>0)
        //{
        //    temp = nodes[father.x, father.y - 1];
        //    if (temp.type != DFS_NODE_TYPE.block && !temp.visted)
        //    {
        //        temp.count = father.count + 1;
        //        temp.visted = true;
        //        temp.father = father;
        //        DFS_STACK.Push(temp);
        //    }
        //}
        //if (father.x - 1>0)
        //{
        //    temp = nodes[father.x - 1, father.y];
        //    if (temp.type != DFS_NODE_TYPE.block && !temp.visted)
        //    {
        //        temp.count = father.count + 1;
        //        temp.visted = true;
        //        temp.father = father;
        //        DFS_STACK.Push(temp);
        //    }
        //}
        #endregion
        //做一个数组存变量
        for (int i = 0; i < dir.Length; i++)
        {
            if (father.x+dir[i].x<0|| father.x + dir[i].x >=MapW)
            {
                continue;
            }
            if (father.y + dir[i].y < 0 || father.y + dir[i].y >= MapH)
            {
                continue;
            }
            temp = nodes[father.x+dir[i].x,father.y+dir[i].y];
            if (temp.type==DFS_NODE_TYPE.block||temp.visted)
            {
                continue;
            }
            temp.count = father.count + 1;
            temp.visted = true;
            temp.father = father;
            Debug.Log(temp.x+" "+temp.y );
            DFS_STACK.Push(temp);
        }
    }


}
