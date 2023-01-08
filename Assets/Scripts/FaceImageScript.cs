using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceImageScript : MonoBehaviour
{
    // SerializeField allow us to see this value in the inespector
    [SerializeField] private GameObject FaceMario;
    [SerializeField] private GameManager gameManager;

    public void OnMouseDown()
    {
        gameManager.checkIdFaceClick(_spriteId);
    }

    private int _spriteId;

    public int spriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

}
