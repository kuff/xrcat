using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using TMPro;

namespace Playmode
{
    [RequireComponent(typeof(RotateWithCursor))]
    public class CursorInstructionsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI instructionText;
        private RotateWithCursor _rotateWithCursorScript;

        private void Start()
        {
            _rotateWithCursorScript = GetComponent<RotateWithCursor>();
        }

        private void Update()
        {
            UpdateInstructionText();
        }

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private void UpdateInstructionText()
        {
            instructionText.text = _rotateWithCursorScript.CursorIsConfined 
                ? "Tryk ESC for at give slip." 
                : "Klik for at rotere.";
        }
    }
}