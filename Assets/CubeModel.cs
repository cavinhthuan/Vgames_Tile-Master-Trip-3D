using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class CubeModel : MonoBehaviour, IBoolObject
{
    [SerializeField] private SpriteRenderer sprite;
    public Rigidbody m_rigidbody;
    public MeshRenderer meshRenderer;
    public int CubeKey;
    public Outline outline;
    public Action HandleClick = () => { };
    LayerMask tableMask;
    public bool onBoolActive
    {
        get;
        set;
    }

    // Start is called before the first frame update

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        // Check if there are any active touches
        if(Input.touchCount > 0)
        {
            // Get the first active touch
            Touch touch = Input.touches[0];
            if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject == gameObject)
                    {
                        // Cube is being hovered
                        HandleFocus();
                    }
                }
            }
            else if(touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject == gameObject)
                    {
                        // Cube is being clicked
                        HandleClick();
                    }
                }
            }
        }

        var dir = new Vector3(0, 0, 1);

        // Declare a variable to store the hit information
        if(!Physics.Raycast(transform.position, transform.TransformDirection(dir) * 200, out RaycastHit hitTable, Mathf.Infinity, LayerMask.GetMask(ConstantVariables.MASK_TABLE)))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hitTable.normal);
            m_rigidbody.MoveRotation(targetRotation);
        }
    }
    private void HandleFocus()
    {
        this.PostEvent(EventID.FocusCube, this);
    }

    private void OnEnable()
    {
        onBoolActive = true;
        this.RegisterListener(EventID.FocusCube, (param) => FocusCube(param as CubeModel));
        this.RegisterListener(EventID.TimeUp, (param) => HandleClick -= OnClickedCube);
        HandleClick += OnClickedCube;
    }
    private void OnDisable()
    {
        onBoolActive = false;
        HandleClick -= OnClickedCube;
    }

    private void FocusCube(CubeModel cubeModel)
    {
        if(cubeModel != this)
        {
            outline.enabled = false;
            return;
        }
        outline.enabled = true;
    }


    public void OnClickedCube()
    {
        this.PostEvent(EventID.FocusCube, this);
        GameManager.Instance.swapController.Remove(this);
        GameManager.Instance.holder.Add(this);
    }

    public void SetSprite(Sprite input)
    {
        if(sprite != null)
        {
            sprite.sprite = input;
        }
    }
}

