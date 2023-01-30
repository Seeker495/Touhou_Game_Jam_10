using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
	[SerializeField] private GameObject m_fadeObject;
	[SerializeField] private GameObject m_imgObject;
	private int timer = 0;
	[SerializeField] private Sprite panel2, panel2b, panel3;
	[SerializeField] private GameObject popup1, popup2;
	Image img;
	
    // Start is called before the first frame update
    void Start()
    {
		img = m_imgObject.GetComponent<Image>();
		
		// ideally want to load from here instead of assigning each sprites by the Unity editor
        //panel2 = Resources.Load<Sprite>("orin_story2");
		//panel2b = Resources.Load<Sprite>("orin_story2_bw");
		//panel2_popup1 = Resources.Load<Sprite>("orin_story2_orinhappy");
		//panel2_popup2 = Resources.Load<Sprite>("orin_story2_ohno");
		//panel3 = Resources.Load<Sprite>("orin_story3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		timer++;
		
		if (timer > 200 && timer < 400) {
			img.sprite = panel2;
		}
		if (timer > 400 && timer < 800) {
			img.sprite = panel2b;
		}
		if (timer > 400 && timer < 800) {
			popup1.SetActive(true);
		}
		if (timer > 600 && timer < 800) {
			popup2.SetActive(true);
		}
		
		if (timer > 800 && timer < 1050) {
			// set popups to disable for next panel
			popup1.SetActive(false);
			popup2.SetActive(false);
			
			img.sprite = panel3;
		}
		
		if (timer > 1050) {
			// start game
			// TODO: put it not in FixedUpdate() so it'd be called only once
			//#if !UNITY_EDITOR
       // Fade.FadeOut_with_Scene(this, "Play", 2.0f);
//#else
        SceneManager.LoadSceneAsync("Play");
//#endif
		}
	}
}
