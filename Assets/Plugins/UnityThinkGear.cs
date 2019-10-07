using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class UnityThinkGear{

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern void TGAM_Init(bool rawEnabled);
	
	[DllImport ("__Internal")]
	private static extern void TGAM_Close();

	[DllImport ("__Internal")]
	private static extern void TGAM_StartStream();

	[DllImport ("__Internal")]
	private static extern void TGAM_StopStream();
	
	[DllImport ("__Internal")]
	private static extern bool TGAM_GetSendRawEnable();

	[DllImport ("__Internal")]
	private static extern bool TGAM_GetSendEEGEnable();
	
	[DllImport ("__Internal")]
	private static extern bool TGAM_GetSendESenseEnable ();
		
	[DllImport ("__Internal")]
	private static extern bool TGAM_GetSendBlinkEnable();

	[DllImport ("__Internal")]
	private static extern void TGAM_SetSendRawEnable(bool enabled);
	
	[DllImport ("__Internal")]
	private static extern void TGAM_SetSendEEGEnable(bool enabled);

	[DllImport ("__Internal")]
	private static extern void TGAM_SetSendESenseEnable(bool enabled);
	
	[DllImport ("__Internal")]
	private static extern void TGAM_SetSendBlinkEnable(bool enabled);
#endif
	/*
    
    Following are connection status that declared in UnityThinkGear2User.Jar
    when you getConnectStatex(),the return value will be one of following string
    	* 
	public static final String STATE_IDLE = "idle";
	public static final String STATE_CONNECTING = "connecting";
	public static final String STATE_CONNECTED = "connected";
	public static final String STATE_NOT_FOUND = "not found";
	public static final String STATE_NOT_PAIRED = "not paired";
	public static final String STATE_DISCONNECTED = "disconnected";
	public static final String LOW_BATTERY = "low battery";
	public static final String BLUETOOTH_ERROR = "bluetooth error";

	public String connectState1 = STATE_IDLE;
	public String connectState2 = STATE_IDLE;
     */
	private static AndroidJavaClass jc;
	private static AndroidJavaObject jo;




	public static void Init(bool rawEnabled){
#if UNITY_IPHONE
		TGAM_Init(rawEnabled);
#endif
		jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		jo.Set<bool>("sendRawEnable", rawEnabled);

	}
	public static void Close(){
#if UNITY_IPHONE
		TGAM_Close();
#endif
		jo.Call("disconnect");

	}
	public static void StartStream(){
#if UNITY_IPHONE
		TGAM_StartStream();
#endif
		jo.Call("connectWithRaw");

	}
	public static void StopStream(){
#if UNITY_IPHONE
		TGAM_StopStream();
#endif
		jo.Call("disconnect");

	}
	
	//========================
	public static bool GetSendRawEnable(){
#if UNITY_IPHONE
		return TGAM_GetSendRawEnable();
#endif
		return jo.Get<bool>("sendRawEnable");

	}
	public static bool GetSendEEGEnable(){
#if UNITY_IPHONE
		return TGAM_GetSendEEGEnable();
#endif
		return jo.Get<bool>("sendEEGEnable");

	}
	public static bool GetSendESenseEnable(){
#if UNITY_IPHONE
		return TGAM_GetSendESenseEnable();
#endif
		return jo.Get<bool>("sendESenseEnable");

	}
	public static bool GetSendBlinkEnable(){
#if UNITY_IPHONE
		return TGAM_GetSendBlinkEnable();
#endif
		return jo.Get<bool>("sendBlinkEnable");

	}
	
	//========================
	public static void SetSendRawEnable(bool enabled){
#if UNITY_IPHONE
		TGAM_SetSendRawEnable(enabled);
#endif
		jo.Set<bool>("sendRawEnable", enabled);

	}
	public static void SetSendEEGEnable(bool enabled){
#if UNITY_IPHONE
		TGAM_SetSendEEGEnable(enabled);
#endif
		jo.Set<bool>("sendEEGEnable", enabled);

	}
	public static void SetSendESenseEnable(bool enabled){
#if UNITY_IPHONE
		TGAM_SetSendESenseEnable(enabled);
#endif
		jo.Set<bool>("sendESenseEnable", enabled);

	}
	public static void SetSendBlinkEnable(bool enabled){
#if UNITY_IPHONE
		TGAM_SetSendBlinkEnable(enabled);
#endif
		jo.Set<bool>("sendBlinkEnable", enabled);

	}
	
}
