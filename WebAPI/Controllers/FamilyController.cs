using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace WebAPI.Controllers
{
    public class FamilyController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select Serialid,Name,Age,
                    convert(varchar(10),DOB,120) as DOB,
                    PhotoFileName
                    from
                    dbo.Family
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["FamilyAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);


        }
        public string Post(Family fam)
        {
            try
            {
                string query = @"
                    insert into dbo.Family values
                    (
                    '" + fam.Name + @"'
                    ,'" + fam.Age + @"'
                    ,'" + fam.DOB + @"'
                    ,'" + fam.PhotoFileName + @"'
                    )
                    ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["FamilyAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully";
            }
            catch
            {
                return "Failure to ADD!";
            }
        }
        public string Put(Family fam)
        {
            try
            {
                string query = @"
                    update dbo.Family set 
                    Name='" + fam.Name + @"'
                    ,Age='" + fam.Age + @"'
                    ,DOB='" + fam.DOB + @"'
                    ,PhotoFileName='" + fam.PhotoFileName + @"'
                    where Serialid=" + fam.SerialId + @"
                    ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["FamilyAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully";
            }
            catch(Exception)
            {
                return "Fail to Update!";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"
                    delete from dbo.Family 
                    where Serialid=" + id + @"
                    ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["FamilyAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failure to Delete!";
            }
        }
        [Route ("api/Family/SaveFile")]
        
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;
            }
            catch (Exception)
            {

                return "anonymous.png";
            }
        }
    }
}