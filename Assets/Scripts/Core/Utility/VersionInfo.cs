using TMPro;
using UnityEngine;

namespace Core.Utility
{
    public class VersionInfo : MonoBehaviour
    {
        private const string VER_TEXT = "Ver. {0}";
        [SerializeField] private TextMeshProUGUI versionText;

        private void Update()
        {
            versionText.text = string.Format(VER_TEXT, Application.version);
        }
    }
}
