using System;
using NUnit.Framework;
using Roar.WebObjects.User;

namespace Testing
{

	class OnRequestSenderTester : RequestSender
	{
		public OnRequestSenderTester () : base(null, null, new Logger())
		{
		}
		
		public void TestOnServerResponse (string raw, string apicall, IRequestCallback cb)
		{
			OnServerResponse(raw, apicall, cb);
		}
	}
	
	class TestCallback : IRequestCallback
	{
		public Roar.RequestResult info;
		
		public void OnRequest (Roar.RequestResult info)
		{
			this.info = info;
		}
	}
	
	[TestFixture]
	public class XmlError
	{
		[Test()]
		public void XmlNullError ()
		{
			OnRequestSenderTester tester = new OnRequestSenderTester();
			TestCallback cb = new TestCallback();
			tester.TestOnServerResponse(null, "user/login", cb);
			
			Assert.AreEqual(cb.info.code, IWebAPI.INVALID_XML_ERROR);
			Assert.AreEqual(cb.info.msg, "Invalid server response");
		}
		
		[Test()]
		public void XmlIoError ()
		{
			string xml =
			@"<roar>
				<io>
					<retry status=""ok"">Please try again in a few moments.</retry>
				</io>
			</roar>";
			
			OnRequestSenderTester tester = new OnRequestSenderTester();
			TestCallback cb = new TestCallback();
			tester.TestOnServerResponse(xml, "user/login", cb);
			
			Assert.AreEqual(cb.info.code, IWebAPI.IO_ERROR);
			Assert.AreEqual(cb.info.msg, "Please try again in a few moments.");
		}
		
		[Test()]
		public void XmlNotRoarError ()
		{
			string xml = "<nasa>space shuttle</nasa>";
			OnRequestSenderTester tester = new OnRequestSenderTester();
			TestCallback cb = new TestCallback();
			tester.TestOnServerResponse(xml, "user/login", cb);
			
			Assert.AreEqual(cb.info.code, IWebAPI.INVALID_XML_ERROR);
			Assert.AreEqual(cb.info.msg, "Incorrect XML response");
		}
		
		[Test()]
		public void XmlMalformedError ()
		{
			string xml = "<sonda> bak </def>";
			OnRequestSenderTester tester = new OnRequestSenderTester();
			TestCallback cb = new TestCallback();
			tester.TestOnServerResponse(xml, "user/login", cb);
			
			Assert.AreEqual(cb.info.code, IWebAPI.INVALID_XML_ERROR);
			Assert.AreEqual(cb.info.msg.Substring(0, 66), "System.Xml.XmlException: 'sonda' is expected  Line 1, position 16.");
		}
	}
}

