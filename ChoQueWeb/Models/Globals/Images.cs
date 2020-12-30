using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models.Globals
{
    public class Images
    {
        public static string ImgURL(IFormFile img,string folder)
        {
            string _fileName = img.FileName;
            //lay thoi gian gan vao ten file
            var timestamp = DateTime.Now.ToFileTime();
            _fileName = timestamp + "_" + _fileName;
            //lay duong dan cua file
            var _folder = folder == "" ? "Images" : folder;
            string _path = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Upload/{_folder}", _fileName));
            //upload file
            using (var stream = new FileStream(_path, FileMode.Create))
            {
                img.CopyTo(stream);
            }
            //update gia tri vao cot img trong csdl
            //record.img = _fileName;
            return _fileName;
        }

        public static void RemoveImgURL(string _path, string folder)
        {
            var _folder = folder == "" ? "Images" : folder;
            if (_path != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", $"{_folder}", _path)))
            {
                //xoa anh
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", $"{_folder}", _path));
            }
        }
    }
}
