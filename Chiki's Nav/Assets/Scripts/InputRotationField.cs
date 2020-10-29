using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotationField : MonoBehaviour
{
    float rotation;

    private void Awake()
    {
        rotation = 0.0f;
    }
    public void SetValueNotOverload()
    {
        if (this.GetComponent<TMPro.TMP_InputField>().text != "")
        {
            rotation = float.Parse(this.GetComponent<TMPro.TMP_InputField>().text);
            if (rotation >= 360.0f)
            {
                rotation = rotation % 360.0f;
                this.GetComponent<TMPro.TMP_InputField>().text = rotation.ToString();
            }
            else if (rotation < 0)
            {
                if (rotation <= -360.0f)
                {
                    rotation = rotation % 360.0f;
                }
                if (rotation < 0)
                {
                    rotation = rotation + 360.0f;
                }
                this.GetComponent<TMPro.TMP_InputField>().text = rotation.ToString();
                //아무것도 하지않음
            }
        }
    }

    public void SetRotationValue(float input)
    {
        rotation = input;
    }

    public float GetRotationValue()
    {
        return rotation;
    }
}
