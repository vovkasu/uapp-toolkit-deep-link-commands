using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UAppToolkit.DeepLinkCommands.Sample
{
    public class SampleDisplay : MonoBehaviour
    {
        public DeepLinksManager DeepLinksManager;
        public Text Url;
        public Transform UrlParametersContainer;
        public UrlParameterInfo UrlParameterInfoPrefab;

        public Text CommandsList;

        private void Awake()
        {
            DeepLinksManager.OnAppliedCommands += AppliedCommands;
            DeepLinksManager.OnOpenApplicationByLink += OpenApplicationByLink;
        }

        private void Start()
        {
            //Test
#if UNITY_STANDALONE_WIN
            var args = System.Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                DeepLinksManager.OpenApplicationByLink(args[1]);
            }
#else
            var testLink = "uapptoolkit://testlink?action=invite&parent-name=vovkasu&parent-id=123456";
            DeepLinksManager.OpenApplicationByLink(testLink);
#endif
        }

        private void OpenApplicationByLink(string url, Dictionary<string, object> parameters)
        {
            Url.text = url;

            var childCount = UrlParametersContainer.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                var child = UrlParametersContainer.GetChild(i);
                Destroy(child.gameObject);
            }

            foreach (var key in parameters.Keys)
            {
                var urlParameterInfo = Instantiate(UrlParameterInfoPrefab, UrlParametersContainer);
                urlParameterInfo.Init(key, parameters[key]);
            }
        }

        private void AppliedCommands(List<DeepLinkCommandBase> commands)
        {
            var names = commands.Select(_ => _.Name);
            CommandsList.text = string.Join(", ", names);
        }
    }
}
