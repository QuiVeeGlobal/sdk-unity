using System.Collections;


namespace Roar.WebObjects
{
	public interface IResponse
	{
		void ParseXml( IXMLNode nn );
	}
	
	namespace Shop
	{
		
		public class ListArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		public class ListResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}
		public class BuyArguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		public class BuyResponse : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}
	}
}
