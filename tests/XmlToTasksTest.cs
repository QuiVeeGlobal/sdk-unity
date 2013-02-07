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
								<random_choice>
									<choice weight=""1"">
										<modifier>
											<grant_stat type=""currency"" ikey=""dragon_points"" value=""25""/>
										</modifier>
									</choice>
									<choice weight=""99"">
										<modifier>
											<grant_stat_range type=""currency"" ikey=""dragon_points"" min=""3"" max=""6""/>
										</modifier>
									</choice>
								</random_choice>
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
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
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
			Assert.AreEqual(response.tasks[0].rewards.Count, 3);
			Assert.AreEqual((response.tasks[0].rewards[1] as Roar.DomainObjects.Modifiers.GrantXp).value, 234);
			Assert.AreEqual(response.tasks[0].requirements.Count, 1);
			Assert.AreEqual((response.tasks[0].requirements[0] as Roar.DomainObjects.Requirements.Item).reason, "requires talisman(2)");
			Assert.AreEqual(response.tasks[0].tags.Count, 2);
			Assert.AreEqual(response.tasks[0].tags[0], "protect");
			Assert.AreEqual(response.tasks[0].tags[1], "monsters");
			Assert.AreEqual((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices.Count, 2);
			Assert.AreEqual((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices[0].weight, 1);
			Assert.AreEqual((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices[0].modifiers.Count, 1);
			Assert.AreEqual(((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices[0].modifiers[0] as Roar.DomainObjects.Modifiers.GrantStat).ikey, "dragon_points");
			Assert.AreEqual(((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices[0].modifiers[0] as Roar.DomainObjects.Modifiers.GrantStat).value, 25);
			Assert.AreEqual((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices[0].requirements.Count, 0);
			Assert.AreEqual((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices[1].weight, 99);
			Assert.AreEqual((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices[1].modifiers.Count, 1);
			Assert.AreEqual((response.tasks[0].rewards[2] as Roar.DomainObjects.Modifiers.RandomChoice).choices[1].requirements.Count, 0);
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
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			Roar.DataConversion.Responses.Tasks.List list_parser = new Roar.DataConversion.Responses.Tasks.List();
			list_parser.ixcrm_parser = ixcrm_parser;
			Roar.DomainObjects.Task task_data = new Roar.DomainObjects.Task();
			
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With(nn.SelectSingleNode("./tasks/list/task/costs")).Will(Return.Value(task_data.costs));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.SelectSingleNode("./tasks/list/task/rewards")).Will(Return.Value(task_data.rewards));
			Expect.Once.On(ixcrm_parser).Method("ParseRequirementList").With(nn.SelectSingleNode("./tasks/list/task/requires")).Will(Return.Value(task_data.requirements));
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(nn.SelectSingleNode("./tasks/list/task/tags")).Will(Return.Value(task_data.tags));
			
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
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.Tasks.Start start_parser = new Roar.DataConversion.Responses.Tasks.Start();
			StartResponse response = start_parser.Build(nn);
			Assert.IsNotNull(response);
		}
	}
}

