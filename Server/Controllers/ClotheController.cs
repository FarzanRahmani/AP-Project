using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mydatas;
using P8.Server.DB;
using P8.Shared;

namespace P8.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // api/Clothe // api/esm class bedoone controller // dakhele[] re az esm class bar midare
    public class ClotheController : Controller
    {
        public static List<Cloth> Clothes = new List<Cloth>
        {
            new Cloth(){Name = "hat" , Color = "Red" , Id = 1 , Price = 1000},
            new Cloth(){Name = "Shirt" , Color = "Blue" , Id = 2 , Price = 2000},
            new Cloth(){Name = "Pants" , Color = "Purple" , Id = 3 , Price = 3000}
        };

        // 1
        [HttpGet] // --> attribute
        [Route("getAllClothes")] // api/Clothe/getAllClothes
        // 2
        // [HttpGet("getAllClothes")] // api/Clothe/getAllClothes
        public List<Cloth> GetAllClothes() => ClotheController.Clothes;

        [HttpGet("getClothById/{id}")] // api/Clothe/getClotheById/2
        public Cloth GetClothById(int id )
        {
            return ClotheController.Clothes.Where(cloth => cloth.Id == id).FirstOrDefault();
        }

        [HttpPost]
        [Route("addNewCloth")]
        public Cloth AddNewCloth(Cloth cloth) // api/Clothe/AddNewCloth   postman --> body 
        // {
        //     "price" : 100,
        //     "Name" : "hat" , 
        //     "Id" : 563 ,
        //     "Color" : "purple"
        // }  
        {
            var newId = ClotheController.Clothes.Last().Id + 1;
            var newCloth = new Cloth() { Name = cloth.Name, Color = cloth.Color, Id = newId, Price = cloth.Price };
            ClotheController.Clothes.Add(newCloth);
            return newCloth;
        }

        [HttpDelete]
        [Route("removeClothes")] // api/Clothe/RemoveClothes    postman --> body --> raw --> Json --> array:mesl list python (Json int va string va array ro mishnase vali object ro na)
        public List<Cloth> RemoveCloth(int[] ids)
        // [       
        // {
        // "price" : 100,
        // "Name" : "hat" , 
        // "Id" : 563 ,
        // "Color" : "purple"
        // },
        //     1,
        //     2,
        //     3
        // ]   
        {
            // ClotheController.Clothes.RemoveAll(arg => ids.Contains(arg.Id));
            Clothes = Clothes.Where(c => !ids.Contains(c.Id)).ToList();
            return Clothes;
        }

        [HttpPut("updateClothName")]
        // [Route("updateClothName")]
        // [HttpPut("updateClothName/{id}/{name}")]    rahe digare anjame in kar
        public Cloth UpdateClothName(int id , string name) // api/Clothe/updateClothName postman --> put -->  Params -> Key : id - Value :5 -. Key : newName - Value : Gloves
        {
            var clothe = Clothes.Where(cloth => cloth.Id == id).FirstOrDefault();
            var idx = Clothes.IndexOf(clothe);
            clothe.Name = name;
            Clothes[idx] = clothe;
            return Clothes[idx];
        }

        [HttpPut]
        [Route("updateCloth")]
        public Cloth UpdateCloth(Cloth newClothe)
        {
            var Clothe = Clothes.Where(c => c.Id == newClothe.Id).FirstOrDefault();
            // // Clothe = newClothe;
            // Clothe.SetValue(newClothe);
            var idx = Clothes.IndexOf(Clothe);
            Clothes[idx] = newClothe;
            return Clothes[idx];
        }
        private readonly ClothProvider _provider;
        public ClotheController(ClothProvider provider)
        {
            this._provider = provider;
        }
        // [HttpPost]
        // [Route("addClothToDb")]
        // public Cloth AddClothToDb(Cloth cloth)
        // {
        //     this._provider.AddCloth(cloth);
        //     return cloth;
        // }   
        [HttpPost] // create
        [Route("addClothToDb")] // api/Clothe/addClothToDb
        public async Task<Cloth> AddClothToDb(Cloth cloth)
        {
            await this._provider.AddCloth(cloth);
            return cloth;
        }  
        
        // [HttpPost]
        // [Route("getAllClothesFromDb")]
        // public async Task<List<Cloth>> GetAllClothesFromDb()
        // {
        //     List<Cloth> Clothes = await this._provider.GetAllClothes();
        //     return Clothes;
        // }

        // [HttpPost] // read
        // [Route("getAllClothesFromDb")]
        // public List<Cloth> GetAllClothesFromDb()
        // {
        //     List<Cloth> Clothes = this._provider.GetAllClothes();
        //     return Clothes;
        // }

        [HttpGet] // read
        [Route("getAllClothesFromDb")] // api/Clothe/getAllClothesFromDb
        public List<Cloth> GetAllClothesFromDb()
        {
            List<Cloth> Clothes = this._provider.GetAllClothes();
            return Clothes;
        }

        [HttpGet("getClothByIdFromDb/{id}")] // api/Clothe/getClothByIdFromDb/2
        public Cloth GetClothByIdFromDb(int id )
        {
            return this._provider.GetClothById(id);
            
        }

        [HttpPut("UpdateClothNameInDb/{Id}/{NewName}")] // update
        public Cloth UpdateClothNameInDb(int Id , string NewName)
        {
            var Clothe = this._provider.UpdateClotheName(Id,NewName);
            return Clothe;
        }

        [HttpPut("UpdateClothInDb")] // update
        public Cloth UpdateClothInDb(Cloth newClothe)
        {
            var Clothe = this._provider.UpdateClothe(newClothe);
            return Clothe;
        }

        [HttpDelete("DeleteClothFromDbByIds")] // delete or remove
        public List<Cloth> DeleteClothFromDbByIds(int[] ids)
        {
            return _provider.DeleteClothByIds(ids);
        }

        [HttpDelete("DeleteClothFromDb")]
        public List<Cloth> DeleteClothFromDb(Cloth clothe)
        {
            return _provider.DeleteCloth(clothe);
        }
        


        [HttpGet] // read
        [Route("GetAllProductsFromDb")] // api/Clothe/GetAllProductsFromDb
        public List<product> GetAllProductsFromDb()
        {
            List<product> Products = this._provider.GetAllProducts();
            return Products;
        }

        [HttpPost] // create
        [Route("addProductToDb")] // api/Clothe/addProductToDb
        public async Task<product> AddProductToDb(product p)
        {
            await this._provider.AddProduct(p);
            return p;
        }

        [HttpDelete("DeleteProductFromDb")]
        public List<product> DeleteProductFromDb(product p)
        {
            return _provider.DeleteProduct(p);
        }

        [HttpDelete("DeleteProductFromDbById")]
        public List<product> DeleteProductFromDbById(int Id)
        {
            return _provider.DeleteProductById(Id);
        }

        [HttpPut("UpdateProductInDb")] // update
        public product UpdateProductInDb(product newProduct)
        {
            var p = this._provider.UpdateProduct(newProduct);
            return p;
        }

        // add and remove cart

        [HttpPut("UpdateProductsInDb")] // api/Clothe/UpdateProductsInDb
        public List<product> UpdateProductsInDb(List<product> newProducts)
        {
            var p = this._provider.UpdateProducts(newProducts);
            return p.Result;
        }
    }
}
