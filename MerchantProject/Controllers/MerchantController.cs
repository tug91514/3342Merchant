using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace MerchantProject.Controllers
{
    [Produces("application/json")]
    [Route("api/Service")]
    public class MerchantController : Controller
    {
        [HttpGet] // GET api/Service/
        [HttpGet("Departments")]   // GET api/Service/Departments
        public DataSet GetAllDepartments()
        {
            //local var
            DBConnect dbconn = new DBConnect();
            string sqlcomm = "SELECT * FROM term_Department";
            DataSet myDataSet = new DataSet();

            try
            {
                //get dataset
                myDataSet = dbconn.GetDataSet(sqlcomm);

                //return DataSet
                return myDataSet;
            }
            catch (SqlException sqlex)
            {
                return null;
            }//end catch sql ex
            catch (Exception ex)
            {
                return null;
            }//end catch
        }

        // GET: api/API/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/API
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/API/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
