using UnityEngine;

[System.Serializable]
public class VNLine
{
    public string name;

    [TextArea(2, 5)] public string dialogue;

    public Sprite characterSprite;
}
