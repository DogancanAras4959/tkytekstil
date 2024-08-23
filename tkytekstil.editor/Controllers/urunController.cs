using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tkytekstil.COMMON.Helpers;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Dtos.BrandData;
using tkytekstil.ENGINE.Dtos.CategoryData;
using tkytekstil.ENGINE.Dtos.ColorData;
using tkytekstil.ENGINE.Dtos.ColorProductData;
using tkytekstil.ENGINE.Dtos.ImagesProductData;
using tkytekstil.ENGINE.Dtos.ProductData;
using tkytekstil.ENGINE.Dtos.SizeData;
using tkytekstil.ENGINE.Dtos.SizeNumProductData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.editor.Controllers
{
    public class urunController : Controller
    {

        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IColorService _colorService;
        private readonly ISizeNumService _sizeNumService;
        private readonly IProductService _productService;
        private readonly IImageProductService _imageProductService;
        private readonly IColorProductService _colorProductService;
        private readonly ISizeNumberProductService _sizeNumberProductService;
        public urunController(ICategoryService categoryService, IBrandService brandService, IColorService colorService, ISizeNumService sizeNumService, IProductService productService, IImageProductService imageProductService, IColorProductService colorProductService, ISizeNumberProductService sizeNumberProductService)
        {
            _categoryService = categoryService;
            _brandService = brandService;
            _colorService = colorService;
            _sizeNumService = sizeNumService;
            _productService = productService;
            _imageProductService = imageProductService;
            _colorProductService = colorProductService;
            _sizeNumberProductService = sizeNumberProductService;
        }

        #endregion

        #region Kategoriler

        [HttpGet]
        public IActionResult kategoriler()
        {
            #region ViewBag

            ViewBag.pTitle = "Kategoriler";
            ViewBag.pageTitle = "Ürün Yönetimi";
            ViewBag.Title = "Kategoriler";

            #endregion

            return View(_categoryService.GetAll());
        }

        [HttpGet]
        public IActionResult kategoriekle()
        {
            #region ViewBag

            ViewBag.pTitle = "Kategori Ekle";
            ViewBag.pageTitle = "Ürün Yönetimi";
            ViewBag.Title = "Kategori Ekle";

            #endregion

            return View();
        }

        [HttpPost]
        public IActionResult kategoriolustur(CategoriesDto model, IFormFile file)
        {
            try
            {
                if (file != null)
                    model.CategoryImage = SaveImageProcess.ImageInsert(file, "Admin");

                _categoryService.Insert(model);
                return RedirectToAction("kategoriler", "urun");
            }

            catch (Exception ex)
            {
                return RedirectToAction("kategoriekle", "urun");
            }
        }

        [HttpGet]
        public IActionResult kategoriduzenle(int Id)
        {
            var categories = _categoryService.Get(Id);
            return View(categories);
        }

        [HttpPost]
        public IActionResult kategoriguncelle(CategoriesDto model, IFormFile file)
        {
            try
            {
                var category = _categoryService.Get(model.ID);

                if (file != null)
                    category.CategoryImage = SaveImageProcess.ImageInsert(file, "Admin");

                category.CategoryName = model.CategoryName;
                category.UpdatedTime = DateTime.Now;
                category.IsActive = model.IsActive;
                category.CategoryDescription = model.CategoryDescription;

                _categoryService.Update(category);
                return RedirectToAction("kategoriler", "urun");
            }

            catch (Exception ex)
            {
                return RedirectToAction("kategoriduzenle", "urun", new { Id = model.ID });
            }
        }

        public IActionResult kategorisil(int Id)
        {
            _categoryService.Delete(Id);
            return RedirectToAction("kategoriler", "urun");
        }

        public IActionResult kategoridurumduzenle(int Id)
        {
            try
            {
                var category = _categoryService.Get(Id);

                if (category.IsActive == true)
                    category.IsActive = false;
                else
                    category.IsActive = true;
                _categoryService.Update(category);

                return RedirectToAction("kategoriler", "urun");
            }
            catch (Exception ex)
            {
                return RedirectToAction("kategoriler", "urun");
            }
        }

        #endregion

        #region Markalar

        [HttpGet]
        public IActionResult markalar()
        {
            return View(_brandService.GetAll());
        }

        [HttpGet]
        public IActionResult markaekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult markaolustur(BrandDto model, IFormFile file)
        {
            try
            {
                if (file != null)
                    model.BrandImage = SaveImageProcess.ImageInsert(file, "Admin");

                _brandService.Insert(model);

                return RedirectToAction("markalar", "urun");
            }
            catch (Exception ex)
            {
                return RedirectToAction("markalar", "urun");
            }
        }

        [HttpGet]
        public IActionResult markaduzenle(int Id)
        {
            var marka = _brandService.Get(Id);
            return View(marka);
        }

        [HttpPost]
        public IActionResult markaguncelle(BrandDto model, IFormFile file)
        {
            try
            {
                if (file != null)
                    model.BrandImage = SaveImageProcess.ImageInsert(file, "Admin");

                var marka = _brandService.Get(model.ID);
                marka.BrandName = model.BrandName;
                marka.BrandImage = model.BrandImage;
                marka.UpdatedTime = DateTime.Now;

                _brandService.Update(marka);

                return RedirectToAction("markalar", "urun");
            }
            catch (Exception ex)
            {
                return RedirectToAction("markaduzenle", "urun");
            }
        }

        public IActionResult markasil(int Id)
        {
            _brandService.Delete(Id);
            return RedirectToAction("markalar", "urun");
        }

        public IActionResult markadurumduzenle(int Id)
        {
            try
            {
                var marka = _brandService.Get(Id);

                if (marka.IsActive == true)
                    marka.IsActive = false;
                else
                    marka.IsActive = true;
                _brandService.Update(marka);

                return RedirectToAction("markalar", "urun");
            }
            catch (Exception ex)
            {
                return RedirectToAction("markalar", "urun");
            }

        }

        #endregion

        #region Ürün Bileşenleri

        [HttpGet]
        public IActionResult urunbilesenleri()
        {
            ViewBag.Size = _sizeNumService.GetAll();
            ViewBag.Color = _colorService.GetAll();
            return View();
        }

        #region Beden

        [HttpPost]
        public IActionResult bedenolustur(SizeDto model)
        {
            model.IsActive = true;
            _sizeNumService.Insert(model);
            return RedirectToAction("urunbilesenleri", "urun");
        }

        [HttpPost]
        public IActionResult bedenguncelle(SizeDto model)
        {
            var size = _sizeNumService.Get(model.ID);
            size.SizeNumber = model.SizeNumber;
            size.IsActive = true;
            size.UpdatedTime = model.UpdatedTime;

            _sizeNumService.Update(size);
            return RedirectToAction("urunbilesenleri", "urun");
        }

        public IActionResult bedensil(int Id)
        {
            _sizeNumService.Delete(Id);
            return RedirectToAction("urunbilesenleri", "urun");
        }

        public IActionResult bedendurumduzenle(int Id)
        {
            var size = _sizeNumService.Get(Id);
            if (size.IsActive == true)
                size.IsActive = false;
            else
                size.IsActive = true;
            _sizeNumService.Update(size);
            return RedirectToAction("urunbilesenleri", "urun");
        }

        #endregion

        #region Renk

        [HttpGet]
        public IActionResult renkele()
        {
            return View();
        }

        [HttpGet]
        public IActionResult renkduzenle(int Id)
        {
            var color = _colorService.Get(Id);
            return View(color);
        }

        [HttpPost]
        public IActionResult renkolustur(ColorDto model)
        {
            model.IsActive = true;
            _colorService.Insert(model);
            return RedirectToAction("urunbilesenleri", "urun");
        }

        [HttpPost]
        public IActionResult renkguncelle(ColorDto model)
        {
            var color = _colorService.Get(model.ID);
            color.UpdatedTime = DateTime.Now;
            color.ColorCode = model.ColorCode;
            color.ColorName = model.ColorName;
            _colorService.Update(color);

            return RedirectToAction("urunbilesenleri", "urun");
        }

        public IActionResult renksil(int Id)
        {
            _colorService.Delete(Id);
            return RedirectToAction("urunbilesenleri", "urun");
        }

        public IActionResult renkdurumduzenle(int Id)
        {
            var color = _colorService.Get(Id);
            if (color.IsActive == true)
                color.IsActive = false;
            else
                color.IsActive = true;

            _colorService.Update(color);
            return RedirectToAction("urunbilesenleri", "urun");
        }

        #endregion

        #endregion

        #region Ürün İşlemler
        public IActionResult urunler(int? pageNumber)
        {
            int pageSize = 100;
            List<ProductDto> products = _productService.GetAll();

            var brands = _brandService.GetAll();
            var categories = _categoryService.GetAll();

            ViewBag.Brands = new SelectList(brands, "ID", "BrandName");
            ViewBag.Category = new SelectList(categories, "ID", "Categories");
            ViewBag.Urunler = PaginationList<ProductDto>.Create(products, pageNumber ?? 1, pageSize);

            return View();
        }

        [HttpGet]
        public IActionResult urunekle()
        {
            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.Brands = new SelectList(brands, "ID", "BrandName");

            List<CategoriesDto> categories = _categoryService.GetAll();
            ViewBag.Category = new SelectList(categories, "ID", "CategoryName");

            return View();
        }

        [HttpPost]
        public IActionResult urunkayityap(ProductDto product, IFormFile file)
        {
            try
            {
                if (file != null)
                    product.ProductBaseImage = SaveImageProcess.ImageInsert(file, "Admin");
                product.KDV = 18;

                int Id = _productService.insertProduct(product);
                return RedirectToAction("urunduzenle", "urun", new { Id = Id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("urunekle", "urun");
            }
        }

        [HttpGet]
        public IActionResult urunduzenle(int Id)
        {
            var urun = _productService.Get(Id);

            List<BrandDto> brands = _brandService.GetAll();
            ViewBag.Brands = new SelectList(brands, "ID", "BrandName", urun.BrandId);

            List<CategoriesDto> categories = _categoryService.GetAll();
            ViewBag.Category = new SelectList(categories, "ID", "CategoryName", urun.CategoryId);

            List<ColorProductDto> colorsProduct = _colorProductService.colorToProductId(urun.ID);
            ViewBag.ColorToProduct = colorsProduct;

            List<SizeNumProductDto> sizeProducts = _sizeNumberProductService.listSizeNumProductToProduct(urun.ID);
            ViewBag.SizeNumProduct = sizeProducts;

            List<ColorDto> colors = _colorService.GetAll();
            ViewBag.Colors = colors;

            List<SizeDto> sizes = _sizeNumService.GetAll();
            ViewBag.Size = sizes;

            List<ImagesProductDto> images = _imageProductService.getToIntProduct(urun.ID);
            ViewBag.ImageProducts = images;

            return View(urun);
        }

        [HttpPost]
        public IActionResult urunguncelle(ProductDto product, IFormFile file)
        {
            var urun = _productService.Get(product.ID);

            if (file != null)
                urun.ProductBaseImage = SaveImageProcess.ImageInsert(file, "Admin");

            urun.KDV = 18;
            urun.IsActive = product.IsActive;
            urun.ProductSpot = product.ProductSpot;
            urun.Quantity = product.Quantity;
            urun.ProductName = product.ProductName;
            urun.Price = product.Price;
            urun.BrandId = product.BrandId;
            urun.CategoryId = product.CategoryId;
            urun.UpdatedTime = DateTime.Now;
            urun.ChooseSizeIsHave = product.ChooseSizeIsHave;
            _productService.Update(urun);

            return RedirectToAction("urunler", "urun");
        }

        public IActionResult urunsil(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("urunler", "urun");
        }

        public IActionResult urundurumduzenle(int id)
        {
            var urun = _productService.Get(id);
            if (urun.IsActive == false)
                urun.IsActive = true;
            else
                urun.IsActive = false;

            _productService.Update(urun);
            return RedirectToAction("urunler", "urun");
        }

        public IActionResult bayiyeoner(int Id)
        {
            var urun = _productService.Get(Id);
            if (urun.Vitrin == false)
                urun.Vitrin = true;
            else
                urun.Vitrin = false;

            _productService.Update(urun);
            return RedirectToAction("urunler", "urun");
        }

        public IActionResult urunututtur(int id)
        {
            var urun = _productService.Get(id);

            if (urun.SortedRow == true)
                urun.SortedRow = false;
            else
                urun.SortedRow = true;

            _productService.Update(urun);

            return RedirectToAction("urunler", "urun");

        }
        #endregion

        #region Partial View

        public IActionResult bedenduzenle()
        {
            return PartialView("bedenduzenle");
        }

        public IActionResult renkduzenle()
        {
            return PartialView("renkduzenle");
        }

        #endregion

        #region External

        [HttpPost]
        public IActionResult fotograflariyukleyin(int Id, List<IFormFile> file)
        {
            var value = _productService.Get(Id);
            value.imagesProductList = new List<ImagesProductDto>();

            long size = file.Sum(f => f.Length);

            if (file.Count != 0)
            {
                foreach (var formFile in file)
                {
                    var gallery = new ImagesProductDto()
                    {
                        Image = SaveImageProcess.ImageInsert(formFile, "Admin"),
                        ProductId = value.ID,
                        IsActive = true,
                    };
                    value.imagesProductList.Add(gallery);
                }

                bool result = _imageProductService.imageInsertInProduct((List<ImagesProductDto>)value.imagesProductList);

                return RedirectToAction("urunduzenle", "urun", new { Id = value.ID });
            }
            else
            {
                return RedirectToAction("urunduzenle", "proje", new { Id = value.ID });
            }

        }

        public IActionResult fotografisil(int Id)
        {
            var value = _imageProductService.Get(Id);
            int productId = value.ProductId;
            _imageProductService.Delete(Id);
            return RedirectToAction("urunduzenle", "urun", new { Id = productId });
        }

        public IActionResult uruneRenkEkle(int colorId, int productId)
        {
            var productToColor = _colorProductService.deleteColorFromProduct(colorId, productId);
            
            if (productToColor == null)
            {
                ColorProductDto colorProduct = new ColorProductDto()
                {
                    ColorId = colorId,
                    ProductId = productId,
                    IsActive = true,
                };

                _colorProductService.Insert(colorProduct);
            }
          
            return Json(true);
        }

        public IActionResult urundenRenkSil(int colorId, int productId)
        {
            var productToColor = _colorProductService.deleteColorFromProduct(colorId, productId);
            if (productToColor != null)
            {
                _colorProductService.Delete(productToColor.ID);
            }
            return Json(true);
        }

        public IActionResult uruneBedenEkle(int sizeId, int productId)
        {

            SizeNumProductDto sizeProduct = new SizeNumProductDto()
            {
                SizeId = sizeId,
                ProductId = productId,
                IsActive = true,
            };

            _sizeNumberProductService.Insert(sizeProduct);
            return Json(true);
        }

        public IActionResult urundenBedenSil(int sizeId, int productId)
        {
            var sizeToProduct = _sizeNumberProductService.deleteSizeFromProduct(sizeId, productId);
            _sizeNumberProductService.Delete(sizeToProduct.ID);
            return Json(true);
        }

        #endregion
    }
}
