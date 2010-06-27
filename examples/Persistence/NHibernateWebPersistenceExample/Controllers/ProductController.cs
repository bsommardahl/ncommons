using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NCommons.Persistence;
using NHibernateWebPersistenceExample.Models;

namespace NHibernateWebPersistenceExample.Controllers
{
    public class ProductController : Controller
    {
        readonly IDatabaseContext _databaseContext;
        readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository, IDatabaseContext databaseContext)
        {
            _productRepository = productRepository;
            _databaseContext = databaseContext;
        }

        public ActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = _productRepository.Get(id);
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var product = new Product
                                  {
                                      Description = collection["Description"],
                                      Price = Double.Parse(collection["Price"])
                                  };

                _productRepository.Save(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Product product = _productRepository.Get(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Product product = _productRepository.Get(id);
                product.Description = collection["Description"];
                product.Price = Double.Parse(collection["Price"]);
                _productRepository.Save(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}