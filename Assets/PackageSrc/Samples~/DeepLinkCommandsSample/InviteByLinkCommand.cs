using System.Collections.Generic;
using UnityEngine;

namespace UAppToolkit.DeepLinkCommands.Sample
{
    [CreateAssetMenu(menuName = "Create InviteByLinkCommand", fileName = "InviteByLinkCommand", order = 0)]
    public class InviteByLinkCommand : DeepLinkCommandBase
    {
        public string ActionKey = "action";
        public string ActionValue = "invite";
        public string ParentNameKey = "parent-name";
        public string ParentIdKey = "parent-id";
        public override bool TryEval(string absoluteURL = "", Dictionary<string, object> parameters = null)
        {
            if (parameters != null
                && parameters.ContainsKey(ActionKey)
                && parameters[ActionKey].ToString() == ActionValue
                && parameters.ContainsKey(ParentNameKey)
                && parameters.ContainsKey(ParentIdKey))
            {
                var parentName = parameters[ParentNameKey].ToString();
                var parentId = parameters[ParentIdKey].ToString();
                Debug.Log($"Invite: parent {parentName} with id {parentId}");

                //invite implementation
                return true;
            }

            return false;
        }
    }
}