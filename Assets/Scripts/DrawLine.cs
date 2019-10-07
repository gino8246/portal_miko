using UnityEngine;  
using System.Collections;  
using System;  
public class DrawLine : MonoBehaviour {  
	
	
	public Vector2[] m_point;//特征点位置  
	public Color m_lineColor;  
	private static Texture2D m_texure;//最终渲染得到的带有折线的纹理  
	
	
	public void InitCanvas(Vector2[] point, int width, int height) {  
		m_point = point;  
		m_texure = new Texture2D(width,height); 
		/*m_point = new Vector2[2]{ new Vector2( 50, 100 ),new Vector2( 100, 80 )};  
		m_texure = new Texture2D(200,200);*/
	}  
	
	
	public IEnumerator Draw()  
	{  
		//清空纹理对象  
		for (int i = 0; i < 100; i++)  
		{  
			for (int j = 0; j < 100; j++)  
			{  
				m_texure.SetPixel(i, j, Color.white);  
			}  
		}  
		
		
		Vector2 currentPoint = m_point[0];  
		for (int i = 1; i < m_point.Length; i++) {  
			for (float j = 0; j < 1; j = j + 0.01f) {  
				Vector2 temp = Vector2.Lerp(m_point[i-1],m_point[i],j);  
				m_texure.SetPixel(Convert.ToInt32(temp.x),Convert.ToInt32(temp.y),m_lineColor);  
			}  
			currentPoint = m_point[i];  
		}  
		m_texure.Apply();  
		yield return m_texure;  
	}  
	void OnPostRender()  
	{  
		StartCoroutine(Draw());  
	}  
	
	
	void Start() {  
		InitCanvas(m_point, 100, 100);  
	}  
	
	
	void OnGUI()  
	{  
		GUI.DrawTexture(new Rect(0, 0, 100, 100), m_texure);  
	}  
}