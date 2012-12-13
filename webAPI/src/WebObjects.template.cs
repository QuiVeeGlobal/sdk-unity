using System;
using System.Collections;
using System.Collections.Generic;


namespace Roar.WebObjects
{
<%
  _.each( data.modules, function(m,i,l) {
    var ns_name = underscoreToCamel(m.name)
%>
	//Namespace for typesafe arguments and responses to Roars <%= m.name%>/foo calls.
	namespace <%= ns_name %>
	{
<% _.each( m.functions, function(f,j,ll) { %>
		// Arguments to <%= m.name %>/<%= f.name %>
		public class <%= underscoreToCamel(f.name) %>Arguments
		{
<% _.each( f.arguments, function(arg, k, lll) {
var argument_type = ("optional" in arg && arg.optional && arg.type=="int")?"int?":arg.type;
%>			public <%= argument_type %> <%= arg.name %>;<% if("note" in arg) { %> // <%= arg.note %> <% } %>
<% } ) %>
			public Hashtable ToHashtable()
			{
				Hashtable retval = new Hashtable();
<% _.each( f.arguments, function(arg, k, lll) {
if( "optional" in arg && arg.optional)
{ %>				if( <%= arg.name %>!=null )
				{
<%}
if( arg.type == "string" )
{
%>				retval["<%= arg.name %>"] = <%= arg.name %>;
<%
} else {
%>				retval["<%= arg.name %>"] = Convert.ToString(<%= arg.name %><%= ("optional" in arg && arg.optional && arg.type=="int")?".Value":"" %>);
<%
}
if( "optional" in arg && arg.optional)
{ %>				}
<%}
} )
%>				return retval;
			}
		}
		
		// Response from <%= m.name %>/<%= f.name %>
		public class <%= underscoreToCamel(f.name) %>Response
		{
<%
  if( "response" in f ) { _.each( f.response.members, function( member, member_index ){
%>			public <%= member.type %> <%= member.name %>;
<% } ) } %>
		}
<% } ) %>
	}
<% } ) %>
	
}

