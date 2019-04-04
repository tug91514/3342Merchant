using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MerchantAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace MerchantAPI.Controllers
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

        [HttpGet("test")]   // GET api/Service/test
        public string testCall()
        {
            return "";
        }

        // GET: api/API/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/API
        [HttpPost]
        public bool RecordPurchase(string ProductID,
            int Quantity, string SellerSiteID, string APIKey,
            [FromBody]Customer CustomerInfo)
        {

            //if APIKey is correct then do transaction
            if (IsCorrectAPIKey(APIKey, GetInt(SellerSiteID)))
            {
                if (CustomerExistsInDB(CustomerInfo.Id))
                {

                }//end if inner
                else
                {

                }//end else
            }//end if outter
        }//end Record Purchase

        protected bool UpdateCustomerTotalSales(int CustomerID, int ProductID,
            int quantity)
        {

        }//end updateCustomerTotalSales

        protected double GetProductPrice(int ProductID)
        {
            //local var
            DBConnect dbconn = new DBConnect();

            SqlCommand sqlcomm = new SqlCommand("getProductPriceByID");
            sqlcomm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcomm.Parameters.Add(new SqlParameter("@Product_id", ProductID));
            DataSet myDataSet = new DataSet();

            try
            {
                //local var
                double price = 0.0;
                //get dataset
                myDataSet = dbconn.GetDataSetUsingCmdObj(sqlcomm);

                //if the dataset contains 1 return true
                if (myDataSet.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = myDataSet.Tables[0].Rows[0];
                    price = (double)dr["price"];
                    return price;
                }//end if
                else
                {
                    return 0;
                }//end else
            }
            catch (SqlException sqlex)
            {
                return 0;
            }//end catch sql ex
            catch (Exception ex)
            {
                return 0;
            }//end catch
        }//end GetProductPrice

        protected bool CustomerExistsInDB(int CustomerID)
        {
            //local var
            DBConnect dbconn = new DBConnect();

            SqlCommand sqlcomm = new SqlCommand("getCustomerByID");
            sqlcomm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcomm.Parameters.Add(new SqlParameter("@Customer_id", CustomerID));
            DataSet myDataSet = new DataSet();

            try
            {
                //get dataset
                myDataSet = dbconn.GetDataSetUsingCmdObj(sqlcomm);

                //if the dataset contains 1 return true
                if (myDataSet.Tables[0].Rows.Count == 1)
                {
                    return true;
                }//end if
                else
                {
                    return false;
                }//end else
            }
            catch (SqlException sqlex)
            {
                return false;
            }//end catch sql ex
            catch (Exception ex)
            {
                return false;
            }//end catch
        }//end CustomerExistsInDB

        protected bool IsCorrectAPIKey(string APIKey, int Site_id)
        {
            //local var
            DBConnect dbconn = new DBConnect();

            SqlCommand sqlcomm = new SqlCommand("getSiteByIDAndAPIKey");
            sqlcomm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcomm.Parameters.Add(new SqlParameter("@Site_id", Site_id));
            sqlcomm.Parameters.Add(new SqlParameter("@api_key", APIKey));
            DataSet myDataSet = new DataSet();

            try
            {
                //get dataset
                myDataSet = dbconn.GetDataSetUsingCmdObj(sqlcomm);

                //if the dataset contains 1 return true
                if (myDataSet.Tables[0].Rows.Count == 1)
                {
                    return true;
                }//end if
                else
                {
                    return false;
                }//end else
            }
            catch (SqlException sqlex)
            {
                return false;
            }//end catch sql ex
            catch (Exception ex)
            {
                return false;
            }//end catch
        }//end IsCorrectAPIKey

        public int GetInt(string toParse)
        {
            //local var
            int num = -1;
            //try parse
            int.TryParse(toParse, out num);
            //return num
            return num;
        }//end getIDInt

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
