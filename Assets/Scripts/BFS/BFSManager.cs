using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFSManager 
{
    private static BFSManager instance;
    public static BFSManager Instance
    {
        get {
            if (instance==null)
            {
                instance = new BFSManager();
            }
            return instance;
        }
    }
    public int MapW;
    public int MapH;
    public BFSNodes[,] nodes;
    Queue<BFSNodes> Node_Queue;
    BFSNodes START_NODE;
    BFSNodes END_NODE;
    public List<BFSNodes> oldpath;

    public void ReSet()
    {
        Node_Queue.Clear();
        oldpath.Clear();
        START_NODE = null;
        END_NODE = null;
        for (int i = 0; i < MapW; i++)
        {
            for (int j = 0; j < MapH; j++)
            {
                nodes[i, j] = new BFSNodes(i, j, Random.Range(0, 100) < 10 ? BFS_NODE_TYPE.block : BFS_NODE_TYPE.walk);
            }
        }

    }
    public void InitMap(int _mapW,int _mapH)
    {
        
        MapW = _mapW;
        MapH = _mapH;
        Node_Queue = new Queue<BFSNodes>();
        nodes = new BFSNodes[_mapW,_mapH];
        oldpath = new List<BFSNodes>();
        for (int i = 0; i < _mapW; i++)
        {
            for (int j = 0; j < _mapH; j++)
            {
                nodes[i, j] = new BFSNodes(i,j,Random.Range(0,100)<10?BFS_NODE_TYPE.block:BFS_NODE_TYPE.walk);
            }
        }
    }
    public List<BFSNodes> FindPath(Vector2 StartPos,Vector2 EndPos)
    {
        if (StartPos.x<0||StartPos.x>MapW||StartPos.y<0||StartPos.y>MapH)
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
        END_NODE= nodes[(int)EndPos.x, (int)EndPos.y];
        if (START_NODE.type==BFS_NODE_TYPE.block||END_NODE.type==BFS_NODE_TYPE.block)
        {
            Debug.Log("起点或者终点是障碍物");
            return null;
        }
        START_NODE.count = 0;
        START_NODE.visted = true;
        START_NODE.father = null;
        Node_Queue.Enqueue(START_NODE);
        while (Node_Queue!=null)
        {
            BFSNodes FirstNode = Node_Queue.Dequeue();
            oldpath.Add(FirstNode);
            if (FirstNode==END_NODE)
            {
                List<BFSNodes> path = new List<BFSNodes>();
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
            FindNearNodes(FirstNode);
            
 
           
        }
        Debug.Log("死路");
        return null;
    }
    public void FindNearNodes(BFSNodes node)
    {

        BFSNodes temp;
        //横向
        for (int i = node.x-1; i <= node.x+1; i++)
        {
            if (i<0||i>=MapW)
            {
                continue;
            }
            temp = nodes[i, node.y];
            if (temp.visted == true)
            {
                continue;
            }
            if (temp.type==BFS_NODE_TYPE.block)
            {
                continue;
            }
            else
            {   
                temp.count = node.count + 1;
                temp.visted = true;
                temp.father = node;
                Node_Queue.Enqueue(temp);
            }
        }
        //纵向
        for (int j = node.y-1; j <= node.y+1; j++)
        {
            if (j<0||j>=MapH)
            {
                continue;
            }
            temp = nodes[node.x, j];
            if (temp.visted == true)
            {
                continue;
            }
            if (temp.type == BFS_NODE_TYPE.block)
            {
                continue;
            }
            else
            {
                temp.count = node.count + 1;
                temp.visted = true;
                temp.father = node;
                Node_Queue.Enqueue(temp);
            }
        }
    }
}

