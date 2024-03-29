﻿using LO30.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Lo30Context : DbContext
  {
    public Lo30Context()
      : base("LO30ReportingDB")
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;

      //Database.SetInitializer(new LO30ContextSeedInitializer());
      Database.SetInitializer(new MigrateDatabaseToLatestVersion<Lo30Context, Lo30MigrationsConfiguration>());
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      // TODO use to convert TimeRemaining on Processed tables to decimal so they will sort correctly
      //modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(4, 2));
      modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<DataProcessing> DataProcessing { get; set; }
    public DbSet<Division> Divisions { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<EmailType> EmailTypes { get; set; }

    public DbSet<ForWebGoalieStat> ForWebGoalieStats { get; set; }
    public DbSet<ForWebPlayerStat> ForWebPlayerStats { get; set; }
    public DbSet<ForWebTeamStanding> ForWebTeamStandings { get; set; }

    public DbSet<Game> Games { get; set; }
    public DbSet<GameOutcome> GameOutcomes { get; set; }
    public DbSet<GameRoster> GameRosters { get; set; }
    public DbSet<GameScore> GameScores { get; set; }
    public DbSet<GameTeam> GameTeams { get; set; }

    public DbSet<GoalieStatGame> GoalieStatsGame { get; set; }
    public DbSet<GoalieStatSeason> GoalieStatsSeason { get; set; }
    public DbSet<GoalieStatSeasonTeam> GoalieStatsSeasonTeam { get; set; }

    public DbSet<Penalty> Penalties { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<PhoneType> PhoneTypes { get; set; }

    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerDraft> PlayerDrafts { get; set; }
    public DbSet<PlayerEmail> PlayerEmails { get; set; }
    public DbSet<PlayerPhone> PlayerPhones { get; set; }
    public DbSet<PlayerRating> PlayerRatings { get; set; }

    public DbSet<PlayerStatCareer> PlayerStatsCareer { get; set; }
    public DbSet<PlayerStatGame> PlayerStatsGame { get; set; }
    public DbSet<PlayerStatSeason> PlayerStatsSeason { get; set; }
    public DbSet<PlayerStatSeasonTeam> PlayerStatsSeasonTeam { get; set; }

    public DbSet<PlayerStatus> PlayerStatuses { get; set; }
    public DbSet<PlayerStatusType> PlayerStatusTypes { get; set; }
    
    public DbSet<ScoreSheetEntry> ScoreSheetEntries { get; set; }
    public DbSet<ScoreSheetEntryProcessed> ScoreSheetEntriesProcessed { get; set; }
    public DbSet<ScoreSheetEntryPenalty> ScoreSheetEntryPenalties { get; set; }
    public DbSet<ScoreSheetEntryPenaltyProcessed> ScoreSheetEntryPenaltiesProcessed { get; set; }
    public DbSet<ScoreSheetEntrySub> ScoreSheetEntrySubs { get; set; }
    public DbSet<ScoreSheetEntrySubProcessed> ScoreSheetEntrySubsProcessed { get; set; }

    public DbSet<Season> Seasons { get; set; }
    public DbSet<SeasonTeam> SeasonTeams { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Sponsor> Sponsors { get; set; }
    public DbSet<SponsorEmail> SponsorEmails { get; set; }
    public DbSet<SponsorPhone> SponsorPhones { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamRoster> TeamRosters { get; set; }
    public DbSet<TeamStanding> TeamStandings { get; set; }

    public DbSet<Article> Articles { get; set; }
  }
}