using NUnit.Framework;
using SoccerDataLibrary;
using SoccerDataLibrary.Utils;
using SoccerDataLibrary.Enums;

namespace SportDataLibrary.Test
{
    [TestFixture]
    class TeamTests
    {
        [TestCase]
        public void GetPlayerByJerseyNumberTest()
        {
            Assert.IsNotNull(new League(LeagueNameId.ENGLAND).GetTeamByName("Manchester United FC")["Manchester United FC"].GetPlayerByJerseyNumber(7));
        }


        [TestCase]
        public void GetPlayerByNameTest()
        {
            Assert.IsNotNull(new League(LeagueNameId.ENGLAND).GetTeamByName("Manchester United FC")["Manchester United FC"].GetPlayerByName("Wayne Rooney"));
        }
    }
}
