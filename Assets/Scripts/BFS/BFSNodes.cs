public class BFSNodes
{
    public int x;
    public int y;
    public int count;//步数
    public BFS_NODE_TYPE type;//结点类型
    public bool visted;//是否遍历过
    public BFSNodes father;
    public BFSNodes(int _x,int _y,BFS_NODE_TYPE _type)
    {
        this.x = _x;
        this.y = _y;
        this.type = _type;
    }
}
public enum BFS_NODE_TYPE
{
    block,
    walk,
}
