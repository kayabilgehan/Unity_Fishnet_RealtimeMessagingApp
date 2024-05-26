using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MaxWidth : MonoBehaviour {
	[SerializeField] private ContentSizeFitter fitter;
	[SerializeField] private LayoutElement layoutElement;
	[SerializeField] private RectTransform rectTransform;
	[SerializeField] private RectTransform parentElement;
	[SerializeField] private float leftPadding = 50;
	[SerializeField] private float rightPadding = 50;

	void Update() {
		if (rectTransform.rect.width >= parentElement.rect.width) {
			float calculatedWidth = parentElement.rect.width - leftPadding - rightPadding;
			rectTransform.rect.Set(0,0,calculatedWidth,0);
			layoutElement.preferredWidth = calculatedWidth;
		}
	}
}
