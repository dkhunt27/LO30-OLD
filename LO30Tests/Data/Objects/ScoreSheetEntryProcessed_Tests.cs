using LO30.Data.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Services.Tests
{
  [TestClass()]
  public class ScoreSheetEntryProcessed_Tests
  {
    [TestInitialize]
    public void Before()
    {
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_Unassisted()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_KnownA1()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_KnownA1_KnownA2()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 3, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_KnownA1_KnownA2_KnownA3()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 3, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_UnknownA1()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 0, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_UnknownA1_KnownA2()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 0, a2pid: 3, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_UnknownA1_KnownA2_KnownA3()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 0, a2pid: 3, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_UnknownA1_UnknownA2()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 0, a2pid: 0, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_UnknownA1_UnknownA2_KnownA3()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 0, a2pid: 0, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_KnownGoal_UnknownA1_UnknownA2_UnknownA3()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 0, a2pid: 0, a3pid: 0, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_Unassisted()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_KnownA1()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 2, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_KnownA1_KnownA2()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 2, a2pid: 3, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_KnownA1_KnownA2_KnownA3()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 2, a2pid: 3, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_UnknownA1()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 0, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_UnknownA1_KnownA2()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 0, a2pid: 3, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_UnknownA1_KnownA2_KnownA3()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 0, a2pid: 3, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_UnknownA1_UnknownA2()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 0, a2pid: 0, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_UnknownA1_UnknownA2_KnownA3()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 0, a2pid: 0, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_UnknownGoal_UnknownA1_UnknownA2_UnknownA3()
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 0, a1pid: 0, a2pid: 0, a3pid: 0, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
      Assert.IsTrue(true);
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_SHGAndPPG()
    {
      string expectedExMsg = "ShortHandedGoal and PowerPlayGoal both cannot be true";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 3, a3pid: 4, shg: true, ppg: true, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_PeriodLessThan1()
    {
      string expectedExMsg = "Period cannot be less than 1";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 0, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 3, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_PeriodMoreThan4()
    {
      string expectedExMsg = "Period cannot be more than 4";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 5, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 3, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_GoalAssist1SamePersion()
    {
      string expectedExMsg = "GoalPlayerId cannot also be an Assist#PlayerId";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 1, a2pid: 3, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_GoalAssist2SamePersion()
    {
      string expectedExMsg = "GoalPlayerId cannot also be an Assist#PlayerId";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 1, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_GoalAssist3SamePersion()
    {
      string expectedExMsg = "GoalPlayerId cannot also be an Assist#PlayerId";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 3, a3pid: 1, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_Assist1And2SamePersion()
    {
      string expectedExMsg = "Assist1PlayerId cannot also be an Assist#PlayerId";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 2, a3pid: 4, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_Assist1And3SamePersion()
    {
      string expectedExMsg = "Assist1PlayerId cannot also be an Assist#PlayerId";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 4, a3pid: 2, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }

    [TestMethod()]
    public void ScoreSheetEntryProcessed_ErrorIf_Assist2And3SamePersion()
    {
      string expectedExMsg = "Assist2PlayerId cannot also be an Assist#PlayerId";
      try
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(sseid: 1, gid: 1, per: 1, ht: true, time: "11.11", gpid: 1, a1pid: 2, a2pid: 3, a3pid: 3, shg: false, ppg: false, gwg: false, upd: DateTime.Now);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }
  }
}
