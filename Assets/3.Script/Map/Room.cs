using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public delegate void OnSelectRoom(); // 해당 방 터치시 실행될 델리게이트
    public delegate void OnTouchOther(); // 다른 곳 터치시 실행될 델리게이트

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

        // 이미지, 아웃라인 바꾸기
        _outLineImage.sprite = stageData.spriteOutline;
        _childImage.sprite = stageData.sprite;

        RoomType = roomType;

        _isGenerate = true;
    }

    public void OnClickButton()
    {
        // 색깔 90 -> 40
        // 크기 1.5배

        if(_isGoable)
        {
            // 해당 방 진입
            IsGoable = false;

            GameManager.Game.CurrentRoom = this;

            // 고치긴 해야 할듯 
            FindObjectOfType<RoomManager>().EnterRoom(RoomType);

            // GameManager.Room.EnterRoom(RoomType);
        }
        else
        {
            GameManager.Game.SelectedRoom = this;
        }
    }

    // 스테이지 클리어 시 
    public void ClearRoom()
    {
        foreach (Room connectedRoom in connectedRooms)
        {
            connectedRoom.IsGoable = true;
        }
    }
}
