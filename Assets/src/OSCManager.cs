using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{

    private long _lastOscTimeStamp = -1;


    /*OSC����󂯎��������ۊǂ��邽�߂̕ϐ���OSCManager�N���X��
	 * �t�B�[���h�Ƃ��Ē�`�B
	/** OSC Argument(s) */
    public float Sum = 0;
    public float Sum1 = 0; //�Ռ��̒l��������
    public float Sum2 = 0;
    public float Sum3 = 0;
    //public int datay = 0;
    /*
	public float sensorB1 = 0.0f;
	public float sensorB2 = 0.0f;
	*/

    // Use this for initialization
    void Start()
    {
        OSCHandler.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        //OSC�ʐM�̃��O���X�V����
        OSCHandler.Instance.UpdateLogs();

        foreach (KeyValuePair<string, ServerLog> item in OSCHandler.Instance.Servers)
        {
            for (int i = 0; i < item.Value.packets.Count; i++)
            {
                if (_lastOscTimeStamp < item.Value.packets[i].TimeStamp)
                {

                    _lastOscTimeStamp = item.Value.packets[i].TimeStamp;


                    //OSC Address���擾���Aadress�ɑ������
                    string address = item.Value.packets[i].Address;
                    Sum1 = (float)item.Value.packets[i].Data[0];
                    Sum2 = (float)item.Value.packets[i].Data[1];
                    Sum3 = (float)item.Value.packets[i].Data[2];
                    Sum = Sum1 + Sum2 + Sum3;

                    //OSC Address��/dataA�̏ꍇ�A�p�P�b�g�̐擪�̃f�[�^��dataA1�ɑ������
                    //if(address == "/test") {
                    //dataA1 = (int)item.Value.packets[i].Data[0];
                    //Debug.Log("aaa");
                    //datay = (int)item.Value.packets[i].Data[1];
                    //}

                    /* ����OSC Address������ꍇ�A�K�v�ɉ����Ĉȉ�
					if(address == "/sensorB") {
						dataB1 = (float)item.Value.packets[i].Data[0];
						dataB2 = (float)item.Value.packets[i].Data[1];
					}
					*/

                    Debug.Log(address + ":(" + item.Value.packets[i].Data[0] + ", " + item.Value.packets[i].Data[1] + ", " + item.Value.packets[i].Data[2] + ")");


                }
            }
        }
    }

    /*
	//�ȉ�Unity���甭�M���邽�߂ɕK�v*/
    void sendDataOSCA(int dataA1)
    {
        var sampleVals = new List<int>() { dataA1 };
        OSCHandler.Instance.SendMessageToClient("Processing", "/eventA", sampleVals);
    }
}
