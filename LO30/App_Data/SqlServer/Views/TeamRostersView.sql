CREATE VIEW [dbo].[TeamRostersView] AS
SELECT
	tr.SeasonTeamId,
	tr.PlayerId,
	tr.StartYYYYMMDD,
    tr.EndYYYYMMDD,
    tr.Position,
    tr.RatingPrimary,
    tr.RatingSecondary,
    tr.Line,
    tr.PlayerNumber,
	'' as XX,
	t.TeamLongName,
	t.TeamShortName,
	p.FirstName,
	p.LastName,
	s.SeasonName,
	s.IsCurrentSeason
FROM 
	TeamRosters tr
	INNER JOIN SeasonTeams st ON (tr.SeasonTeamId = st.SeasonTeamId)
	INNER JOIN Seasons s ON (st.SeasonId = s.SeasonId)
	INNER JOIN Teams t ON (st.TeamId = t.TeamId)
	INNER JOIN Players p ON (tr.PlayerId = p.PlayerId)


