using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Kona.App.Services;
using Kona.Model;
using Kona.ViewModels;
using Kona.Services;
using Kona.Infrastructure;

namespace Kona.Controllers
{
    public class ProductController : Controller
    {

        IStoreService _service;
        ICustomerService _customerService;
        public ProductController(IStoreService service,ICustomerService customerService)
        {
            _service = service;
            _customerService = customerService;
        }

        public ActionResult Search(string q)
        {
            return View(_service.Search(q));
        }
        //
        // GET: /Product/

        public ActionResult Index(int? id)
        {
            //ProductListViewModel model=null;
            ////if an ID is sent in it's a category
            //if (id.HasValue) {
            //    model = _service.GetCategoryModel((int)id);
            //} else {
            //    RedirectToAction("Index", "Home");
            //}
            //return View(model);
            return Content("Success");
        }

        //
        // GET: /Product/Details/5
        [TransactionFilter]
        public ActionResult Details(string id)
        {

            var model = _service.GetDetails(id);
            
            //track an event
            _customerService.TrackProductView(model.SelectedProduct);
            return View(model);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Product/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Product/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
