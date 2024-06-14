using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BrokenMachine : MonoBehaviour
{
    private InputAction inputAction;

    
    [SerializeField]Image repairBar;
    [SerializeField] GameObject progress;

    bool repair = false;
    bool repairComplete = false;
    private void Awake()
    {
        // InputAction 초기화
        inputAction = new InputAction(binding: "<Keyboard>/E");

        // '누르고 있을 때'에 대한 이벤트 설정
        inputAction.performed += ctx => {
            RepairToggle();
        };

        // '뗐을 때'에 대한 이벤트 설정
        inputAction.canceled += ctx => {
            RepairToggle();
        };

    }
    private void Start()
    {
        inputAction.Disable();
    }



    private void OnEnable()
    {
        // InputAction 활성화
        inputAction.Enable();
    }

    private void OnDisable()
    {
        // InputAction 비활성화
        inputAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(repair && repairBar.fillAmount < 1)
            Repair();
    }
    private void Repair()
    {
        repairBar.fillAmount += Time.deltaTime*10;
        Main.GameManager.Repair(repairBar.fillAmount);
        if (repairBar.fillAmount >= 1) RepairComplete();
    }

    private void RepairComplete()
    {
        if (!repairComplete)
        {

            Main.GameManager.RepairComplete();
            repairComplete = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //플레이어 상호작용 키 이벤트에 연결 해제 RepairToggle()
            inputAction.Disable(); // inputaction 비활성화
            Main.GameManager.RepairUI(false, 0);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //플레이어 상호작용 키 이벤트에 연결 RepairToggle()
            inputAction.Enable(); // InputAction 활성화
            Main.GameManager.Repair(repairBar.fillAmount);
            Main.GameManager.RepairUI(true, repairBar.fillAmount);
        }
    }

    private void RepairToggle()
    {
        repair = !repair;

    }
}
