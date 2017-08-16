﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class DynamicUIController : Controller
    {
        // GET: DynamicUI
        private IDynamicUIBusiness _dynamicUIBusiness;
        public DynamicUIController(IDynamicUIBusiness dynamicUIBusiness )
        {
            _dynamicUIBusiness = dynamicUIBusiness;
           
        }

        public ActionResult _MenuNavBar()
        {
            List<Menu> menulist = _dynamicUIBusiness.GetAllMenues();
            DynamicUIViewModel dUIObj = new DynamicUIViewModel();
            dUIObj.MenuViewModelList = Mapper.Map<List<Menu>, List<MenuViewModel>>(menulist);
            return View(dUIObj);
        }


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UnderConstruction() {
            return View();
        }
    }
}