using System;
using System.Collections.Generic;
using UnityEngine;

namespace UAppToolkit.DeepLinkCommands.Sample
{
    public class DeepLinksManager : MonoBehaviour
    {
        public string DeeplinkURL;
        public List<DeepLinkCommandBase> Commands;
        public event Action<string, Dictionary<string, object>> OnOpenApplicationByLink;
        public event Action<List<DeepLinkCommandBase>> OnAppliedCommands;

        private const string NoneLink = "[none]";
        private void Awake()
        {
            Application.deepLinkActivated += OpenApplicationByLink;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                OpenApplicationByLink(Application.absoluteURL);
            }
            else
            {
                DeeplinkURL = NoneLink;
            }
        }

        public void OpenApplicationByLink(string url)
        {
            DeeplinkURL = url;
            Debug.Log($"DeppLinksManager.OpenApplicationByLink {url}");

            if (!string.IsNullOrEmpty(url) && url != NoneLink)
            {
                var uri = new Uri(url, UriKind.Absolute);
                var parameters = System.Web.HttpUtility.ParseQueryString(uri.Query);

                var parametersDictionary = new Dictionary<string, object>();

                foreach (string parameterKey in parameters.AllKeys)
                {
                    parametersDictionary.Add(parameterKey, parameters[parameterKey]);
                }
                ApplyActions(url, parametersDictionary);

                OnOpenApplicationByLink?.Invoke(url, parametersDictionary);
            }
        }

        public void ApplyActions(string absoluteUrl = "", Dictionary<string, object> attributions = null)
        {
            var appliedCommands = new List<DeepLinkCommandBase>();
            foreach (var command in Commands)
            {
                var tryEval = command.TryEval(absoluteUrl, attributions);
                if (tryEval)
                {
                    Debug.Log($"Eval DeepLink command {command.Name}");
                    appliedCommands.Add(command);
                }
            }

            if (appliedCommands.Count == 0)
            {
                Debug.LogError($"Incorrect deepLink URL {absoluteUrl}. No DeepLink Command found.");
            }
            else
            {
                OnAppliedCommands?.Invoke(appliedCommands);
            }
        }

#if APPS_FLYER_ENABLED
        public void onConversionDataSuccess(string conversionData)
        {
            AppsFlyerSDK.AppsFlyer.AFLog("onConversionDataSuccess", conversionData);
            var conversionDataDictionary = AppsFlyerSDK.AppsFlyer.CallbackStringToDictionary(conversionData);
            Debug.Log($"%%%% onConversionDataSuccess {conversionData}");
            ApplyActions(attributions: conversionDataDictionary);
        }

        public void onConversionDataFail(string error)
        {
            AppsFlyerSDK.AppsFlyer.AFLog("onConversionDataFail", error);
        }

        public void onAppOpenAttribution(string attributionData)
        {
            AppsFlyerSDK.AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
            var attributionDataDictionary = AppsFlyerSDK.AppsFlyer.CallbackStringToDictionary(attributionData);
            Debug.Log($"%%%% onAppOpenAttribution {attributionData}");

            ApplyActions(attributions: attributionDataDictionary);
        }
#endif
    }
}
