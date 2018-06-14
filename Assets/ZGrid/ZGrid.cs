using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZLib{

	public class ZGrid<T> : IZGrid, IZGrid<T>, IZGridIterator, IZGridIterator<T>{

		/// <summary>
		/// The data.
		/// </summary>
		ZGridIterator<T> _Data;
		ZGridIterator _Data2;

		ZRowsIterator<T> _Rows;
		ZRowsIterator _Rows2;

		ZRowsIterator<T> _Cols;
		ZRowsIterator _Cols2;

		internal Dictionary<int, T> _DataList;// = new List<T>();

		//Interface's property
		public IZGridIterator<T> Datas {
			get{
				return _Data;
			}
		}

		IZGridIterator IZGrid.Datas {
			get{
				return _Data2;
			}
		}

		public IZRowsIterator<T> Rows {
			get{
				return _Rows;
			}
		}

		IZRowsIterator IZGrid.Rows {
			get{
				return _Rows2;
			}
		}

		public int Width{ get; }
		public int Height{ get;}

		/// <summary>
		/// Initializes a new instance of the <see cref="ZLib.ZGrid`1"/> class.
		/// </summary>
		/// <param name="sizeX">Size x.</param>
		/// <param name="sizeY">Size y.</param>
		public ZGrid(int witdh, int height){
			_Data = new ZGridIterator<T> (this);
			_Data2 = new ZGridIterator (this);

			_Rows = new ZRowsIterator<T> (this);
			_Rows2 = new ZRowsIterator (this);

			Width = witdh;
			Height = height;

			//init the data list
			_DataList = new Dictionary<int, T> (Width * Height);
		}


		public T this [int row, int col] {
			set {
				SetData (row, col, value);
			}
			get {
				return GetData(row, col);
			}
		}

		public IZRowIterator<T> this [int row] {

			get {
				return Rows[row];
			}
		}

		object IZGridIterator.this [int row, int col] {
			set {
				SetData (row, col, (T)value);
			}
			get {
				return GetData(row, col);
			}
		}

		IZRowIterator IZGridIterator.this [int row] {

			get {
				return Rows[row] as IZRowIterator;
			}
		}

		//Interface's function
		object IZGrid.GetData (int col, int row)
		{
			return _DataList[col * Width + row];
		}

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <returns>The data.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		public T GetData (int row, int col)
		{
			return _DataList[col * Width + row];
		}

		/// <summary>
		/// Sets the data.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="data">Data.</param>
		void IZGrid.SetData(int row, int col, object data){
			_SetData (row, col, (T)data);
		}

		/// <summary>
		/// Sets the data.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="data">Data.</param>
		public void SetData(int row, int col, T data){
			_SetData (row, col, data);
		}

		/// <summary>
		/// Sets the data.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="data">Data.</param>
		void _SetData(int row, int col, T data){
			_DataList [col * Width + row] = data;

			//if (typeof(T).IsSubclassOf (ZGridIndexable<T>)) {

			//}
			var indexableData = data as IZGridIndexable;
			if (indexableData != null) {
				indexableData.Row = row;
				indexableData.Col = col;
			}
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear(){
			_DataList.Clear ();
		}

		/// <summary>
		/// Contains the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		bool IZGrid.Contains(object value)
		{
			return _DataList.ContainsValue((T)value);
		}

		/// <summary>
		/// Contains the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public bool Contains(T value)
		{
			return _DataList.ContainsValue (value);
		}

		/// <summary>
		/// Subs the grid.
		/// </summary>
		/// <returns>The grid.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		public ZGrid<T> SubGrid(int row, int col, int width, int height){
			if (row + height >= this.Height)
				return null;

			if (col + width >= this.Width)
				return null;

			ZGrid<T> ret = new ZGrid<T> (width, height);


			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					ret.Datas [i, j] = Datas [i + row, j + col];
				}
			}

			return ret;
		}

		/// <summary>
		/// return Enumerator for data and row /col
		/// </summary>
		/// <returns>The enumerator.</returns>
		IEnumerator<T> IEnumerable<T>.GetEnumerator(){
			return _DataList.Values.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator(){
			return _DataList.Values.GetEnumerator ();
		}

		IEnumerator IZGrid.GetRowEnumerator()
		{
			return new ZRowsEnumerator<T> (this);
		}

		IEnumerator<IZRowIterator<T>> IZGrid<T>.GetRowEnumerator()
		{
			return new ZRowsEnumerator<T> (this);
		}

		IEnumerator IZGrid.GetColEnumerator()
		{
			return new ZRowsEnumerator<T> (this);
		}

		IEnumerator<IZRowIterator<T>> IZGrid<T>.GetColEnumerator()
		{
			return new ZRowsEnumerator<T> (this);
		}


		/// <summary>
		/// Gets the row.[TODO]need use the objects pool
		/// </summary>
		/// <returns>The row.</returns>
		/// <param name="row">Row.</param>
		IZRowIterator IZGrid.GetRow (int row)
		{
			return new ZRowIterator<T>(this, row);
		}
		IZRowIterator IZGrid.GetCol (int col)
		{
			return new ZRowIterator<T>(this, col);
		}

		public IZRowIterator<T> GetRow (int row)
		{
			return new ZRowIterator<T>(this, row);
		}

		public IZRowIterator<T> GetCol (int col)
		{
			return new ZRowIterator<T>(this, col);
		}
	}
}


