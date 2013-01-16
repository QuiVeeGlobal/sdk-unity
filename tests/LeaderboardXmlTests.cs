using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using System.Reflection;

namespace Testing
{
	[TestFixture()]
	public class LeaderboardXmlTests
	{
		[Test()]
		public void TestParseLeaderboardListResponse()
		{
			string xml =
			@"<roar tick=""135510457230"">
				<leaderboards>
					<list status=""ok"">
						<board board_id=""4000"" ikey=""premium_currency"" resource_id=""4000"" label=""""/>
						<board board_id=""5001"" ikey=""xp"" resource_id=""5001"" label=""""/>
					</list>
				</leaderboards>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
			Roar.WebObjects.Leaderboards.ListResponse response = (new Roar.DataConversion.Responses.Leaderboards.List()).Build(nn);
			
			Assert.IsNotNull( response );
			Assert.AreEqual( 2, response.boards.Count );
			Assert.AreEqual( "4000", response.boards[0].board_id );
			Assert.AreEqual( "premium_currency", response.boards[0].ikey);
			Assert.AreEqual( "4000", response.boards[0].resource_id);
			Assert.AreEqual( "", response.boards[0].label);
			
			Assert.AreEqual( "5001", response.boards[1].board_id );
			Assert.AreEqual( "xp", response.boards[1].ikey);
			Assert.AreEqual( "5001", response.boards[1].resource_id);
			Assert.AreEqual( "", response.boards[1].label);
		}
		
		[Test()]
		public void TestParseLeaderboardViewResponse()
		{
			string xml = @"<roar tick=""0"">
							<leaderboards>
								<view status=""ok"">
									<ranking ikey=""mojo"" offset=""0"" num_results=""100"" page=""1"" low_is_high=""false"">
										<entry rank=""1"" player_id=""612421456098"" value=""560"">
											<custom>
												<property ikey=""player_name"" value=""Monkey""/>
											</custom>
										</entry>
										<entry rank=""2"" player_id=""195104156933"" value=""514"">
											<custom>
												<property ikey=""player_name"" value=""Dragon""/>
											</custom>
										</entry>
										<entry rank=""3"" player_id=""440312985759"" value=""490"">
											<custom>
												<property ikey=""player_name"" value=""Fun and Awesome DUUUUUDE""/>
											</custom>
										</entry>
									</ranking>
								</view>
							</leaderboards>
						</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
			Roar.WebObjects.Leaderboards.ViewResponse response = (new Roar.DataConversion.Responses.Leaderboards.View()).Build(nn);
			
			Assert.IsNotNull( response );
			Assert.AreEqual( response.leaderboard_data.ikey, "mojo" );
			Assert.AreEqual( response.leaderboard_data.offset, 0 );
			Assert.AreEqual( response.leaderboard_data.num_results, 100 );
			Assert.AreEqual( response.leaderboard_data.page, 1 );
			Assert.AreEqual( response.leaderboard_data.low_is_high, false );

			
		}
	}
}

