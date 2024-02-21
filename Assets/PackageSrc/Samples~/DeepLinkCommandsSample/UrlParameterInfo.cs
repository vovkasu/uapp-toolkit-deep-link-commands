using UnityEngine;
using UnityEngine.UI;

namespace UAppToolkit.DeepLinkCommands.Sample
{
    public class UrlParameterInfo : MonoBehaviour
    {
        public Text Key;
        public Text Value;

        public void Init(string key, object value)
        {
            Key.text = key;
            Value.text = value.ToString();
        }
    }
}