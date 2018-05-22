﻿using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.BL;
using DI.Reminder.BL.Repository;
using DI.Reminder.BL.Category;
using DI.Reminder.Web.Models;

namespace DI.Reminder.Web.Controllers
{
    public class PromptController : Controller
    {
        private readonly IPrompt prompt;
        IPromptRepository _promptrep;
        IGetCategory _getcategory;
        public IList<Prompt> _promptlist {get;set;}
        public PromptController(IPrompt _prompt, IPromptRepository promptrep, IGetCategory getcategory)
        {
            prompt = _prompt;
            _promptrep = promptrep;
            _getcategory = getcategory;
        }
        // GET: Prompt
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Show(int? id=null)
        {
            Ins ins = new Ins();
            _promptlist = _promptrep.GetCategoryItemsByID(id);
            if (_promptlist == null || id == 0 || _promptlist.Count == 0)
                {
                    ins = new Ins()
                    {
                        CategoryList = _getcategory.Get(id),
                        PromptList = _promptlist
                    };

                }
                else
                {
                    ins = new Ins()
                    {
                        CategoryList = _getcategory.Get(_promptrep.GetID(_promptlist[0].Category)),
                        PromptList = _promptlist
                    };


                }
            if (ins.CategoryList.Count == 0)
                    return RedirectToAction("HttpError404", "Error");
            return View(ins);
            
        }
        public ActionResult PromptDetails(int? ID)
        {
            Prompt prompt = _promptrep.GetPromptDetails(ID);
            if(prompt==null)
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
       
    }
}