CREATE VIEW [dbo].[GamesView] AS
select 
	g.GameId,
	g.SeasonId,
	g.GameDateTime,
	g.GameYYYYMMDD,
	g.Location,
	g.Playoff,
	s.SeasonName,
	s.IsCurrentSeason,
	s.StartYYYYMMDD,
	s.EndYYYYMMDD
from
	Games g
	INNER JOIN Seasons s on (g.SeasonId = s.SeasonId)
