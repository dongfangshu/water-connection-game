public enum Node_Type
{
    walk,
    block,
}
public class Nodes 
{
    public float f;//寻路消耗
    public float g;//与起点的距离
    public float h;//与终点的距离

    public Nodes father;//父节点

    public int x;//位置信息
    public int y;

    public Node_Type type;//标记
    public Nodes(int _x, int _y,Node_Type _type)
    {
        x = _x;
        y = _y;
        type = _type;
    }

}
