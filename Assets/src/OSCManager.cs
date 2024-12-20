using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{

    private long _lastOscTimeStamp = -1;


    /*OSCから受け取った情報を保管するための変数をOSCManagerクラスの
	 * フィールドとして定義。
	/** OSC Argument(s) */
    public float Sum = 0;
    public float Sum1 = 0; //衝撃の値を代入する
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
        //OSC通信のログを更新する
        OSCHandler.Instance.UpdateLogs();

        foreach (KeyValuePair<string, ServerLog> item in OSCHandler.Instance.Servers)
        {
            for (int i = 0; i < item.Value.packets.Count; i++)
            {
                if (_lastOscTimeStamp < item.Value.packets[i].TimeStamp)
                {

                    _lastOscTimeStamp = item.Value.packets[i].TimeStamp;


                    //OSC Addressを取得し、adressに代入する
                    string address = item.Value.packets[i].Address;
                    Sum1 = (float)item.Value.packets[i].Data[0];
                    Sum2 = (float)item.Value.packets[i].Data[1];
                    Sum3 = (float)item.Value.packets[i].Data[2];
                    Sum = Sum1 + Sum2 + Sum3;

                    //OSC Addressが/dataAの場合、パケットの先頭のデータをdataA1に代入する
                    //if(address == "/test") {
                    //dataA1 = (int)item.Value.packets[i].Data[0];
                    //Debug.Log("aaa");
                    //datay = (int)item.Value.packets[i].Data[1];
                    //}

                    /* 他のOSC Addressもある場合、必要に応じて以下
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
	//以下Unityから発信するために必要*/
    void sendDataOSCA(int dataA1)
    {
        var sampleVals = new List<int>() { dataA1 };
        OSCHandler.Instance.SendMessageToClient("Processing", "/eventA", sampleVals);
    }
}
