using UnityEngine;
using UnityEditor;
using Game.UI;

namespace Game.Editor
{
    [CustomEditor(typeof(HealthBar))]
    public class HealthBarEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            HealthBar healthBar = target as HealthBar;

            if (GUILayout.Button("Generate health bar"))
            {
                if (healthBar != null)
                {
                    healthBar.RenderHealthBarInEditor();
                }
            }
        }
    }
}
