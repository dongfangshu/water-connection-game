using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager 
{
    private static AStarManager instance;
    public static AStarManager Instance
    {
        get {
            if (instance==null)
            {
                instance = new AStarManager();
            }
            return instance;
        }
    }

    public float mapH;//地图的高
    public float mapW;//地图的宽
    public Nodes[,] nodes;//地图数据
    public List<Nodes> OpenList;//开启列表
    public List<Nodes> CloseList;//关闭列表
    Nodes End_NODE;//开启结点
    Nodes START_NODE;//关闭结点
    public void ResetData()//重置地图数据
    {
        OpenList.Clear();
        CloseList.Clear();
        START_NODE = null;
        End_NODE = null;
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                nodes[i, j] = new Nodes(i, j, Random.Range(0, 100) < 30 ? Node_Type.block : Node_Type.walk);//30%几率是障碍
            }
        }
    }
    /// <summary>
    /// 初始化地图数据
    /// </summary>
    /// <param name="_W">宽</param>
    /// <param name="_H">高</param>
    public void initMap(int _W, int _H)
    {
        mapW = _W;
        mapH = _H;
       
        START_NODE = null;
        End_NODE = null;
        OpenList = new List<Nodes>();
        CloseList = new List<Nodes>();
        nodes = new Nodes[_W,_H];

        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                nodes[i, j] = new Nodes(i, j, Random.Range(0, 100) < 30 ? Node_Type.block : Node_Type.walk);//10%几率是障碍
            }
        }
    }
    /// <summary>
    /// 寻找路径
    /// </summary>
    /// <param name="StartPos"></param>
    /// <param name="EndPos"></param>
    /// <returns></returns>
    public List<Nodes> FindPath(Vector2 StartPos,Vector2 EndPos)
    {
        
        //判断传入的点是否正确
        if (StartPos.x<0||StartPos.x>=mapW||StartPos.y<0||StartPos.y>=mapH)
        {
            Debug.Log("起始点无效");
            return null;
        }
        if (EndPos.x < 0 || EndPos.x >= mapW || EndPos.y < 0 || EndPos.y >= mapH)
        {
            Debug.Log("目标点无效");
            return null;
        }
        START_NODE = nodes[(int)StartPos.x,(int)StartPos.y];
        End_NODE = nodes[(int)EndPos.x, (int)EndPos.y];
        if (START_NODE.type==Node_Type.block|| End_NODE.type == Node_Type.block)
        {
            Debug.Log("起始点或者目标点是障碍");
            return null;
        }
        START_NODE.father = null;
        START_NODE.f = 0;
        START_NODE.g = 0;
        START_NODE.h = 0;
        CloseList.Add(START_NODE);


        while (true)
        {
            //寻找周围九宫格合适的点
            FindNearNodes(START_NODE);
            
            if (OpenList.Count == 0)
            {
               return null;
            }
           //对比开启列表中的节点，查找寻路消耗最少的那个
            Nodes MinF = OpenList[0];

            for (int i = 0; i < OpenList.Count; i++)
            {
                if (OpenList[i].f < MinF.f)
                {
                    MinF = OpenList[i];
                }
            }
            CloseList.Add(MinF);
            OpenList.Remove(MinF);
            
            if (MinF == End_NODE)
            {
                //找到了路径
                List<Nodes> path = new List<Nodes>() { End_NODE};
                while (End_NODE != null)
                {
                    path.Add(End_NODE.father);
                    End_NODE = End_NODE.father;
                }
                path.Reverse();
                Debug.Log("找到了全部路径");
                return path;
            }
            else START_NODE = MinF;

        }



    }
    /// <summary>
    /// 寻找周围点
    /// </summary>
    /// <param name="father"></param>
    private void FindNearNodes(Nodes father)//查找周围的点是否符合标准
    {
        Nodes temp;//临时NODES变量
        for (int i = father.x - 1; i <= father.x + 1; i++)
        {
            for (int j = father.y - 1; j <= father.y + 1; j++)
            {

                if (i < 0 || i >= mapW || j < 0 || j >= mapH)
                {
                    continue;//超出边界，退出
                }
                temp = nodes[i, j];
                if (OpenList.Contains(temp))
                {
                    continue;//在开启列表，退出
                }
                if (CloseList.Contains(temp))
                {
                    continue;//在关闭列表，退出
                }
                if (temp.type == Node_Type.block )
                {
                    continue;//标记为障碍，退出
                }
                temp.g = father.g+Mathf.Sqrt((temp.x - father.x) * (temp.x - father.x)
                                            + (temp.y - father.y) * (temp.y - father.y));
                temp.h = Mathf.Abs(temp.x - End_NODE.x) + Mathf.Abs(temp.y - End_NODE.y);
                temp.father = father;
                temp.f = temp.g + temp.h;

                OpenList.Add(temp);//符合标准的结点添加到开启列表

            }
        }
    }
}
