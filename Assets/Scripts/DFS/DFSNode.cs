public class DFSNode 
{
    public int x;
    public int y;
    public bool visted;
    public int count;
    public DFSNode father;
    public DFS_NODE_TYPE type;

    public DFSNode(int _X,int _Y,DFS_NODE_TYPE _Type)
    {
        x = _X;
        y = _Y;
        type = _Type;
    }
}
public enum DFS_NODE_TYPE
{
    block,
    walk,
}
