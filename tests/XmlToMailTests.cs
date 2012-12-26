using System;
using NUnit.Framework;
using Roar.WebObjects.Mail;

namespace Testing
{
	[TestFixture]
	public class XmlToMailTests
	{
		[Test()]
		public void TestAcceptGetXmlAttributes ()
		{
			string xml =
			@"<roar tick=""128555559022"">
				<mail>
					<accept status=""ok""/>
				</mail>
				<!-- Inventory has changed upon receiving this mail -->
				<server>
					<inventory_changed/>
				</server>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Mail.Accept accept_parser = new Roar.DataConversion.Responses.Mail.Accept();
			AcceptResponse response = accept_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestSendGetXmlAttributes ()
		{
			string xml =
			@"<roar tick=""12855555840"">
				<mail>
					<send status=""ok""/>
				</mail>
				<!-- Note any server updates (in this case from gift ""cost"") -->
				<server>
					<update type=""currency"" ikey=""in_game"" value=""49990""/>
				</server>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Mail.Send send_parser = new Roar.DataConversion.Responses.Mail.Send();
			SendResponse response = send_parser.Build(nn);
			Assert.IsNotNull(response);
		}
	}
}

