using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LO30.Models;
using LO30.Services;
using LO30.Data;
using DDay.iCal;
using DDay.iCal.Serialization;
using DDay.iCal.Serialization.iCalendar;
using System.Text;

namespace LO30.Controllers
{
    public class ScheduleController : Controller
    {
        Lo30Context _context;
        public ScheduleController(Lo30Context context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TeamFeed(int seasonId, int seasonTeamId, bool playoffs)
        {
            var gameTeams = _context.GameTeams
                                      .Include("Game")
                                      .Include("SeasonTeam")
                                      .Include("SeasonTeam.Team")
                                      .Include("SeasonTeam.Season")
                                      .Include("OpponentSeasonTeam")
                                      .Include("OpponentSeasonTeam.Team")
                                      .Include("OpponentSeasonTeam.Season")
                                      .Where(x => x.Game.SeasonId == seasonId && x.Game.Playoffs == playoffs && x.SeasonTeamId == seasonTeamId)
                                      .OrderBy(x => x.SeasonTeam.Team.TeamShortName)
                                      .ToList();

            iCalendar ical = new iCalendar();
            foreach (var gameTeam in gameTeams)
            {
                Event icalEvent = ical.Create<Event>();

                var summary = gameTeam.OpponentSeasonTeam.Team.TeamShortName + " vs " + gameTeam.SeasonTeam.Team.TeamShortName;
                if (gameTeam.HomeTeam)
                {
                    summary = gameTeam.SeasonTeam.Team.TeamShortName + " vs " + gameTeam.OpponentSeasonTeam.Team.TeamShortName;
                }

                icalEvent.Summary = summary;
                icalEvent.Description = summary + " " + gameTeam.Game.Location;

                var year = gameTeam.Game.GameDateTime.Year;
                var mon = gameTeam.Game.GameDateTime.Month;
                var day = gameTeam.Game.GameDateTime.Day;
                var hr = gameTeam.Game.GameDateTime.Hour;
                var min = gameTeam.Game.GameDateTime.Minute;
                var sec = gameTeam.Game.GameDateTime.Second;
                icalEvent.Start = new iCalDateTime(gameTeam.Game.GameDateTime);
                icalEvent.Duration = TimeSpan.FromHours(1.25);
                icalEvent.Location = "Eddie Edgar " + gameTeam.Game.Location;
            }

            ISerializationContext ctx = new SerializationContext();
            ISerializerFactory factory = new SerializerFactory();
            IStringSerializer serializer = factory.Build(ical.GetType(), ctx) as IStringSerializer;

            string output = serializer.SerializeToString(ical);
            var contentType = "text/calendar";

            return Content(output, contentType);

            //var bytes = Encoding.UTF8.GetBytes(output);
            //return File(bytes, contentType, "BillBrown.ics");
        }
    }
}
