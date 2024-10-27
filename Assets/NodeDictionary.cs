using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomizationInspector.Runtime;

namespace Assets
{
    [Serializable]
    public class NodeDictionary: SerializableDictionary<DirectionEnum, NodeModel>
    {
    }
}
