using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZLib;


public class TestGrid : MonoBehaviour {

	public class TestData : ZGridIndexable<TestData>{
		public int data;
	}

	// Use this for initialization
	void Start () {
		//common 
		int[][] array = new int[2][] { new int[] {0,11,12 }, new int[] {1,2,3,4,5 }};
		for (int i = 0; i < array.Length; i++) {
			foreach (int j in array[i]) {
				Debug.Log ("i  " + i + " j " + j);
			}
		}



		ZGrid<int> grid = new ZGrid<int> (10, 10);
		//数据索引器的使用

		//赋值操作
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				grid[i, j] = j * 10 + i;
			}
		}

		Debug.Log ("grid size is " + grid.Width);

		//遍历操作
		foreach (var i in grid.Datas) {
			//Debug.Log ("item is " + i);
		}

		//行、列索引器的使用
		foreach (var i in grid.Rows) {

			foreach (var d in i) {
				//Debug.Log ("row is " + d);
			}
		}

		//获取子网格
		var subGrid = grid.SubGrid(5, 5, 3, 3);
		foreach (var i in subGrid) {
			Debug.Log ("Sub item is " + i);
		}

		//Ling操作
		var items = subGrid.Datas.Where (a => a > 60).ToList ();

		foreach (var i in items) {
			Debug.Log ("Select item is " + i);
		}


		ZGrid<TestData> dataGrid = new ZGrid<TestData> (3, 3);
		dataGrid[2, 2] = new TestData ();
		dataGrid[2, 2].data = 10;

		Debug.Log ("dataGrid[2, 2] col = " + dataGrid [2, 2].Col);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
