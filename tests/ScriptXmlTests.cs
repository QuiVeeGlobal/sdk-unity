using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using System.Reflection;

namespace Testing
{
    [TestFixture()]
    public class ScriptXmlTests
    {
        [Test()]
        public void TestScriptRun()
        {
            string xml =
            @"<roar tick=""135537804720"">
			  <scripts>
			    <run status=""ok"">
			      <result b=""7"">
					<a>
					<value>2</value>
					<value>3</value>
					</a>
					</result>
			    </run>
			  </scripts>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Scripts.RunAdminResponse response = (new Roar.DataConversion.Responses.Scripts.RunAdmin()).Build(nn);

            Assert.IsNotNull(response);

        }

    

        [Test()]
        public void TestScriptRunAdmin()
        {
            string xml =
            @"<roar tick=""135537804720"">
			  <scripts>
			    <run status=""ok"">
			      <result b=""7"">
					<a>
					<value>2</value>
					<value>3</value>
					</a>
					</result>
			    </run>
			  </scripts>
			</roar>";

            IXMLNode nn = (new XMLNode.XMLParser()).Parse(xml);

            Roar.WebObjects.Scripts.RunAdminResponse response = (new Roar.DataConversion.Responses.Scripts.RunAdmin()).Build(nn);

            Assert.IsNotNull(response);
			Assert.IsNotNull( response.result.resultNode);

        }

    }

}