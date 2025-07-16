using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GunChooseManager : MonoBehaviour
{
    public static GunChooseManager instance;

    public class GunStatus
    {
        public string GunName;
        public int num = 0;
        public GameObject GunModel;
        public int damage;
        public float AttackSpeed;
        public float Range;
        public float ReloadTime;
    }

    public List<GunStatus> GunStatusList = new List<GunStatus>();
    public int CurrentNum = 0;
    public GameObject GunModelParent;
    public TextMeshProUGUI GunName;
    public TextMeshProUGUI GunStatusText;

    public float rotationSpeed = 30f; // ȸ�� �ӵ� (��/��)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // ���� ���õ� �ѱ� �� ȸ��
        if (GunModelParent.transform.childCount > CurrentNum)
        {
            Transform currentGun = GunModelParent.transform.GetChild(CurrentNum);
            if (currentGun.gameObject.activeSelf)
            {
                currentGun.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            }
        }
    }

    public void GunLotate()
    {
        GameObject gun = GunModelParent.transform.GetChild(CurrentNum).gameObject;

        for (int i = 0; i < GunModelParent.transform.childCount; i++)
        {
            GameObject child = GunModelParent.transform.GetChild(i).gameObject;
            child.SetActive(i == CurrentNum);
        }

        GunName.text = GunStatusList[CurrentNum].GunName;
        GunStatusText.text = $"���ݷ� : {GunStatusList[CurrentNum].damage} ���� �ӵ� : {GunStatusList[CurrentNum].AttackSpeed}�� ź�� : {GunStatusList[CurrentNum].Range}�� ���� �ð� : {GunStatusList[CurrentNum].ReloadTime}��";
    }
}
