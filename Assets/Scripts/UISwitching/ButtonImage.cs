using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonImage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

	public Sprite HoverOrPressedImage;
	public Sprite NormalSprite;
	Image extraImage;
	Image btnSprite;
	Button btn;


	public void OnPointerEnter(PointerEventData eventData)
	{
      
		btn.image.sprite= HoverOrPressedImage;

	}
	public void OnPointerExit(PointerEventData eventData)
	{
		btn.image.sprite = NormalSprite;
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		btn.image.sprite= HoverOrPressedImage;
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		btn.image.sprite = NormalSprite;
	}


	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
		btnSprite = btn.image;

//		btn.OnPointerDown +=OnImageChangeToHover;
//		btn.OnPointerEnter += OnImageChangeToHover;
//		btn.OnPointerExit +=OnImageChangeToNormal;
//		btn.OnPointerUp += OnImageChangeToNormal;
	}
	
//	void OnImageChangeToHover(UnityEngine.EventSystems.PointerEventData eventData)
//	{
//
//		btn.image = HoverOrPressedImage;
//
//	}
//	void OnImageChangeToNormal(UnityEngine.EventSystems.PointerEventData eventData)
//	{
//
//		btn.image = btnSprite;
//
//	}
}
