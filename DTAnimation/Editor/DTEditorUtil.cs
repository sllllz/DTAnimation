using UnityEditor;
using UnityEngine;

namespace Extend.DTAnimation.Editor
{
    public static class DTEditorUtil{
        public static readonly float LINE_HEIGHT = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    
		private static GUIStyle m_buttonSelectedStyle;
		public static GUIStyle ButtonSelectedStyle {
			get {
				if( m_buttonSelectedStyle == null ) {
					var style = (GUIStyle)"ButtonMid";
					m_buttonSelectedStyle = new GUIStyle(style) {
						normal = new GUIStyleState() {
							background = style.onActive.scaledBackgrounds[0],
							textColor = new Color(0.7f, 0.7f, 0.7f, 1)
						}
					};
				}

				return m_buttonSelectedStyle;
			}
		}
	}
}