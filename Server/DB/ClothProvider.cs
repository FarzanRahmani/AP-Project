using Microsoft.Extensions.Logging;
using P8.Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mydatas;

namespace P8.Server.DB
{
    public class ClothProvider
    {
        private readonly ClothDbContext _context;
        private readonly ILogger _logger;
        public ClothProvider(ClothDbContext context , ILoggerFactory logger)
        {
            this._context = context;
            this._logger = logger.CreateLogger("ClothProvider");
        }
        // public void AddCloth(Cloth cloth)
        // {
        //     var newId = 0;
        //     if (_context.Clothes.Any())
        //         newId = _context.Clothes.OrderBy(arg=> arg).Last().Id + 1;
        //     cloth.Id = newId;
        //     _context.Clothes.Add(cloth);
        //     _context.SaveChanges();
        // }

        public async Task AddCloth(Cloth clothe)
        {
            // await _context.Clothes.AddAsync(clothe); // Clothes --> table
            // await _context.SaveChangesAsync(); // mire toye heroku upload mishe
            var newId = this._context.Clothes.Select(ArgIterator => ArgIterator.Id).Max() + 1;
            clothe.Id = newId;
            // clothe.Id = 1;
            await _context.Clothes.AddAsync(clothe);
            await _context.SaveChangesAsync();
        }

        // public async Task<List<Cloth>> GetAllClothes()
        // {
        //     return await this._context.Clothes.ToList();
        // }

        public List<Cloth> GetAllClothes()
        {
            return this._context.Clothes.ToList();
        }

        public Cloth GetClothById(int id)
        {
            return _context.Clothes.Where(cloth => cloth.Id == id).FirstOrDefault();
        }

        public Cloth UpdateClotheName(int Id,string NewName)
        {
            var clothe = _context.Clothes.Where(cloth => cloth.Id == Id).FirstOrDefault();
            var idx = _context.Clothes.ToList().IndexOf(clothe);
            clothe.Name = NewName;
            _context.Clothes.ToList()[idx] = clothe;
            // await _context.SaveChangesAsync();
            _context.SaveChangesAsync();
            return _context.Clothes.ToList()[idx];

            // var clothe = _context.Clothes.Where(cloth => cloth.Id == Id).FirstOrDefault();
            // var temp = _context.Clothes.ToList();
            // var idx = temp.IndexOf(clothe);
            // clothe.Name = NewName;
            // temp[idx] = clothe;
            // _context.Clothes = (DbSet<Cloth>)temp.AsQueryable();
            // _context.SaveChangesAsync();
            // return temp[idx];
        }

        public Cloth UpdateClothe(Cloth NewClothe)
        {
            var Clothe = _context.Clothes.Where(c => c.Id == NewClothe.Id).FirstOrDefault();
            // // Clothe = newClothe;
            // Clothe.SetValue(newClothe);
            var temp = _context.Clothes.ToList();
            var idx = temp.IndexOf(Clothe);
            temp[idx] = NewClothe;
            _context.Clothes = (DbSet<Cloth>)temp.AsQueryable();
            _context.SaveChangesAsync();
            return temp[idx];
        }

        public List<Cloth> DeleteClothByIds(int[] Ids)
        {
            _context.Clothes = (DbSet<Cloth>)_context.Clothes.Where(c => !Ids.Contains(c.Id));
            _context.SaveChangesAsync();
            return _context.Clothes.ToList();
        }

        public List<Cloth> DeleteCloth(Cloth clothe)
        {
            _context.Clothes = (DbSet<Cloth>)_context.Clothes.Where(c => !(clothe.Id == c.Id));
            _context.SaveChangesAsync();
            return _context.Clothes.ToList();
        }



        public List<product> GetAllProducts()
        {
            return this._context.Products.ToList();
        }

        public async Task AddProduct(product p)
        {
            if (!this._context.Products.Contains(p))
            {
                var newId = this._context.Products.Select(ArgIterator => ArgIterator.Id).Max() + 1;
                p.Id = newId;
                await _context.Products.AddAsync(p);
                await _context.SaveChangesAsync();
            }
        }

        public List<product> DeleteProduct(product p)
        {
            // _context.Products = (DbSet<product>)_context.Products.Where(c => !(p.Id == c.Id));
            _context.Products.Remove(p);
            _context.SaveChangesAsync();
            return _context.Products.ToList();
        }

        public product UpdateProduct(product NewProduct)
        {
            // var p = _context.Products.Where(c => c.Id == NewProduct.Id).FirstOrDefault();
            // var temp = _context.Products.ToList();
            // var idx = temp.IndexOf(p);
            // temp[idx] = NewProduct;
            // _context.Products = (DbSet<product>)temp.AsQueryable();
            // // _context.Products.Update(NewProduct);
            // // _context.SaveChangesAsync();
            // return temp[idx];
            // // return NewProduct;

            var product = _context.Products.Where(product => product.Id == NewProduct.Id).FirstOrDefault();
            var idx = _context.Products.ToList().IndexOf(product);
            product = NewProduct;
            _context.Products.ToList()[idx] = product;
            _context.SaveChangesAsync();
            return _context.Products.ToList()[idx];
        }
    
        // add and remove cart
        public async Task<List<product>> UpdateProducts(List<product> products)
        {
            this._context.Products.UpdateRange(products.ToArray());
            await _context.SaveChangesAsync();
            return _context.Products.ToList();
        }

        internal List<product> DeleteProductById(int id)
        {
            product p = this._context.Products.Where(p => p.Id == id).FirstOrDefault();
            this._context.Products.Remove(p);
            _context.SaveChanges();
            return _context.Products.ToList();
        }
    }
}
