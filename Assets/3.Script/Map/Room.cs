using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public delegate void OnSelectRoom(); // �ش� �� ��ġ�� ����� ��������Ʈ
    public delegate void OnTouchOther(); // �ٸ� �� ��ġ�� ����� ��������Ʈ

    public OnSelectRoom onSelectRoom;
    public OnTouchOther onTouchOther;

    private float _posX;
    private float _posY;
    private bool _isGenerate;
    private bool _isGoable;
    private bool _isBigger;
    private bool _isHighlight;

    private bool _isEnable = false;

    private StageData _stageData;

    public List<Room> connectedRooms;
    
    [SerializeField] 
    private Animator _anim;
    [SerializeField]
    private Image _outLineImage;
    [SerializeField]
    private Image _childImage;
    [SerializeField]
    private Color _unSelectedColor;
    [SerializeField]
    private Color _selectedColor;

    public float PosX => _posX;
    public float PosY => _posY;
    public bool IsGenerate { get { return _isGenerate; } set { _isGenerate = value; } }
    public bool IsGoable
    {
        get { return _isGoable; }
        set 
        { 
            _isGoable = value;
            
            if(_isEnable)
            {
                IsBigger = false;
                IsHighlight = false;
                _anim.SetBool("isGoable", _isGoable);
            }
        }
    }

    public bool IsBigger
    {
        get { return _isBigger; }
        set
        {
            _isBigger = value;

            if (_isEnable)
            {
                _anim.SetBool("isBigger", _isBigger);
            }
        }
    }

    public bool IsHighlight
    {
        get { return _isHighlight; }
        set
        {
            _isHighlight = value;

            if (_isEnable)
            {
                if (IsGoable)
                    return;

                IsBigger = false;
                _anim.SetBool("isHighlight", _isHighlight);
            }
        }
    }

    public ERoomType RoomType { get; set; }

    private void OnEnable()
    {
        _isEnable = true;

        _anim.SetBool("isGoable", _isGoable);
        _anim.SetBool("isBigger", false);
        _anim.SetBool("isHighlight", false);
    }

    private void OnDisable()
    {
        _isEnable = false;
    }

    public void InitRoom(float posX, float posY)
    {
        _posX = posX;
        _posY = posY;

        _isGenerate = false;
        _isGoable = false;

        connectedRooms = new List<Room>();

        GetComponent<Button>().onClick.AddListener(() => onSelectRoom());

        onSelectRoom += OnClickButton;
    }

    public void Positioning()
    {
        transform.localPosition = new Vector3(_posX, _posY, 0);
        transform.SetAsLastSibling();
    }

    public void SetStageType(StageData stageData, ERoomType roomType)
    {
        _stageData = stageData;

        // �̹���, �ƿ����� �ٲٱ�
        _outLineImage.sprite = stageData.spriteOutline;
        _childImage.sprite = stageData.sprite;

        RoomType = roomType;

        _isGenerate = true;
    }

    public void OnClickButton()
    {
        // ���� 90 -> 40
        // ũ�� 1.5��

        if(_isGoable)
        {
            // �ش� �� ����
            IsGoable = false;

            GameManager.Game.CurrentRoom = this;

            // ��ġ�� �ؾ� �ҵ� 
            FindObjectOfType<RoomManager>().EnterRoom(RoomType);

            // GameManager.Room.EnterRoom(RoomType);
        }
        else
        {
            GameManager.Game.SelectedRoom = this;
        }
    }

    // �������� Ŭ���� �� 
    public void ClearRoom()
    {
        foreach (Room connectedRoom in connectedRooms)
        {
            connectedRoom.IsGoable = true;
        }
    }
}
