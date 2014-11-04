using LO30.Data;
using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LO30.Controllers
{
  public class AdminSettingsController : Controller
  {
    private ILo30Repository _repo;
    private Lo30ContextService _lo30ContextService;

    public AdminSettingsController(ILo30Repository repo, Lo30ContextService contextService)
    {
      _repo = repo;
      _lo30ContextService = contextService;
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(Setting settingToCreate)
    {
      try
      {
        if (!ModelState.IsValid) return View();

        _lo30ContextService.SaveOrUpdateSetting(settingToCreate);

        return RedirectToAction("List");
      }
      catch (Exception ex)
      {
        ViewBag.ErrorMessage = "Unable to perform action.  Exception:" + ex.Message;
        return View(settingToCreate);
      }
    }

    [Authorize]
    public ActionResult Delete(int id, bool? exception, string exceptionMessage)
    {
      if (exception.HasValue)
      {
        ViewBag.ErrorMessage = "Unable to perform action.  Exception:" + exceptionMessage;
      }
      var setting = _repo.GetSettingBySettingId(id);
      return View(setting);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      try
      {
        var deletedSetting = _repo.DeleteSettingBySettingId(id);

        return RedirectToAction("List");
      }
      catch (Exception ex)
      {
        return RedirectToAction("Delete",
           new System.Web.Routing.RouteValueDictionary {
                                      { "id", id },
                                      { "exception", true },
                                      { "exceptionMessage", ex.Message}
                                  });
      }
    }

    [Authorize]
    public ActionResult Details(int id)
    {
      var setting = _repo.GetSettingBySettingId(id);
      return View(setting);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var setting = _repo.GetSettingBySettingId(id);
      return View(setting);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit(Setting settingToEdit)
    {
      try
      {
        var originalSetting = _repo.GetSettingBySettingId(settingToEdit.SettingId);

        if (!ModelState.IsValid) return View(originalSetting);

        _lo30ContextService.SaveOrUpdateSetting(settingToEdit);

        return RedirectToAction("List");
      }
      catch (Exception ex)
      {
        ViewBag.ErrorMessage = "Unable to perform action.  Exception:" + ex.Message;
        return View(settingToEdit);
      }

    }

    [Authorize]
    public ActionResult List()
    {
      var settings = _repo.GetSettings();
      return View(settings);
    }

  }
}
