using System.Collections.Generic;
using UnityEngine;

namespace UAppToolkit.DeepLinkCommands
{
    public class DeepLinksManager : MonoBehaviour
    {
        public string deeplinkURL;
        public List<DeepLinkCommandBase> Commands;
        private void Awake()
        {
            Application.deepLinkActivated += OpenApplicationByLink;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                OpenApplicationByLink(Application.absoluteURL);
            }
            else
            {
                deeplinkURL = "[none]";
            }
        }

        private void OpenApplicationByLink(string url)
        {
            deeplinkURL = url;
            Debug.Log($"DeppLinksManager.OpenApplicationByLink {url}");
        }

        public void ApplyActions(string absoluteUrl = "", Dictionary<string, object> attributions = null)
        {
            var anyCommandEvaluated = false;
            foreach (var command in Commands)
            {
                var tryEval = command.TryEval(absoluteUrl, attributions);
                if (tryEval)
                {
                    Debug.Log($"Eval DeepLink command {command.Name}");
                    anyCommandEvaluated = true;
                }
            }

            if (!anyCommandEvaluated)
            {
                Debug.LogError($"Incorrect deepLink URL {absoluteUrl}. No DeepLink Command found.");
            }
        }
    }
}
