using System;
using NUnit.Framework;
using Roar;

namespace Testing
{
	[TestFixture]
	public class JsonConversionTests
	{
		[Test()]
		public void StringToJSONTests ()
		{
			Assert.AreEqual(Json.StringToJSON("sonda"), "\"sonda\"");
			Assert.AreEqual(Json.StringToJSON("Computer says: \"yes\"."), "\"Computer says: \\\"yes\\\".\"");
			Assert.AreEqual(Json.StringToJSON("Backslash (\\) can be escaped (\\\\)."), "\"Backslash (\\\\) can be escaped (\\\\\\\\).\"");
			Assert.AreEqual(Json.StringToJSON("Łódź"), "\"Łódź\"");
			Assert.AreEqual(Json.StringToJSON("\a\v"), "\"\\u0007\\u000B\"");
			Assert.AreEqual(Json.StringToJSON("Backspace [\b]"), "\"Backspace [\\b]\"");
			Assert.AreEqual(Json.StringToJSON("Formfeed [\f]"), "\"Formfeed [\\f]\"");
			Assert.AreEqual(Json.StringToJSON("New Line [\n]"), "\"New Line [\\n]\"");
			Assert.AreEqual(Json.StringToJSON("Carriage Return [\r]"), "\"Carriage Return [\\r]\"");
			Assert.AreEqual(Json.StringToJSON("Tab [\t]"), "\"Tab [\\t]\"");
		}
	}
}

