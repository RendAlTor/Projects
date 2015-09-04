using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Security.Cryptography;

namespace ECP.UploadPage1
{
    public partial class UploadForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {////////////////Creating Grid View ///////////////
            DataTable dt = new DataTable();//создаём таблицу
            dt.Columns.Add("File", typeof(string));
            dt.Columns.Add("Size", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Hash", typeof(string));
            dt.Columns.Add("Sign", typeof(string));
            dt.Columns.Add("PublicKey", typeof(string));
            dt.Columns.Add("Mod", typeof(string));
            dt.Columns.Add("Login", typeof(string));
            String userName, sign_out, openkey, mod = "";
            using (site_dbDataContext contex = new site_dbDataContext()) //Создан экземпляр класса БД               
            {
                //Создаем объекты таблиц
                FileUser UserName = new FileUser();


                foreach (string strfile in Directory.GetFiles(Server.MapPath("~/Data")))//вносим данные
                {
                    FileInfo fi = new FileInfo(strfile);
                    userName = (from x in new site_dbDataContext().FileUser where x.FileName == fi.Name select x.Email).First();
                    sign_out = (from x in new site_dbDataContext().FileUser where x.FileName == fi.Name select x.Sign).First();
                    openkey = (from x in new site_dbDataContext().PublicKeys where x.UserLogin == userName select x.PublicKey).First();
                    mod = (from x in new site_dbDataContext().PublicKeys where x.UserLogin == userName select x.Mod).First();
                    dt.Rows.Add(fi.Name, fi.Length, GetFileTypeByExtension(fi.Extension), ComputeMD5Checksum(Server.MapPath("~/Data/") + fi.Name), sign_out, openkey, mod, userName);
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();

            //////////////////////////////////////////////////
        }
        private string ComputeMD5Checksum(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSum = md5.ComputeHash(fileData);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                return result;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Context.User.Identity.Name == "")
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Data/") + FileUpload1.FileName);// куда грузить
                    ////////////Writing user name into database///////////
                    using (site_dbDataContext contex = new site_dbDataContext()) //Создан экземпляр класса БД               
                    {
                        //Создаем объекты таблиц
                        FileUser UserName = new FileUser();
                        //PublicKey
                        UserName.Email = Context.User.Identity.Name;
                        UserName.FileName = FileUpload1.FileName;


                        ////////////////////////////////////////////////////
                        ///////////////Добавляем Подпись///////////////////

                        RSA.BigInteger BI_Text = new RSA.BigInteger(ComputeMD5Checksum(Server.MapPath("~/Data/") + FileUpload1.FileName), 16);
                        RSA.BigInteger d = new RSA.BigInteger((from x in new site_dbDataContext().Sisurity where x.UserLogin == Context.User.Identity.Name select x.PrivatKey).First(), 10);
                        RSA.BigInteger n = new RSA.BigInteger((from x in new site_dbDataContext().PublicKeys where x.UserLogin == Context.User.Identity.Name select x.Mod).First(), 10);
                        byte[] sign = BI_Text.modPow(d, n).getBytes();
                        UserName.Sign = BitConverter.ToString(sign).Replace("-", string.Empty);
                        contex.FileUser.InsertOnSubmit(UserName);
                        contex.SubmitChanges();
                        ///////////////////////////////////////////////////*/
                    }
                }

                DataTable dt = new DataTable();//создаём таблицу
                dt.Columns.Add("File", typeof(string));
                dt.Columns.Add("Size", typeof(string));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Hash", typeof(string));
                dt.Columns.Add("Sign", typeof(string));
                dt.Columns.Add("PublicKey", typeof(string));
                dt.Columns.Add("Mod", typeof(string));
                dt.Columns.Add("Login", typeof(string));
                String userName, sign_out, openkey, mod = "";
                using (site_dbDataContext contex = new site_dbDataContext()) //Создан экземпляр класса БД               
                {
                    //Создаем объекты таблиц
                    FileUser UserName = new FileUser();


                    foreach (string strfile in Directory.GetFiles(Server.MapPath("~/Data")))//вносим данные
                    {
                        FileInfo fi = new FileInfo(strfile);
                        userName = (from x in new site_dbDataContext().FileUser where x.FileName == fi.Name select x.Email).First();
                        sign_out = (from x in new site_dbDataContext().FileUser where x.FileName == fi.Name select x.Sign).First();
                        openkey = (from x in new site_dbDataContext().PublicKeys where x.UserLogin == userName select x.PublicKey).First();
                        mod = (from x in new site_dbDataContext().PublicKeys where x.UserLogin == userName select x.Mod).First();
                        dt.Rows.Add(fi.Name, fi.Length, GetFileTypeByExtension(fi.Extension), ComputeMD5Checksum(Server.MapPath("~/Data/") + fi.Name), sign_out, openkey, mod, userName);
                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
        }
        private string GetFileTypeByExtension(string extension)// Рассширенный тип файла
        {
            switch (extension.ToLower())
            {
                case ".doc":
                case ".docx":
                    return "Microsoft Word Document"; ////брейки дописать
                case ".xlsx":
                case ".xls":
                    return "Microsoft Exel Document";
                case ".txt":
                    return "Text Document";
                case ".jpg":
                case ".png":
                    return "Image";
                default:
                    return "Unknown";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)// Загрузка по ссылке
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("~/Data/") + e.CommandArgument);
                Response.End();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
             RSA.BigInteger mod = new RSA.BigInteger(TextBox1.Text,16).modPow(new RSA.BigInteger(TextBox2.Text,10), new RSA.BigInteger(TextBox4.Text.Trim(),10));
             TextBox3.Text = mod.ToHexString();
            
        }
    }
}