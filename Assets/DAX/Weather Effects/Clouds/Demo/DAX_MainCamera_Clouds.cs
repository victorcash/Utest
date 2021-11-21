using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DAX_MainCamera_Clouds : MonoBehaviour 
{
	public GameObject[] Clouds;
	public Text OutText;
	public int curIndex = 0;

	GameObject curPrefab = null;

	// Use this for initialization
	void Start () 
	{
		showPrefab( this.curIndex );
	}

	void Update()
	{
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		this.OutText.text = string.Format( "{0}/{1}", this.curIndex+1, this.Clouds.Length );
	}

	public void Next()
	{
		this.curIndex += 1;
		if (this.curIndex >= this.Clouds.Length )
		{
			this.curIndex = 0;
		}
		showPrefab( this.curIndex );
	}

	public void Prev()
	{
		this.curIndex -= 1;
		if (this.curIndex <0) { this.curIndex = this.Clouds.Length-1;};
		showPrefab( this.curIndex );
	}

	public void  showPrefab( int index )
	{
		if (this.curPrefab!=null)
		{
			GameObject.Destroy( this.curPrefab );
		}
		this.curPrefab = Instantiate( this.Clouds[ this.curIndex ] ) as GameObject;
		this.curPrefab.transform.position.Set( 0f, 0f, 0f );
	}


}
