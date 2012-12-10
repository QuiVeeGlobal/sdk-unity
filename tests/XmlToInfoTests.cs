using System;
using NUnit.Framework;
using Roar.WebObjects.Info;

namespace Testing
{

	[TestFixture()]
	public class XmlToInfoTests
	{
	
		[SetUp]
		public void TestInitialise ()
		{
		}
		
		[Test()]
		public void TestPingInfoXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455529480"">
				<info>
					<ping status=""ok"">
						<text>hello</text>
					</ping>
				</info>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml).GetFirstChild("roar");
			
			PingResponse ping_response = (new Roar.DataConversion.Responses.Info.Ping()).Build(nn);
			Assert.AreEqual(ping_response.text, "hello");			
		}
	
		[Test()]
		public void TestPollInfoXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135510457230"">
				<info>
					<poll status=""ok""/>
				</info>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml).GetFirstChild("roar");
			
			PollResponse poll_response = (new Roar.DataConversion.Responses.Info.Poll()).Build(nn);
			
			Assert.IsNotNull( poll_response );
			
		}

	}
	
}

