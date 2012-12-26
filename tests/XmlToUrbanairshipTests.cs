using System;
using NUnit.Framework;
using Roar.WebObjects.Urbanairship;

namespace Testing
{
	[TestFixture]
	public class XmlToUrbanairshipTests
	{
		[Test()]
		public void TestUrbanairshipIOSRegisterXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<urbanairship>
					<ios_register status=""ok"" />
				</urbanairship>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Urbanairship.IosRegister ios_register_parser = new Roar.DataConversion.Responses.Urbanairship.IosRegister();
			IosRegisterResponse response = ios_register_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestUrbanairshipPushXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<urbanairship>
					<ios_register status=""ok"" />
				</urbanairship>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Urbanairship.Push push_parser = new Roar.DataConversion.Responses.Urbanairship.Push();
			PushResponse response = push_parser.Build(nn);
			Assert.IsNotNull(response);
		}
	}
}

