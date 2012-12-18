using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using Roar.WebObjects.Tasks;

namespace Testing
{
	[TestFixture]
	public class XmlToTasksTest
	{
	
		[Test()]
		public void TestTasksListXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""135577684654"">
				<tasks>
					<list status=""ok"">
						<task ikey=""dungeon_crawl"">
							<label>Dungeon crawl</label>
							<description>Go into the pits</description>
							<location>Australia</location>
							<tags>
								<tag value=""protect""/>
								<tag value=""monsters""/>
							</tags>
							<costs>
								<item_cost ikey=""mariner"" number_required=""3"" ok=""false"" reason=""requires mariner(3)""/>
								<stat_cost type=""currency"" ikey=""premium_currency"" value=""477"" ok=""true""/>
							</costs>
							<rewards>
								<grant_stat type=""currency"" ikey=""premium_currency"" value=""453""/>
								<grant_xp value=""234""/>
							</rewards>
							<mastery level=""2"" progress=""5""/>
							<requires>
								<item_requirement ikey=""talisman"" number_required=""2"" ok=""false"" reason=""requires talisman(2)""/>
							</requires>
						</task>
						<task ikey=""journey"">
							<label>Journey</label>
							<description>Travel there and back.</description>
							<location>Australia</location>
							<tags/>
							<mastery level=""3"" progress=""6""/>
						</task>
					</list>
				</tasks>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Roar.DataConversion.Responses.Tasks.List list_parser = new Roar.DataConversion.Responses.Tasks.List();
			ListResponse response = list_parser.Build(nn);
			
			Assert.IsNotNull(response);
			Assert.IsNotNull(response.tasks);
			Assert.AreEqual(response.tasks.Count, 2);
			
			Assert.IsNotNull(response.tasks[0]);
			Assert.AreEqual(response.tasks[0].ikey, "dungeon_crawl");
			Assert.AreEqual(response.tasks[0].label, "Dungeon crawl");
			Assert.AreEqual(response.tasks[0].description, "Go into the pits");
			Assert.AreEqual(response.tasks[0].location, "Australia");
			Assert.AreEqual(response.tasks[0].costs.Count, 2);
			Assert.AreEqual((response.tasks[0].costs[0] as Roar.DomainObjects.Costs.Item).reason, "requires mariner(3)");
			Assert.AreEqual(response.tasks[0].rewards.Count, 2);
			Assert.AreEqual((response.tasks[0].rewards[1] as Roar.DomainObjects.Modifiers.GrantXp).value, 234);
			Assert.AreEqual(response.tasks[0].requirements.Count, 1);
			Assert.AreEqual((response.tasks[0].requirements[0] as Roar.DomainObjects.Requirements.Item).reason, "requires talisman(2)");
			Assert.AreEqual(response.tasks[0].tags.Count, 2);
			Assert.AreEqual(response.tasks[0].tags[0], "protect");
			Assert.AreEqual(response.tasks[0].tags[1], "monsters");
			Assert.AreEqual(response.tasks[0].mastery_level, 2);
			Assert.AreEqual(response.tasks[0].mastery_progress, 5);
			
			Assert.IsNotNull(response.tasks[1]);
			Assert.AreEqual(response.tasks[1].costs.Count, 0);
			Assert.AreEqual(response.tasks[1].rewards.Count, 0);
			Assert.AreEqual(response.tasks[1].requirements.Count, 0);
			Assert.AreEqual(response.tasks[1].tags.Count, 0);
		}
		
		[Test()]
		public void TestTasksListParseMechanics()
		{
			string xml =
			@"<roar tick=""135577684654"">
				<tasks>
					<list status=""ok"">
						<task ikey=""dungeon_crawl"">
							<label>Dungeon crawl</label>
							<description>Go into the pits</description>
							<location>Australia</location>
							<tags>
								<tag value=""protect""/>
								<tag value=""monsters""/>
							</tags>
							<costs>
								<item_cost ikey=""mariner"" number_required=""3"" ok=""false"" reason=""requires mariner(3)""/>
								<stat_cost type=""currency"" ikey=""premium_currency"" value=""477"" ok=""true""/>
							</costs>
							<rewards>
								<grant_stat type=""currency"" ikey=""premium_currency"" value=""453""/>
								<grant_xp value=""234""/>
							</rewards>
							<mastery level=""2"" progress=""5""/>
							<requires>
								<item_requirement ikey=""talisman"" number_required=""2"" ok=""false"" reason=""requires talisman(2)""/>
							</requires>
						</task>
					</list>
				</tasks>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			Roar.DataConversion.Responses.Tasks.List list_parser = new Roar.DataConversion.Responses.Tasks.List();
			list_parser.ixcrm_parser = ixcrm_parser;
			Roar.DomainObjects.Task task_data = new Roar.DomainObjects.Task();
			
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With(nn.GetNode("roar>0>tasks>0>list>0>task>0>costs>0")).Will(Return.Value(task_data.costs));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.GetNode("roar>0>tasks>0>list>0>task>0>rewards>0")).Will(Return.Value(task_data.rewards));
			Expect.Once.On(ixcrm_parser).Method("ParseRequirementList").With(nn.GetNode("roar>0>tasks>0>list>0>task>0>requires>0")).Will(Return.Value(task_data.requirements));
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(nn.GetNode("roar>0>tasks>0>list>0>task>0>tags>0")).Will(Return.Value(task_data.tags));
			
			ListResponse response = list_parser.Build(nn);
			
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreEqual(response.tasks.Count, 1);
			Assert.AreEqual(response.tasks[0].costs.Count, 0);
			Assert.AreEqual(response.tasks[0].rewards.Count, 0);
			Assert.AreEqual(response.tasks[0].requirements.Count, 0);
			Assert.AreEqual(response.tasks[0].tags.Count, 0);
			Assert.AreEqual(response.tasks[0].costs, task_data.costs);
			Assert.AreEqual(response.tasks[0].rewards, task_data.rewards);
			Assert.AreEqual(response.tasks[0].requirements, task_data.requirements);
			Assert.AreEqual(response.tasks[0].tags, task_data.tags);
		}
		
		[Test()]
		public void TestTasksStartXmlGetAttributes()
		{
			string xml =
			@"<roar tick=""128555552127"">
				<tasks>
					<start status=""ok""/>
				</tasks>
				<server>
					<task_complete>
						<ikey>task_ikey</ikey>
						<label>Task label</label>
						<description>Task description</description>
						<location/>
						<tags>comma,separated,tags</tags>
						<costs>
							<stat_change ikey=""energy"" value=""10""/>
						</costs>
						<modifiers>
							<stat_change ikey=""xp"" value=""20""/>
						</modifiers>
						<mastery level=""3"" progress=""100""/>
					</task_complete>
					<update type=""resource"" ikey=""energy"" value=""20""/>
					<update type=""xp"" ikey=""xp"" value=""20""/>
				</server>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Tasks.Start start_parser = new Roar.DataConversion.Responses.Tasks.Start();
			StartResponse response = start_parser.Build(nn);
			Assert.IsNotNull(response);
		}
	}
}

