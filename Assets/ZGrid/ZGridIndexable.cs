using System;

namespace ZLib
{
	public interface IZGridIndexable{
		int Row{ set; get;}
		int Col{ set; get;}
	}
	
	public class ZGridIndexable<T> : IZGridIndexable where T : ZGridIndexable<T>
	{
		public int Row{ set; get;}
		public int Col{ set; get;}

		public ZGridIndexable ()
		{
			

		}
	}
}

