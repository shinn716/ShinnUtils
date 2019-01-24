using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBreathe : MonoBehaviour {

    public enum Type2D
    {
        Text, Image, Sprite
    }
    private Color TypeSelect(GameObject go)
    {
        switch (_2Dtype)
        {
            default:
                Image imaged = go.GetComponent<Image>();
                return imaged.color = new Color(orgColor.r, orgColor.g, orgColor.b, fadeout);
            case Type2D.Text:
                Text text = go.GetComponent<Text>();
                return text.color = new Color(orgColor.r, orgColor.g, orgColor.b, fadeout);
            case Type2D.Image:
                Image image = go.GetComponent<Image>();
                return image.color = new Color(orgColor.r, orgColor.g, orgColor.b, fadeout);
            case Type2D.Sprite:
                SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
                return sprite.color = new Color(orgColor.r, orgColor.g, orgColor.b, fadeout);
        }
    }


    public Type2D _2Dtype;

    public GameObject fadeobj;
    [Range(0, 2)]
    public float speed = .5f;
    public Color orgColor = Color.white;
    public Vector2 UpperLower = Vector2.one;

    private float fadeout = 0;
    private bool breathin = true;
    private bool breathout = false;

    void FixedUpdate()
    {
        if (breathin)
        {
            if (fadeout > UpperLower.y)
            {
                fadeout = UpperLower.y;
                breathout = true;
                breathin = false;
            }
            else
                fadeout += speed * Time.fixedDeltaTime;
            
            TypeSelect(fadeobj);
        }

        else if (breathout)
        {
            if (fadeout < UpperLower.x)
            {
                fadeout = UpperLower.x;
                breathout = false;
                breathin = true;
            }
            else
                fadeout -= speed * Time.fixedDeltaTime;

            TypeSelect(fadeobj);
        }

    }
}
