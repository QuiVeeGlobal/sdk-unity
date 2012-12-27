using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
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
		
		[Test()]
		public void TestWhatCanIAcceptGetXmlAttributes ()
		{
			string xml =
			@"<roar tick=""128555554651"">
				<mail>
					<what_can_i_accept status=""ok"">
						<package type=""item"" id=""15850999291750564699"" message=""Enjoy the beans!"" sender_id=""123123"" sender_name=""John"">
							<item id=""15850999291750564699"" ikey=""magic_beans"" count=""1"" label=""Magic Beans"" type=""custom_type"" description=""Grow a beanstalk!"" consumable=""true""/>
						</package>
						<package type=""gift"" id=""6760640600796911244"" message=""Have a happy day"" sender_id=""234"" sender_name=""Brenda"">
							<tag value=""test_tag""/>
							<tag value=""next tag""/>
							<modifiers>
								<grant_item ikey=""item_ikey_1""/>
							</modifiers>
						</package>
					</what_can_i_accept>
				</mail>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Mail.WhatCanIAccept what_can_i_accept_parser = new Roar.DataConversion.Responses.Mail.WhatCanIAccept();
			WhatCanIAcceptResponse response = what_can_i_accept_parser.Build(nn);
			
			Assert.AreEqual(response.packages.Count, 2);
			Assert.AreEqual(response.packages[0].id, "15850999291750564699");
			Assert.AreEqual(response.packages[0].type, "item");
			Assert.AreEqual(response.packages[0].sender_id, "123123");
			Assert.AreEqual(response.packages[0].sender_name, "John");
			Assert.AreEqual(response.packages[0].message, "Enjoy the beans!");
			Assert.AreEqual(response.packages[0].items.Count, 1);
			Assert.AreEqual(response.packages[0].tags.Count, 0);
			Assert.AreEqual(response.packages[0].items[0].id, "15850999291750564699");
			Assert.AreEqual(response.packages[0].items[0].ikey, "magic_beans");
			Assert.AreEqual(response.packages[0].items[0].count, 1);
			Assert.AreEqual(response.packages[0].items[0].label, "Magic Beans");
			Assert.AreEqual(response.packages[0].items[0].type, "custom_type");
			Assert.AreEqual(response.packages[0].items[0].description, "Grow a beanstalk!");
			Assert.IsTrue(response.packages[0].items[0].consumable);
			Assert.AreEqual(response.packages[0].modifiers.Count, 0);
			Assert.AreEqual(response.packages[1].id, "6760640600796911244");
			Assert.AreEqual(response.packages[1].type, "gift");
			Assert.AreEqual(response.packages[1].sender_id, "234");
			Assert.AreEqual(response.packages[1].sender_name, "Brenda");
			Assert.AreEqual(response.packages[1].message, "Have a happy day");
			Assert.AreEqual(response.packages[1].items.Count, 0);
			Assert.AreEqual(response.packages[1].tags.Count, 2);
			Assert.AreEqual(response.packages[1].modifiers.Count, 1);
			Assert.AreEqual(response.packages[1].tags[0], "test_tag");
			Assert.AreEqual(response.packages[1].tags[1], "next tag");
			Assert.AreEqual((response.packages[1].modifiers[0] as Roar.DomainObjects.Modifiers.GrantItem).ikey, "item_ikey_1");
		}
		
		[Test()]
		public void TestWhatCanIAcceptParseMechanics ()
		{
			string xml =
			@"<roar tick=""128555554651"">
				<mail>
					<what_can_i_accept status=""ok"">
						<package type=""item"" id=""15850999291750564699"" message=""Enjoy the beans!"" sender_id=""123123"" sender_name=""John"">
							<item id=""15850999291750564699"" ikey=""magic_beans"" count=""1"" label=""Magic Beans"" type=""custom_type"" description=""Grow a beanstalk!"" consumable=""true""/>
							<tag value=""test_tag""/>
							<tag value=""next tag""/>
							<modifiers>
								<grant_item ikey=""item_ikey_1""/>
							</modifiers>
						</package>
					</what_can_i_accept>
				</mail>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Mail.WhatCanIAccept what_can_i_accept_parser = new Roar.DataConversion.Responses.Mail.WhatCanIAccept();
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			what_can_i_accept_parser.ixcrm_parser = ixcrm_parser;
			IList<string> tag_list = new List<string>();
			IList<Roar.DomainObjects.Modifier> modifier_list = new List<Roar.DomainObjects.Modifier>();
			IList<Roar.DomainObjects.ItemStat> item_stat_list = new List<Roar.DomainObjects.ItemStat>();
			IList<Roar.DomainObjects.Modifier> item_price_list = new List<Roar.DomainObjects.Modifier>();
			IList<string> item_tag_list = new List<string>();
			
			Expect.Once.On(ixcrm_parser).Method("ParseItemStatList").With(nn.GetNode("roar>0>mail>0>what_can_i_accept>0>package>0>item>0>stats>0")).Will(Return.Value(item_stat_list));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.GetNode("roar>0>mail>0>what_can_i_accept>0>package>0>item>0>price>0")).Will(Return.Value(item_price_list));
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(nn.GetNode("roar>0>mail>0>what_can_i_accept>0>package>0>item>0>tags>0")).Will(Return.Value(item_tag_list));
			
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(nn.GetNode("roar>0>mail>0>what_can_i_accept>0>package>0")).Will(Return.Value(tag_list));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.GetNode("roar>0>mail>0>what_can_i_accept>0>package>0>modifiers>0")).Will(Return.Value(modifier_list));
			
			WhatCanIAcceptResponse response = what_can_i_accept_parser.Build(nn);
			
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreEqual(response.packages.Count, 1);
			Assert.AreEqual(response.packages[0].id, "15850999291750564699");
			Assert.AreEqual(response.packages[0].type, "item");
			Assert.AreEqual(response.packages[0].sender_id, "123123");
			Assert.AreEqual(response.packages[0].sender_name, "John");
			Assert.AreEqual(response.packages[0].message, "Enjoy the beans!");
			Assert.AreEqual(response.packages[0].items.Count, 1);
			Assert.AreEqual(response.packages[0].tags.Count, 0);
			Assert.AreEqual(response.packages[0].modifiers.Count, 0);
			Assert.AreEqual(response.packages[0].items[0].stats, item_stat_list);
			Assert.AreEqual(response.packages[0].items[0].price, item_price_list);
			Assert.AreEqual(response.packages[0].tags, tag_list);
			Assert.AreEqual(response.packages[0].modifiers, modifier_list);
		}
		
		[Test()]
		public void TestWhatCanISendGetXmlAttributes ()
		{
			string xml =
			@"<roar tick=""12835555872"">
				<mail>
					<what_can_i_send status=""ok"">
						<mailable id=""3467"" type=""gift"" label=""a label"">
							<requirements>
								<friends_requirement required=""5"" ok=""false"" reason=""Insufficient friends""/>
								<level_requirement level=""3"" ok=""true"" reason=""requires level 3""/>
							</requirements>
							<costs>
								<item_cost ikey=""mariner"" number_required=""3"" ok=""false"" reason=""requires mariner(3)""/>
								<stat_cost type=""currency"" ikey=""premium_currency"" value=""477"" ok=""true""/>
							</costs>
							<on_accept>
								<grant_item ikey=""your_gift_item_ikey""/>
							</on_accept>
							<on_give>
								<grant_xp value=""500""/>
							</on_give>
							<tags>
								<tag value=""tag 1""/>
								<tag value=""tag 2""/>
							</tags>
						</mailable>
					</what_can_i_send>
				</mail>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Mail.WhatCanISend what_can_i_send_parser = new Roar.DataConversion.Responses.Mail.WhatCanISend();
			Roar.WebObjects.Mail.WhatCanISendResponse response = what_can_i_send_parser.Build(nn);
			
			Assert.IsNotNull(response.mailables);
			Assert.AreEqual(response.mailables.Count, 1);
			Assert.AreEqual(response.mailables[0].id, "3467");
			Assert.AreEqual(response.mailables[0].type, "gift");
			Assert.AreEqual(response.mailables[0].label, "a label");
			Assert.AreEqual(response.mailables[0].requirements.Count, 2);
			Assert.AreEqual((response.mailables[0].requirements[0] as Roar.DomainObjects.Requirements.Friends).required, 5);
			Assert.AreEqual((response.mailables[0].requirements[0] as Roar.DomainObjects.Requirements.Friends).reason, "Insufficient friends");
			Assert.IsFalse((response.mailables[0].requirements[0] as Roar.DomainObjects.Requirements.Friends).ok);
			Assert.AreEqual((response.mailables[0].requirements[1] as Roar.DomainObjects.Requirements.Level).level, 3);
			Assert.AreEqual((response.mailables[0].requirements[1] as Roar.DomainObjects.Requirements.Level).reason, "requires level 3");
			Assert.IsTrue((response.mailables[0].requirements[1] as Roar.DomainObjects.Requirements.Level).ok);
			Assert.AreEqual(response.mailables[0].costs.Count, 2);
			Assert.AreEqual((response.mailables[0].costs[0] as Roar.DomainObjects.Costs.Item).ikey, "mariner");
			Assert.AreEqual((response.mailables[0].costs[0] as Roar.DomainObjects.Costs.Item).number_required, 3);
			Assert.AreEqual((response.mailables[0].costs[0] as Roar.DomainObjects.Costs.Item).reason, "requires mariner(3)");
			Assert.IsFalse((response.mailables[0].costs[0] as Roar.DomainObjects.Costs.Item).ok);
			Assert.AreEqual((response.mailables[0].costs[1] as Roar.DomainObjects.Costs.Stat).ikey, "premium_currency");
			Assert.AreEqual((response.mailables[0].costs[1] as Roar.DomainObjects.Costs.Stat).type, "currency");
			Assert.AreEqual((response.mailables[0].costs[1] as Roar.DomainObjects.Costs.Stat).value, 477);
			Assert.IsTrue((response.mailables[0].costs[1] as Roar.DomainObjects.Costs.Stat).ok);
			Assert.AreEqual(response.mailables[0].on_accept.Count, 1);
			Assert.AreEqual((response.mailables[0].on_accept[0] as Roar.DomainObjects.Modifiers.GrantItem).ikey, "your_gift_item_ikey");
			Assert.AreEqual(response.mailables[0].on_give.Count, 1);
			Assert.AreEqual((response.mailables[0].on_give[0] as Roar.DomainObjects.Modifiers.GrantXp).value, 500);
			Assert.AreEqual(response.mailables[0].tags.Count, 2);
			Assert.AreEqual(response.mailables[0].tags[0], "tag 1");
			Assert.AreEqual(response.mailables[0].tags[1], "tag 2");
		}
		
		[Test()]
		public void TestWhatCanISendParseMechanics ()
		{
			string xml =
			@"<roar tick=""12835555872"">
				<mail>
					<what_can_i_send status=""ok"">
						<mailable id=""3467"" type=""gift"" label=""a label"">
							<requirements>
								<friends_requirement required=""5"" ok=""false"" reason=""Insufficient friends""/>
								<level_requirement level=""3"" ok=""true"" reason=""requires level 3""/>
							</requirements>
							<costs>
								<item_cost ikey=""mariner"" number_required=""3"" ok=""false"" reason=""requires mariner(3)""/>
								<stat_cost type=""currency"" ikey=""premium_currency"" value=""477"" ok=""true""/>
							</costs>
							<on_accept>
								<grant_item ikey=""your_gift_item_ikey""/>
							</on_accept>
							<on_give>
								<grant_xp value=""500""/>
							</on_give>
							<tags>
								<tag value=""tag 1""/>
								<tag value=""tag 2""/>
							</tags>
						</mailable>
					</what_can_i_send>
				</mail>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Mail.WhatCanISend what_can_i_send_parser = new Roar.DataConversion.Responses.Mail.WhatCanISend();
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			what_can_i_send_parser.ixcrm_parser = ixcrm_parser;
			
			IList<Roar.DomainObjects.Requirement> requirement_list = new List<Roar.DomainObjects.Requirement>();
			IList<Roar.DomainObjects.Cost> cost_list = new List<Roar.DomainObjects.Cost>();
			IList<Roar.DomainObjects.Modifier> accept_list = new List<Roar.DomainObjects.Modifier>();
			IList<Roar.DomainObjects.Modifier> give_list = new List<Roar.DomainObjects.Modifier>();
			IList<string> tag_list = new List<string>();
			
			Expect.Once.On(ixcrm_parser).Method("ParseRequirementList").With(nn.GetNode("roar>0>mail>0>what_can_i_send>0>mailable>0>requirements>0")).Will(Return.Value(requirement_list));
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With(nn.GetNode("roar>0>mail>0>what_can_i_send>0>mailable>0>costs>0")).Will(Return.Value(cost_list));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.GetNode("roar>0>mail>0>what_can_i_send>0>mailable>0>on_accept>0")).Will(Return.Value(accept_list));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.GetNode("roar>0>mail>0>what_can_i_send>0>mailable>0>on_give>0")).Will(Return.Value(give_list));
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(nn.GetNode("roar>0>mail>0>what_can_i_send>0>mailable>0>tags>0")).Will(Return.Value(tag_list));
			
			Roar.WebObjects.Mail.WhatCanISendResponse response = what_can_i_send_parser.Build(nn);
			
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.IsNotNull(response.mailables);
			Assert.AreEqual(response.mailables.Count, 1);
			Assert.AreEqual(response.mailables[0].id, "3467");
			Assert.AreEqual(response.mailables[0].type, "gift");
			Assert.AreEqual(response.mailables[0].label, "a label");
			Assert.AreEqual(response.mailables[0].requirements, requirement_list);
			Assert.AreEqual(response.mailables[0].costs, cost_list);
			Assert.AreEqual(response.mailables[0].on_accept, accept_list);
			Assert.AreEqual(response.mailables[0].on_give, give_list);
			Assert.AreEqual(response.mailables[0].tags, tag_list);
		}
	}
}

