using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBehavior : MonoBehaviour
{
    enum Model
    {
        Select,
        Operation,
    }
    private Node currentSelectNode;
    private Model currentModel = Model.Select;
    public GameObject OperationUI;
    public Button RotateBtn;
    // Start is called before the first frame update
    void Start()
    {
        RotateBtn.onClick.AddListener(OnRotationBtnClick);
    }

    private void OnRotationBtnClick()
    {
        if (currentSelectNode)
        {
            currentSelectNode.Rotate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentModel == Model.Select)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var nodeModel = hit.transform.GetComponent<NodeModel>();
                    if (nodeModel)
                    {
                        Debug.Log($"µã»÷µ½ÁË{hit.transform.name}");
                        currentSelectNode = nodeModel.BindNode;
                        EnterOperationModel();
                    }
                }
            }
        }
        else if (currentModel == Model.Operation)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EnterSelectModel();
            }
        }


    }
    void EnterOperationModel()
    {
        currentModel = Model.Operation;
        OperationUI.SetActive(true);
    }
    void EnterSelectModel()
    {
        currentModel = Model.Select;
        currentSelectNode = null;
        OperationUI.SetActive(false);

    }
}
