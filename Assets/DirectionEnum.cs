using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets
{
    /*
     * 
     */
    public enum DirectionEnum
    {
        Up = 1,//0001
        Right = 1 << 1,//0010
        Down = 1 << 2,//0100
        Left = 1 << 3,//1000
    }
    public static class DirectionExtension
    {
        const int BitCount = 4;
        const int mask = (1 << BitCount) - 1;
        public static int LeftExtension(int value)
        {
            return ((value >> (BitCount - 1)) & mask) | ((value << 1) & mask);
        }
        public static int RightExtension(int value)
        {
            return ((value << (BitCount - 1)) & mask) | ((value >> 1) & mask);
        }
        public static List<DirectionEnum> ConvertDirection(int value)
        {
            List<DirectionEnum> directions = new List<DirectionEnum>(BitCount);
            if ((value & (int)DirectionEnum.Up) == (int)DirectionEnum.Up)
            {
                directions.Add(DirectionEnum.Up);
            }
            if ((value & (int)DirectionEnum.Right) == (int)DirectionEnum.Right)
            {
                directions.Add(DirectionEnum.Right);
            }
            if ((value & (int)DirectionEnum.Down) == (int)DirectionEnum.Down)
            {
                directions.Add(DirectionEnum.Down);
            }
            if ((value & (int)DirectionEnum.Left) == (int)DirectionEnum.Left)
            {
                directions.Add(DirectionEnum.Left);
            }
            return directions;
        }
    }
}
