using System;
using NUnit.Framework;
using Roar.WebObjects.User;

namespace Testing
{
	[TestFixture]
	public class XmlToUserTests
	{
	
		[Test()]
		public void TestUserCreateXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<user>
					<create status=""ok""/>
				</user>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.User.Create create_parser = new Roar.DataConversion.Responses.User.Create();
			CreateResponse response = create_parser.Build(nn);
			Assert.IsNotNull(response);
		}
	}
}

