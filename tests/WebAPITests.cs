using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using System.Reflection;


[TestFixture()]
public class WebAPITests
{
	private Mockery mockery = null;

	
    [SetUp]
    public void TestInitialise()
    {
        this.mockery = new Mockery();
    }
	
	[Test()]
	public void testNotifyOfServerChanges()
	{
		System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
		System.Xml.XmlElement serverNode = doc.CreateElement("server");
		System.Xml.XmlElement child1 = doc.CreateElement("child1");
		System.Xml.XmlElement child2 = doc.CreateElement("child2");
		
		serverNode.AppendChild(child1);
		serverNode.AppendChild(child2);
		doc.AppendChild(serverNode);
		
		bool called=false;

		Action<object> callback = o => { called=true; Console.WriteLine( o.ToString() ); };

		RoarManager.roarServerAllEvent += callback;
		
		RoarManager.NotifyOfServerChanges( serverNode );

		Assert.IsTrue(called);
		
		this.mockery.VerifyAllExpectationsHaveBeenMet();
	}
}

