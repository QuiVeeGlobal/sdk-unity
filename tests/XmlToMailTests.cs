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
	}
}

