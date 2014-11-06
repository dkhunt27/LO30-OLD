﻿using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    public List<GameScore> GetGameScores()
    {
      return _gameScores;
    }

    public List<GameScore> GetGameScoresByGameId(int gameId)
    {
      return _gameScores.Where(x => x.GameTeam.GameId == gameId).ToList();
    }

    public List<GameScore> GetGameScoresByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return _gameScores.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam).ToList();
    }
  }
}