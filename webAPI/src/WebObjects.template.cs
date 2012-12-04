using System.Collections;


namespace Roar.WebObjects
{
	public interface IResponse
	{
		void ParseXml( IXMLNode nn );
	}

<%
  _.each( data.modules, function(m,i,l) {
    var ns_name = capitalizeFirst(m.name)
%>
	//Namespace for typesafe arguments and responses to Roars <%= m.name%>/foo calls.
	namespace <%= ns_name %>
	{
<% _.each( m.functions, function(f,j,ll) { %>
		// Arguments to <%= m.name %>/<%= f.name %>
		public class <%= capitalizeFirst(f.name) %>Arguments
		{
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
				return retval;
			}
		}
		
		// Response from <%= m.name %>/<%= f.name %>
		public class <%= capitalizeFirst(f.name) %>Response : IResponse
		{
			public void ParseXml( IXMLNode nn )
			{
			}
		}
<% } ) %>
	}
<% } ) %>
	
}

