using System.Collections.Generic;
using UnityEngine;

namespace UAppToolkit.DeepLinkCommands
{
    public abstract class DeepLinkCommandBase : ScriptableObject
    {
        public string Name;
        public abstract bool TryEval(string absoluteURL = "", Dictionary<string, object> parameters = null);
    }
}