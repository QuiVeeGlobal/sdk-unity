using System;
using NUnit.Framework;
using Roar.WebObjects.Google;

namespace Testing
{
	[TestFixture]
	public class XmlToGoogleTests
	{
		[Test()]
		public void TestGoogleBindUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<bind_user status=""ok""/>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.BindUser bind_user_parser = new Roar.DataConversion.Responses.Google.BindUser();
			BindUserResponse response = bind_user_parser.Build(nn);
			Assert.IsNotNull(response);
		}
	}
}

