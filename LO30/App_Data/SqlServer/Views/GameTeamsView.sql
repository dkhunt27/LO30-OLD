CREATE VIEW [dbo].[GameTeamsView] AS
SELECT 
	gt.GameTeamId,
  gt.GameId,
  gt.HomeTeam,
  gt.SeasonTeamId,
	g.SeasonId,
	g.GameDateTime,
	g.GameYYYYMMDD,
	g.Location,
	g.Playoff,
	s.SeasonName,
	s.IsCurrentSeason,
	s.StartYYYYMMDD,
	s.EndYYYYMMDD,
	t.TeamId,
	t.TeamLongName,
	t.TeamShortName
FROM 
	GameTeams gt
	INNER JOIN Games g on (gt.GameId = g.GameId)
	INNER JOIN Seasons s on (g.SeasonId = s.SeasonId)
	INNER JOIN SeasonTeams st on (gt.SeasonTeamId = st.SeasonTeamId)
	INNER JOIN Teams t on (st.TeamId = t.TeamId)



