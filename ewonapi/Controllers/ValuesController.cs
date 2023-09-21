// Decompiled with JetBrains decompiler
// Type: ewonapi.Controllers.ValuesController
// Assembly: ewonapi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F7FCC8E0-3BD0-4F21-931C-0E28139ADE38
// Assembly location: C:\Users\e5014039\Desktop\wwwroot (5)\bin\ewonapi.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ewonapi.Controllers
{
    public class ValuesController : ApiController
    {
        private string constring = ConfigurationManager.ConnectionStrings["ConnectTitan"].ToString();

        public IEnumerable<string> Get() => (IEnumerable<string>)new string[2]
        {
      "value1",
      "value2"
        };

        public string Get(int id) => "value";

        public void Post([FromBody] string value)
        {
        }

        public void Put(int id, [FromBody] string value)
        {
        }

        public void Delete(int id)
        {
        }

        [Route("api/Values/Insertdata")]
        [HttpPost]
        public void Insertdata(DateTime Timestamp, string tagname, int tagvalue)
        {

            string cmdText = "Insert Into dbo.test (timestamp,tagname,tagvalue) VALUES (@timestamp,@tn,@tv) ";
            using (SqlConnection connection = new SqlConnection(this.constring))
            {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                {
                    sqlCommand.Parameters.Add("@tn", SqlDbType.NVarChar, 100).Value = (object)tagname;
                    sqlCommand.Parameters.Add("@tv", SqlDbType.NVarChar, 100).Value = (object)tagvalue;
                    sqlCommand.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = (object)Timestamp;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        [Route("api/Values/InsertRawtable")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage InsertRawtable(
          DateTime Time_Stamp,
          DateTime Date,
          string Shift_Id,
          string Line_Code,
          string Machine_Code,
          string Variant_Code,
          string Machine_Status,
          int OK_Parts,
          int NOK_Parts,
          int Rework_Parts,
          string Rejection_Reasons,
          int Auto__Mode_Selected,
          int Manual_Mode_Slected,
          int Auto_Mode_Running,
          string CompanyCode,
          string PlantCode,
          string OperatorID,
          string Live_Alarm,
          string Live_Loss,
          string Batch_code)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                DateTime dateValue;
                if (DateTime.TryParse(Time_Stamp.ToString(),
                                 CultureInfo.InvariantCulture,
                                 DateTimeStyles.None,
                                 out dateValue) && DateTime.TryParse(Date.ToString(),
                                 CultureInfo.InvariantCulture,
                                 DateTimeStyles.None,
                                 out dateValue))
                {
                    string cmdText = "Insert Into dbo.RAWTable(Time_Stamp,Date,Shift_Id,Line_Code,Machine_Code,Variant_Code,Machine_Status,OK_Parts,NOK_Parts,Rework_Parts,Rejection_Reasons,Auto__Mode_Selected,Manual_Mode_Slected,Auto_Mode_Running,CompanyCode,PlantCode) VALUES (@ts,@date,@shift,@line,@machine,@variant,@status,@ok,@nok,@rework,@rej,@automode,@manual,@autorunning,@company,@plant) ";
                    using (SqlConnection connection = new SqlConnection(con_string))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                        {
                            sqlCommand.Parameters.Add("@ts", SqlDbType.DateTime).Value = (object)Time_Stamp;
                            sqlCommand.Parameters.Add("@date", SqlDbType.DateTime).Value = (object)Date;
                            sqlCommand.Parameters.Add("@shift", SqlDbType.NVarChar).Value = (object)Shift_Id;
                            sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                            sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                            sqlCommand.Parameters.Add("@variant", SqlDbType.NVarChar).Value = (object)Variant_Code;
                            sqlCommand.Parameters.Add("@status", SqlDbType.NVarChar).Value = (object)Machine_Status;
                            sqlCommand.Parameters.Add("@ok", SqlDbType.Int).Value = (object)OK_Parts;
                            sqlCommand.Parameters.Add("@nok", SqlDbType.Int).Value = (object)NOK_Parts;
                            sqlCommand.Parameters.Add("@rework", SqlDbType.Int).Value = (object)Rework_Parts;
                            sqlCommand.Parameters.Add("@rej", SqlDbType.NVarChar).Value = (object)Rejection_Reasons;
                            sqlCommand.Parameters.Add("@automode", SqlDbType.Int).Value = (object)Auto__Mode_Selected;
                            sqlCommand.Parameters.Add("@manual", SqlDbType.Int).Value = (object)Manual_Mode_Slected;
                            sqlCommand.Parameters.Add("@autorunning", SqlDbType.Int).Value = (object)Auto_Mode_Running;
                            sqlCommand.Parameters.Add("@company", SqlDbType.NVarChar).Value = (object)CompanyCode;
                            sqlCommand.Parameters.Add("@plant", SqlDbType.NVarChar).Value = (object)PlantCode;
                            sqlCommand.Parameters.Add("@OperatorID", SqlDbType.NVarChar).Value = (object)OperatorID;
                            sqlCommand.Parameters.Add("@Live_Alarm", SqlDbType.NVarChar).Value = (object)Live_Alarm;
                            sqlCommand.Parameters.Add("@Live_Loss", SqlDbType.NVarChar).Value = (object)Live_Loss;
                            sqlCommand.Parameters.Add("@Batch_code", SqlDbType.NVarChar).Value = (object)Batch_code;
                            connection.Open();
                            sqlCommand.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
                else
                {
                    string cmdText = "Insert Into dbo.RAWTable_Date(Time_Stamp,Date,Shift_Id,Line_Code,Machine_Code,Variant_Code,Machine_Status,OK_Parts,NOK_Parts,Rework_Parts,Rejection_Reasons,Auto__Mode_Selected,Manual_Mode_Slected,Auto_Mode_Running,CompanyCode,PlantCode,OperatorID,Live_Alarm,Live_Loss,Batch_code) VALUES (@ts,@date,@shift,@line,@machine,@variant,@status,@ok,@nok,@rework,@rej,@automode,@manual,@autorunning,@company,@plant,@OperatorID,@Live_Alarm,@Live_Loss,@Batch_code) ";
                    using (SqlConnection connection = new SqlConnection(con_string))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                        {
                            sqlCommand.Parameters.Add("@ts", SqlDbType.NVarChar).Value = (object)Time_Stamp;
                            sqlCommand.Parameters.Add("@date", SqlDbType.NVarChar).Value = (object)Date;
                            sqlCommand.Parameters.Add("@shift", SqlDbType.NVarChar).Value = (object)Shift_Id;
                            sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                            sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                            sqlCommand.Parameters.Add("@variant", SqlDbType.NVarChar).Value = (object)Variant_Code;
                            sqlCommand.Parameters.Add("@status", SqlDbType.NVarChar).Value = (object)Machine_Status;
                            sqlCommand.Parameters.Add("@ok", SqlDbType.Int).Value = (object)OK_Parts;
                            sqlCommand.Parameters.Add("@nok", SqlDbType.Int).Value = (object)NOK_Parts;
                            sqlCommand.Parameters.Add("@rework", SqlDbType.Int).Value = (object)Rework_Parts;
                            sqlCommand.Parameters.Add("@rej", SqlDbType.NVarChar).Value = (object)Rejection_Reasons;
                            sqlCommand.Parameters.Add("@automode", SqlDbType.Int).Value = (object)Auto__Mode_Selected;
                            sqlCommand.Parameters.Add("@manual", SqlDbType.Int).Value = (object)Manual_Mode_Slected;
                            sqlCommand.Parameters.Add("@autorunning", SqlDbType.Int).Value = (object)Auto_Mode_Running;
                            sqlCommand.Parameters.Add("@company", SqlDbType.NVarChar).Value = (object)CompanyCode;
                            sqlCommand.Parameters.Add("@plant", SqlDbType.NVarChar).Value = (object)PlantCode;
                            sqlCommand.Parameters.Add("@OperatorID", SqlDbType.NVarChar).Value = (object)OperatorID;
                            sqlCommand.Parameters.Add("@Live_Alarm", SqlDbType.NVarChar).Value = (object)Live_Alarm;
                            sqlCommand.Parameters.Add("@Live_Loss", SqlDbType.NVarChar).Value = (object)Live_Loss;
                            sqlCommand.Parameters.Add("@Batch_code", SqlDbType.NVarChar).Value = (object)Batch_code;
                            connection.Open();
                            sqlCommand.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }

                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }

        [Route("api/Values/InsertOperatoreff")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage InsertOperatoreff(
          DateTime Time_Stamp,
          string Line_Code,
          string Machine_Code,
          string CompanyCode,
          string PlantCode,
          string Shift_Id,
          string Variant_Code,
          string Machine_Status,
          string OperatorID,
          decimal Manual_CycleTime,
          DateTime StartTime_Operation,
          DateTime EndTime_Operation,
          int Ok_Parts,
          int NOk_Parts)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                DateTime dateValue;
                //if (DateTime.TryParse(Time_Stamp.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue) && DateTime.TryParse(StartTime_Operation.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue) && DateTime.TryParse(EndTime_Operation.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue))
                //{
                string cmdText = "Insert Into dbo.tbl_Raw_Operator_Efficiency(Time_Stamp,Line_Code,Machine_Code,CompanyCode,PlantCode,Shift_Id,Variant_Code,Machine_Status,OperatorID,Manual_CycleTime,StartTime_Operation,EndTime_Operation,Ok_Parts,NOk_Parts)VALUES (@ts,@line,@machine,@company,@plant,@shift,@variant,@status,@operatorid,@ct,@sttime,@endtime,@okparts,@nokparts) ";
                using (SqlConnection connection = new SqlConnection(con_string))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@ts", SqlDbType.DateTime).Value = (object)Time_Stamp;
                        sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                        sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                        sqlCommand.Parameters.Add("@company", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@plant", SqlDbType.NVarChar).Value = (object)PlantCode;
                        sqlCommand.Parameters.Add("@shift", SqlDbType.NVarChar).Value = (object)Shift_Id;
                        sqlCommand.Parameters.Add("@variant", SqlDbType.NVarChar).Value = (object)Variant_Code;
                        sqlCommand.Parameters.Add("@status", SqlDbType.Int).Value = (object)Machine_Status;
                        sqlCommand.Parameters.Add("@operatorid", SqlDbType.NVarChar).Value = (object)OperatorID;
                        sqlCommand.Parameters.Add("@ct", SqlDbType.Decimal).Value = (object)Manual_CycleTime;
                        sqlCommand.Parameters.Add("@sttime", SqlDbType.DateTime).Value = (object)StartTime_Operation;
                        sqlCommand.Parameters.Add("@endtime", SqlDbType.DateTime).Value = (object)EndTime_Operation;
                        sqlCommand.Parameters.Add("@okparts", SqlDbType.Int).Value = (object)Ok_Parts;
                        sqlCommand.Parameters.Add("@nokparts", SqlDbType.Int).Value = (object)NOk_Parts;
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                // }
                //else
                //{
                //    string cmdText = "Insert Into dbo.tbl_Raw_Operator_Efficiency_Date(Time_Stamp,Line_Code,Machine_Code,CompanyCode,PlantCode,Shift_Id,Variant_Code,Machine_Status,OperatorID,Manual_CycleTime,StartTime_Operation,EndTime_Operation,Ok_Parts,NOk_Parts)VALUES (@ts,@line,@machine,@company,@plant,@shift,@variant,@status,@operatorid,@ct,@sttime,@endtime,@okparts,@nokparts) ";
                //    using (SqlConnection connection = new SqlConnection(this.constring))
                //    {
                //        using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                //        {
                //            sqlCommand.Parameters.Add("@ts", SqlDbType.NVarChar).Value = (object)Time_Stamp;
                //            sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                //            sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                //            sqlCommand.Parameters.Add("@company", SqlDbType.NVarChar).Value = (object)CompanyCode;
                //            sqlCommand.Parameters.Add("@plant", SqlDbType.NVarChar).Value = (object)PlantCode;
                //            sqlCommand.Parameters.Add("@shift", SqlDbType.NVarChar).Value = (object)Shift_Id;
                //            sqlCommand.Parameters.Add("@variant", SqlDbType.NVarChar).Value = (object)Variant_Code;
                //            sqlCommand.Parameters.Add("@status", SqlDbType.Int).Value = (object)Machine_Status;
                //            sqlCommand.Parameters.Add("@operatorid", SqlDbType.NVarChar).Value = (object)OperatorID;
                //            sqlCommand.Parameters.Add("@ct", SqlDbType.Decimal).Value = (object)Manual_CycleTime;
                //            sqlCommand.Parameters.Add("@sttime", SqlDbType.NVarChar).Value = (object)StartTime_Operation;
                //            sqlCommand.Parameters.Add("@endtime", SqlDbType.NVarChar).Value = (object)EndTime_Operation;
                //            sqlCommand.Parameters.Add("@okparts", SqlDbType.Int).Value = (object)Ok_Parts;
                //            sqlCommand.Parameters.Add("@nokparts", SqlDbType.Int).Value = (object)NOk_Parts;
                //            connection.Open();
                //            sqlCommand.ExecuteNonQuery();
                //            connection.Close();
                //        }
                //    }
                //}

                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }

        [Route("api/Values/InsertToollife")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage InsertToollife(
          string Line_Code,
          string Machine_Code,
          string ToolID,
          string Classification,
          Decimal CurrentLifeCycle,
          DateTime Time_Stamp,
          string CompanyCode,
          string PlantCode)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                DateTime dateValue;
                //if (DateTime.TryParse(Time_Stamp.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue))
                //{

                string cmdText = "Insert Into dbo.tbl_Raw_Toollife(Line_Code,Machine_Code,ToolID,Classification,CurrentLifeCycle,Time_Stamp,CompanyCode,PlantCode)VALUES (@line,@machine,@toolid,@class,@lifecycle,@timestamp,@company,@plant) ";
                using (SqlConnection connection = new SqlConnection(con_string))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                        sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                        sqlCommand.Parameters.Add("@toolid", SqlDbType.NVarChar).Value = (object)ToolID;
                        sqlCommand.Parameters.Add("@class", SqlDbType.NVarChar).Value = (object)Classification;
                        sqlCommand.Parameters.Add("@lifecycle", SqlDbType.Int).Value = (object)CurrentLifeCycle;
                        sqlCommand.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = (object)Time_Stamp;
                        sqlCommand.Parameters.Add("@company", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@plant", SqlDbType.NVarChar).Value = (object)PlantCode;
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                // }
                //else
                //{
                //    string cmdText = "Insert Into dbo.tbl_Raw_Toollife_Date(Line_Code,Machine_Code,ToolID,Classification,CurrentLifeCycle,Time_Stamp,CompanyCode,PlantCode)VALUES (@line,@machine,@toolid,@class,@lifecycle,@timestamp,@company,@plant) ";
                //    using (SqlConnection connection = new SqlConnection(this.constring))
                //    {
                //        using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                //        {
                //            sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                //            sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                //            sqlCommand.Parameters.Add("@toolid", SqlDbType.NVarChar).Value = (object)ToolID;
                //            sqlCommand.Parameters.Add("@class", SqlDbType.NVarChar).Value = (object)Classification;
                //            sqlCommand.Parameters.Add("@lifecycle", SqlDbType.Int).Value = (object)CurrentLifeCycle;
                //            sqlCommand.Parameters.Add("@timestamp", SqlDbType.NVarChar).Value = (object)Time_Stamp;
                //            sqlCommand.Parameters.Add("@company", SqlDbType.NVarChar).Value = (object)CompanyCode;
                //            sqlCommand.Parameters.Add("@plant", SqlDbType.NVarChar).Value = (object)PlantCode;
                //            connection.Open();
                //            sqlCommand.ExecuteNonQuery();
                //            connection.Close();
                //        }
                //    }
                //}

                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }

        [Route("api/Values/InsertMachineAlarm")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage InsertMachineAlarm(
          string Line_Code,
          string Machine_Code,
          string Shift_ID,
          string Alarm_ID,
          DateTime Start_Time,
          DateTime End_Time,
          DateTime Time_Stamp,
          DateTime Date,
          string CompanyCode,
          string PlantCode)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                DateTime dateValue;
                //if ((DateTime.TryParse(Time_Stamp.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue)) && (DateTime.TryParse(Start_Time.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue)) && (DateTime.TryParse(End_Time.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue)))
                //{
                string cmdText = "Insert Into dbo.MachineAlarm(Line_Code,Machine_Code,Shift_ID,Alarm_ID,Start_Time,End_Time,Time_Stamp,Date,CompanyCode,PlantCode)VALUES (@line,@machine,@shift,@alarm,@start,@end,@timestamp,@date,@companycode,@plantcode) ";
                using (SqlConnection connection = new SqlConnection(con_string))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                        sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                        sqlCommand.Parameters.Add("@shift", SqlDbType.NVarChar).Value = (object)Shift_ID;
                        sqlCommand.Parameters.Add("@alarm", SqlDbType.NVarChar).Value = (object)Alarm_ID;
                        sqlCommand.Parameters.Add("@start", SqlDbType.DateTime).Value = (object)Start_Time;
                        sqlCommand.Parameters.Add("@end", SqlDbType.DateTime).Value = (object)End_Time;
                        sqlCommand.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = (object)Time_Stamp;
                        sqlCommand.Parameters.Add("@date", SqlDbType.DateTime).Value = (object)Date;
                        sqlCommand.Parameters.Add("@companycode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@plantcode", SqlDbType.NVarChar).Value = (object)PlantCode;
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                //}
                //else
                //{
                //    string cmdText = "Insert Into dbo.MachineAlarm_Date(Line_Code,Machine_Code,Shift_ID,Alarm_ID,Start_Time,End_Time,Time_Stamp,Date,CompanyCode,PlantCode)VALUES (@line,@machine,@shift,@alarm,@start,@end,@timestamp,@date,@companycode,@plantcode) ";
                //    using (SqlConnection connection = new SqlConnection(this.constring))
                //    {
                //        using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                //        {
                //            sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                //            sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                //            sqlCommand.Parameters.Add("@shift", SqlDbType.NVarChar).Value = (object)Shift_ID;
                //            sqlCommand.Parameters.Add("@alarm", SqlDbType.NVarChar).Value = (object)Alarm_ID;
                //            sqlCommand.Parameters.Add("@start", SqlDbType.NVarChar).Value = (object)Start_Time;
                //            sqlCommand.Parameters.Add("@end", SqlDbType.NVarChar).Value = (object)End_Time;
                //            sqlCommand.Parameters.Add("@timestamp", SqlDbType.NVarChar).Value = (object)Time_Stamp;
                //            sqlCommand.Parameters.Add("@date", SqlDbType.NVarChar).Value = (object)Time_Stamp;
                //            sqlCommand.Parameters.Add("@companycode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                //            sqlCommand.Parameters.Add("@plantcode", SqlDbType.NVarChar).Value = (object)PlantCode;
                //            connection.Open();
                //            sqlCommand.ExecuteNonQuery();
                //            connection.Close();
                //        }
                // }
                // }

                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }

        [Route("api/Values/InsertMachineLoss_Ewon")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage InsertMachineLoss_Ewon(
          string Line_Code,
          string Machine_Code,
          string Shift_ID,
          string Loss_ID,
          DateTime Start_Time,
          DateTime End_Time,
          DateTime Time_Stamp,
          string CompanyCode,
          string PlantCode,
          DateTime Date)
        {

            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                DateTime dateValue;


                bool check2 = DateTime.TryParseExact(End_Time.ToString(),
                              new string[] { "yyyy-MM-dd HH:mm:ss.fff" },
                               CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out dateValue);

                bool check1 = DateTime.TryParse(End_Time.ToString(),
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out dateValue);
                bool check3 = DateTime.TryParse(Date.ToString(),
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out dateValue);
                //if (DateTime.TryParse(Time_Stamp.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue) && DateTime.TryParse(Start_Time.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue) && DateTime.TryParse(End_Time.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue) && DateTime.TryParse(Date.ToString(),
                //                CultureInfo.InvariantCulture,
                //                DateTimeStyles.None,
                //                out dateValue))
                //{

                string cmdText = "Insert Into dbo.MachineLoss(Line_Code,Machine_Code,Shift_ID,Loss_ID,Start_Time,End_Time,Time_Stamp,CompanyCode,PlantCode,Date)VALUES (@line,@machine,@shift,@loss,@start,@end,@timestamp,@companycode,@plantcode,@Date) ";
                using (SqlConnection connection = new SqlConnection(con_string))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                        sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                        sqlCommand.Parameters.Add("@shift", SqlDbType.NVarChar).Value = (object)Shift_ID;
                        sqlCommand.Parameters.Add("@loss", SqlDbType.NVarChar).Value = (object)Loss_ID;
                        sqlCommand.Parameters.Add("@start", SqlDbType.DateTime).Value = (object)Start_Time;
                        sqlCommand.Parameters.Add("@end", SqlDbType.DateTime).Value = (object)End_Time;
                        sqlCommand.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = (object)Time_Stamp;
                        sqlCommand.Parameters.Add("@companycode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@plantcode", SqlDbType.NVarChar).Value = (object)PlantCode;
                        sqlCommand.Parameters.Add("@Date", SqlDbType.NVarChar).Value = (object)Date;
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }


                //}
                //else
                //{
                //    string cmdText = "Insert Into dbo.MachineLoss_Date(Line_Code,Machine_Code,Shift_ID,Loss_ID,Start_Time,End_Time,Time_Stamp,CompanyCode,PlantCode,Date)VALUES (@line,@machine,@shift,@loss,@start,@end,@timestamp,@companycode,@plantcode,@Date) ";
                //    using (SqlConnection connection = new SqlConnection(this.constring))
                //    {
                //        using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                //        {
                //            sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                //            sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                //            sqlCommand.Parameters.Add("@shift", SqlDbType.NVarChar).Value = (object)Shift_ID;
                //            sqlCommand.Parameters.Add("@loss", SqlDbType.NVarChar).Value = (object)Loss_ID;
                //            sqlCommand.Parameters.Add("@start", SqlDbType.NVarChar).Value = (object)Start_Time;
                //            sqlCommand.Parameters.Add("@end", SqlDbType.NVarChar).Value = (object)End_Time;
                //            sqlCommand.Parameters.Add("@timestamp", SqlDbType.NVarChar).Value = (object)Time_Stamp;
                //            sqlCommand.Parameters.Add("@companycode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                //            sqlCommand.Parameters.Add("@plantcode", SqlDbType.NVarChar).Value = (object)PlantCode;
                //            sqlCommand.Parameters.Add("@Date", SqlDbType.NVarChar).Value = (object)Date;
                //            connection.Open();
                //            sqlCommand.ExecuteNonQuery();
                //            connection.Close();
                //        }
                //    }
                //}

                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }

        [Route("api/Values/Inserttbl_ANDON_Ewon")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Inserttbl_ANDON_Ewon(
          string Andon_id,
          string Line_Code,
          string Machine_Code,
          DateTime Start_Time,
          DateTime End_Time,
          string AndonReason,
          DateTime Acknowledge_time,
          string FromDepartment,
          string ToDepartment,
          string AndonStatus,
          string CompanyCode,
          string PlantCode)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                string cmdText = "Insert Into dbo.tbl_ANDON(Andon_id,Line_Code,Machine_Code,start_Time,End_Time,AndonReason,Acknowledge_time,FromDepartment,ToDepartment,AndonStatus,CompanyCode,PlantCode)VALUES (@andonid,@line,@machine,@start,@end,@AndonReason,@Acknowledge_time,@FromDepartment,@ToDepartment,@AndonStatus,@companycode,@plantcode) ";
                using (SqlConnection connection = new SqlConnection(con_string))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@andonid", SqlDbType.NVarChar).Value = (object)Andon_id;
                        sqlCommand.Parameters.Add("@line", SqlDbType.NVarChar).Value = (object)Line_Code;
                        sqlCommand.Parameters.Add("@machine", SqlDbType.NVarChar).Value = (object)Machine_Code;
                        sqlCommand.Parameters.Add("@start", SqlDbType.DateTime).Value = (object)Start_Time;
                        sqlCommand.Parameters.Add("@end", SqlDbType.DateTime).Value = (object)End_Time;
                        sqlCommand.Parameters.Add("@AndonReason", SqlDbType.NVarChar).Value = (object)AndonReason;
                        sqlCommand.Parameters.Add("@Acknowledge_time", SqlDbType.DateTime).Value = (object)End_Time;
                        sqlCommand.Parameters.Add("@FromDepartment", SqlDbType.NVarChar).Value = (object)FromDepartment;
                        sqlCommand.Parameters.Add("@ToDepartment", SqlDbType.NVarChar).Value = (object)ToDepartment;
                        sqlCommand.Parameters.Add("@AndonStatus", SqlDbType.NVarChar).Value = (object)AndonReason;
                        sqlCommand.Parameters.Add("@companycode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@plantcode", SqlDbType.NVarChar).Value = (object)PlantCode;
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }


        ///------we are not using this for dieset------///

        [Route("api/Values/Inserttbl_Dieset_Ewon")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Inserttbl_Dieset_Ewon(
         DateTime Date,
         string ToolID,
         DateTime Dieset_loaded_time,
         DateTime Dieset_Unloaded_time,
         DateTime Start_Time,
         DateTime Stop_Time,
         int Production_Qty,
         int Cummulative_Qty,
         string Reason_for_dieset_removal,
         string Machine_code,
         string Line_code,
         string CompanyCode,
         string PlantCode)
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                string cmdText = "Insert Into dbo.Dieset_history(Date,ToolID,Dieset_loaded_time,Dieset_Unloaded_time,Start_Time,Stop_Time,Production_Qty,Cummulative_Qty,Reason_for_dieset_removal,Machine_code,Line_code,CompanyCode,PlantCode)VALUES (@Date,@ToolID,@Dieset_loaded_Time,@Dieset_Unloaded_Time,@Start_Time,@Stop_Time,@Production_Qty,@Cummulative_Qty,@Reason,@Machine_code,@Line_code,@companycode,@plantcode) ";
                using (SqlConnection connection = new SqlConnection(con_string))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = (object)Date;
                        sqlCommand.Parameters.Add("@ToolID", SqlDbType.NVarChar).Value = (object)ToolID;
                        sqlCommand.Parameters.Add("@Dieset_loaded_Time", SqlDbType.DateTime).Value = (object)Dieset_loaded_time;
                        sqlCommand.Parameters.Add("@Dieset_Unloaded_Time", SqlDbType.DateTime).Value = (object)Dieset_Unloaded_time;
                        sqlCommand.Parameters.Add("@Start_Time", SqlDbType.DateTime).Value = (object)Start_Time;
                        sqlCommand.Parameters.Add("@Stop_Time", SqlDbType.DateTime).Value = (object)Stop_Time;
                        sqlCommand.Parameters.Add("@Production_Qty", SqlDbType.Int).Value = (object)Production_Qty;
                        sqlCommand.Parameters.Add("@Cummulative_Qty", SqlDbType.Int).Value = (object)Cummulative_Qty;
                        sqlCommand.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = (object)Reason_for_dieset_removal;
                        sqlCommand.Parameters.Add("@Machine_code", SqlDbType.NVarChar).Value = (object)Machine_code;
                        sqlCommand.Parameters.Add("@Line_code", SqlDbType.NVarChar).Value = (object)Line_code;
                        sqlCommand.Parameters.Add("@companycode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@plantcode", SqlDbType.NVarChar).Value = (object)PlantCode;

                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }

        [Route("api/Values/Inserttbl_Dieset_Replacement_Ewon")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Inserttbl_Dieset_Replacement_Ewon(
        DateTime Date,
        string ToolID,
        string Machine_code,
         string Line_code,
        string CompanyCode,
        string PlantCode,
        DateTime Dieset_Loaded_Time,
        DateTime Dieset_Unloaded_Time,
        int Production_Qty,
        int Cummulative_Qty,
        string Reason_For_Dieset_Change,
        DateTime Time_Stamp,
        int Instance
        )
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                string cmdText = "Insert Into dbo.DiesetReplacement_Rawtable(Date,ToolID,Machine_code,Line_code,PlantCode,CompanyCode,Dieset_Loaded_Time, Dieset_Unloaded_Time,Reason_For_Dieset_Change,Instance,Production_Qty,Cummulative_Qty,Time_Stamp)VALUES (@Date,@ToolID,@Machine_code,@Line_code,@PlantCode,@CompanyCode,@Loaded_Time,@Unloaded_Time,@Reason,@Instance,@Production_Qty,@Cummulative_Qty,@Time_Stamp) ";
                using (SqlConnection connection = new SqlConnection(con_string))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = (object)Date;
                        sqlCommand.Parameters.Add("@ToolID", SqlDbType.NVarChar).Value = (object)ToolID;
                        sqlCommand.Parameters.Add("@Loaded_Time", SqlDbType.DateTime).Value = (object)Dieset_Loaded_Time;
                        sqlCommand.Parameters.Add("@Unloaded_Time", SqlDbType.DateTime).Value = (object)Dieset_Unloaded_Time;
                        sqlCommand.Parameters.Add("@Instance", SqlDbType.Int).Value = (object)Instance;
                        sqlCommand.Parameters.Add("@Production_Qty", SqlDbType.Int).Value = (object)Production_Qty;
                        sqlCommand.Parameters.Add("@Cummulative_Qty", SqlDbType.Int).Value = (object)Cummulative_Qty;
                        sqlCommand.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = (object)Reason_For_Dieset_Change;
                        sqlCommand.Parameters.Add("@Machine_code", SqlDbType.NVarChar).Value = (object)Machine_code;
                        sqlCommand.Parameters.Add("@Line_code", SqlDbType.NVarChar).Value = (object)Line_code;
                        sqlCommand.Parameters.Add("@CompanyCode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@PlantCode", SqlDbType.NVarChar).Value = (object)PlantCode;
                        sqlCommand.Parameters.Add("@Time_Stamp", SqlDbType.DateTime).Value = (object)Time_Stamp;

                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }

        [Route("api/Values/Inserttbl_Dieset_stopandstart_Ewon")]
        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage Inserttbl_Dieset_stopandstart_Ewon(
        DateTime Date,
        string ToolID,
        string Machine_code,
         string Line_code,
        string CompanyCode,
        string PlantCode,
        DateTime Start_Time,
        DateTime EndTime,
        int Production_Qty,
        int Cummulative_Qty,
        int Replacement_code,
        DateTime Time_Stamp,
        int Instance
        )
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                string cmdText = "Insert Into dbo.Dieset_stopandstart_Rawtable(Date,ToolID,Machine_code,Line_code,PlantCode,CompanyCode,Start_Time, EndTime,Replacement_code ,Instance,Production_Qty,Cummulative_Qty,Time_Stamp)VALUES (@Date,@ToolID,@Machine_code,@Line_code,@PlantCode,@CompanyCode,@Start_Time,@EndTime,@Replacement_code,@Instance,@Production_Qty,@Cummulative_Qty,@Time_Stamp) ";
                using (SqlConnection connection = new SqlConnection(this.constring))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = (object)Date;
                        sqlCommand.Parameters.Add("@ToolID", SqlDbType.NVarChar).Value = (object)ToolID;
                        sqlCommand.Parameters.Add("@Start_Time", SqlDbType.DateTime).Value = (object)Start_Time;
                        sqlCommand.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = (object)EndTime;
                        sqlCommand.Parameters.Add("@Instance", SqlDbType.Int).Value = (object)Instance;
                        sqlCommand.Parameters.Add("@Production_Qty", SqlDbType.Int).Value = (object)Production_Qty;
                        sqlCommand.Parameters.Add("@Cummulative_Qty", SqlDbType.Int).Value = (object)Cummulative_Qty;
                        sqlCommand.Parameters.Add("@Replacement_code", SqlDbType.Int).Value = (object)Replacement_code;
                        sqlCommand.Parameters.Add("@Machine_code", SqlDbType.NVarChar).Value = (object)Machine_code;
                        sqlCommand.Parameters.Add("@Line_code", SqlDbType.NVarChar).Value = (object)Line_code;
                        sqlCommand.Parameters.Add("@CompanyCode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@PlantCode", SqlDbType.NVarChar).Value = (object)PlantCode;
                        sqlCommand.Parameters.Add("@Time_Stamp", SqlDbType.DateTime).Value = (object)Time_Stamp;

                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }



        [Route("api/Values/Check_login")]
        [HttpPost]
       
        public string Check_login(string device_id)
        {
            using (SqlConnection connection = new SqlConnection(this.constring))
            {
                connection.Open();
                DataSet dataSet = new DataSet();
                SqlCommand selectCommand = new SqlCommand("SELECT id FROM tbl_Ewon_Details WHERE device_id=@device", connection);
                selectCommand.Parameters.AddWithValue("@device", (object)device_id);
                new SqlDataAdapter(selectCommand).Fill(dataSet);
                return selectCommand.ExecuteScalar() != null ? TokenManager.GenerateToken(device_id) : "Login Failed";
            }
        }

        [Route("api/Values/GetToken")]
        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetToken() => this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");

        [Route("api/Values/InsertAlertRawTable")]
        [HttpPost]
        //[CustomAuthenticationFilter]
        public HttpResponseMessage  InsertAlertRawTable(
            DateTime Time_Stamp,
            DateTime Date,
            string Live_Alarm,
            string Live_Loss,
            string Machine_Status,
            string Shift_Id,
            string Line_Code,
            string Machine_Code,
            string CompanyCode,
            string PlantCode,
            string Status_Alarm,
            string Status_Loss

            )
        {
            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {
                string cmdText = "Insert Into dbo.AlertRAWTable(Time_Stamp,Date,Live_Alarm,Live_Loss,Machine_Status,Shift_Id,Line_Code,Machine_Code, CompanyCode,PlantCode,Status_Alarm,Status_Loss)VALUES (@Time_Stamp,@Date,@Live_Alarm,@Live_Loss,@Machine_Status,@Shift_Id,@Line_Code,@Machine_Code,@CompanyCode,@PlantCode,@Status_Alarm,@Status_Loss) ";
                using (SqlConnection connection = new SqlConnection(con_string))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        sqlCommand.Parameters.Add("@Time_Stamp", SqlDbType.DateTime).Value = (object)Time_Stamp;
                        sqlCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = (object)Date;
                        sqlCommand.Parameters.Add("@Live_Alarm", SqlDbType.NVarChar).Value = (object)Live_Alarm;
                        sqlCommand.Parameters.Add("@Live_Loss", SqlDbType.NVarChar).Value = (object)Live_Loss;
                        sqlCommand.Parameters.Add("@Machine_Status", SqlDbType.NVarChar).Value = (object)Machine_Status;
                        sqlCommand.Parameters.Add("@Shift_Id", SqlDbType.NVarChar).Value = (object)Shift_Id;
                        sqlCommand.Parameters.Add("@Line_Code", SqlDbType.NVarChar).Value = (object)Line_Code;
                        sqlCommand.Parameters.Add("@Machine_Code", SqlDbType.NVarChar).Value = (object)Machine_Code;
                        sqlCommand.Parameters.Add("@CompanyCode", SqlDbType.NVarChar).Value = (object)CompanyCode;
                        sqlCommand.Parameters.Add("@PlantCode", SqlDbType.NVarChar).Value = (object)PlantCode;//Status_Alarm
                        sqlCommand.Parameters.Add("@Status_Alarm", SqlDbType.NVarChar).Value = (object)Status_Alarm;
                        sqlCommand.Parameters.Add("@Status_Loss", SqlDbType.NVarChar).Value = (object)Status_Loss;
                        
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }
        }

        [Route("api/Values/Store_Errors_bitswise")]
        [HttpPost]
        //[CustomAuthenticationFilter]
        public HttpResponseMessage Store_Errors_bitswise(
        string Time_Stamp,
        string Shift_Id,
        string Line_Code,
        string Machine_Code,
        string CompanyCode,
        string PlantCode,
        int bit1,
        int bit2,
        int bit3,
        int bit4,
        int bit5,
        int bit6,
        int bit7,
        int bit8,
        int bit9,
        int bit10,
        int bit11,
        int bit12,
        int bit13,
        int bit14,
        int bit15,
        int bit16,
        int bit17,
        int bit18,
        int bit19,
        int bit20,
        int bit21,
        int bit22,
        int bit23,
        int bit24,
        int bit25,
        int bit26,
        int bit27,
        int bit28,
        int bit29,
        int bit30,
        int bit31


        )
        {

            database_connection d = new database_connection();
            string con_string = d.Getconnectionstring(CompanyCode, PlantCode, Line_Code);
            if (con_string == "0")
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "Error", msg = "Coudnot Connect to database", data = "" });

            }
            else
            {

                //for proper insertion
                DataTable dt = new DataTable();

                dt.Columns.Add("Timestamp");
                dt.Columns.Add("Shift_ID");
                dt.Columns.Add("Machine_code");
                dt.Columns.Add("Line_code");
                dt.Columns.Add("PlantCode");
                dt.Columns.Add("Companycode");

                int k = 1;
                int j = 32;
                for (int i1 = 1; i1 <= 31; i1++)
                {

                    for (int i2 = k; i2 <= j; i2++)
                    {
                        dt.Columns.Add(new DataColumn("E" + i2).ToString().Trim(), typeof(int));
                    }
                    k += 32;
                    j += 32;
                }

                //for improper date
                DataTable dt_temp = new DataTable();
                dt_temp.Columns.Add("Timestamp");
                dt_temp.Columns.Add("Shift_ID");
                dt_temp.Columns.Add("Machine_code");
                dt_temp.Columns.Add("Line_code");
                dt_temp.Columns.Add("PlantCode");
                dt_temp.Columns.Add("Companycode");

                int kk = 1;
                int jj = 32;
                for (int i1 = 1; i1 <= 31; i1++)
                {

                    for (int i2 = kk; i2 <= jj; i2++)
                    {
                        dt_temp.Columns.Add(new DataColumn("E" + i2).ToString().Trim(), typeof(int));
                    }
                    kk += 32;
                    jj += 32;
                }

                var listVariables = new Dictionary<int, int>
                    {
                        { 1, bit1 },
                        { 2, bit2 },
                        { 3, bit3 },
                        { 4, bit4 },
                        { 5, bit5 },
                        { 6, bit6 },
                        { 7, bit7 },
                        { 8, bit8 },
                        { 9, bit9 },
                        { 10, bit10 },
                        { 11, bit11 },
                        { 12, bit12 },
                        { 13, bit13 },
                        { 14, bit14 },
                        { 15, bit15 },
                        { 16, bit16 },
                        { 17, bit17 },
                        { 18, bit18 },
                        { 19, bit19 },
                        { 20, bit20 },
                        { 21, bit21 },
                        { 22, bit22 },
                        { 23, bit23 },
                        { 24, bit24 },
                        { 25, bit25 },
                        { 26, bit26 },
                        { 27, bit27 },
                        { 28, bit28 },
                        { 29, bit29 },
                        { 30, bit30 },
                        { 31, bit31 },
                    };

                DataRow dc = dt.NewRow();
                DataRow dc_temp = dt_temp.NewRow();
                DateTime dateValue;

                if ((DateTime.TryParse(Time_Stamp.ToString(),
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out dateValue)))
                {

                    dc["Timestamp"] = Time_Stamp.Replace("T", " ").ToString();
                    dc["Shift_ID"] = Shift_Id;
                    dc["Machine_code"] = Machine_Code;
                    dc["Line_code"] = Line_Code;
                    dc["PlantCode"] = PlantCode;
                    dc["Companycode"] = CompanyCode;

                    int kk1 = 0;
                    for (int i1 = 1; i1 <= 31; i1++)
                    {

                        int number = listVariables[i1];
                        string result2 = Reverse(Convert.ToString(number, 2).PadLeft(32, '0'));
                        for (int i2 = 0; i2 < result2.Length; i2++)
                        {
                            kk1++;
                            string ee = "E" + kk1 + "";
                            dc[ee] = result2[i2].ToString();

                        }

                    }



                    dt.Rows.Add(dc);

                }
                else
                {

                    dc_temp["Timestamp"] = Time_Stamp.Replace("T", " ").ToString();
                    dc_temp["Shift_ID"] = Shift_Id;
                    dc_temp["Machine_code"] = Machine_Code;
                    dc_temp["Line_code"] = Line_Code;
                    dc_temp["PlantCode"] = PlantCode;
                    dc_temp["Companycode"] = CompanyCode;

                    int kk1 = 0;
                    for (int i1 = 1; i1 <= 31; i1++)
                    {
                        string gg = "bit" + i1;
                        int number = Convert.ToInt32("bit" + i1);
                        string result2 = Reverse(Convert.ToString(number, 2).PadLeft(32, '0'));
                        for (int i2 = 0; i2 < result2.Length; i2++)
                        {
                            kk1++;
                            string ee = "E" + kk1 + "";
                            dc_temp[ee] = result2[i2].ToString();

                        }

                    }


                    dt_temp.Rows.Add(dc_temp);
                }


                if (dt.Rows.Count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(con_string))
                    {
                        using (SqlBulkCopy sqlBulk = new SqlBulkCopy(con_string, SqlBulkCopyOptions.FireTriggers))
                        {
                            conn.Open();
                            sqlBulk.DestinationTableName = "Errors";
                            sqlBulk.BatchSize = dt.Rows.Count;
                            sqlBulk.BulkCopyTimeout = 6000000;


                            sqlBulk.ColumnMappings.Clear();

                            foreach (DataColumn column in dt.Columns)
                            {
                                sqlBulk.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                            }


                            try
                            {
                                sqlBulk.WriteToServer(dt);
                                dt.Dispose();

                            }
                            catch (Exception e)
                            {
                                return this.Request.CreateResponse<string>(HttpStatusCode.OK, e.ToString());
                            }
                            //sqlBulk.WriteToServer(dt);
                            //dt.Dispose();
                            conn.Close();
                        }

                    }

                }

                //sql insetion for improper date format
                if (dt_temp.Rows.Count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(con_string))
                    {
                        using (SqlBulkCopy sqlBulk = new SqlBulkCopy(con_string, SqlBulkCopyOptions.FireTriggers))
                        {
                            conn.Open();
                            sqlBulk.DestinationTableName = "Errors_Date";
                            sqlBulk.BatchSize = dt_temp.Rows.Count;
                            sqlBulk.BulkCopyTimeout = 6000000;


                            sqlBulk.ColumnMappings.Clear();

                            foreach (DataColumn column in dt_temp.Columns)
                            {
                                sqlBulk.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                            }


                            try
                            {
                                sqlBulk.WriteToServer(dt_temp);
                                dt_temp.Dispose();
                            }
                            catch (Exception e)
                            {
                                return this.Request.CreateResponse<string>(HttpStatusCode.OK, e.ToString());
                            }
                            //sqlBulk.WriteToServer(dt);
                            //dt.Dispose();
                            conn.Close();
                        }

                    }

                }



                return this.Request.CreateResponse<string>(HttpStatusCode.OK, "Successfully valid");
            }

        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
