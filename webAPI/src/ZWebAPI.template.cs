/*
Copyright (c) 2012, Run With Robots
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the roar.io library nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY RUN WITH ROBOTS ''AS IS'' AND ANY
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL MICHAEL ANDERSON BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System.Collections;
using WebObjects = Roar.WebObjects;


public class ZWebAPI
{
	public IWebAPI iwebapi_;

	public ZWebAPI (IWebAPI iwebapi)
	{
		iwebapi_ = iwebapi;

<% _.each( data.modules, function(m,i,l) {
     print( "\t\t" +m.name + "_ = new " + capitalizeFirst(m.name)+"Actions (iwebapi."+m.name+");\n" );
     } );
%>	}

<% _.each( data.modules, function(m,i,l) {
     print( "\tpublic " + capitalizeFirst(m.name) + "Actions "+m.name+" { get { return "+m.name+"_; } }\n" );
     print( "\tpublic " + capitalizeFirst(m.name)+"Actions " +m.name + "_;\n\n" );
     } );
%>

	public interface Callback<T>
	{
		void OnError( Roar.RequestResult nn );
		void OnSuccess( Roar.CallbackInfo<T> nn );
	}

<%
  _.each( data.modules, function(m,i,l) {
    var class_name = capitalizeFirst(m.name)+"Actions"
%>
	public class <%= class_name %> 
	{
		public IWebAPI.I<%= class_name %> actions_;

		public <%= class_name %> (IWebAPI.I<%= class_name %> actions)
		{
			actions_=actions;
		}

<% _.each( m.functions, function(f,j,ll) {
     var arg = "WebObjects."+capitalizeFirst(m.name)+"."+capitalizeFirst(f.name)+"Arguments"
     var response  = "WebObjects."+capitalizeFirst(m.name)+"."+capitalizeFirst(f.name)+"Response"
     print("\t\tpublic void "+fix_reserved_word(f.name)+" (" + arg +" args, Callback<"+response+"> cb)\n");
     print("\t\t{\n");
     print("\t\t\tactions_."+fix_reserved_word(f.name)+"(\n")
     print("\t\t\t\targs.ToHashtable(),\n")
     print("\t\t\t\tnew CallbackBridge<"+response+">(cb)\n")
     print("\t\t\t\t);\n");
     print("\t\t}\n\n");
} ) %>	}

<% } ) %>

}

