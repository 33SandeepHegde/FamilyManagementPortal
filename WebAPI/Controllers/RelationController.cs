using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class RelationController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select Serialid,Name,Age,
                    convert(varchar(10),DOB,120) as DOB,
                    PhotoFileName
                    from
                    dbo.Relation
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
        public string Post(Relation rel)
        {
            try
            {
                string query = @"
                    insert into dbo.Relation values
                    (
                    '" + rel.Name + @"'
                    ,'" + rel.Age + @"'
                    ,'" + rel.DOB + @"'
                    ,'" + rel.PhotoFileName + @"'
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
        public string Put(Relation rel)
        {
            try
            {
                string query = @"
                    update dbo.Relation set 
                    Name='" + rel.Name + @"'
                    ,Age='" + rel.Age + @"'
                    ,DOB='" + rel.DOB + @"'
                    ,PhotoFileName='" + rel.PhotoFileName + @"'
                    where Serialid=" + rel.SerialId + @"
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
            catch (Exception)
            {
                return "Fail to Update!";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"
                    delete from dbo.Relation 
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
        [Route("api/Relation/SaveFile")]

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
