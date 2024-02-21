﻿using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.DAL
{
    public class DAL_MIX_DESIGN
    {
        #region Rohit
        public List<MIX_DESIGN_DATA_Dtl> LAB_TEST_SCHEDULE_LIST(decimal? LTS_ID)
        {
            List<MIX_DESIGN_DATA_Dtl> result = new List<MIX_DESIGN_DATA_Dtl>();

            SqlParameter[] param = { new SqlParameter("@LTS_ID", LTS_ID) };

            try
            {
                DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX())
                    .GetDataTable("[AGG].[USP_SELECT_LAB_TEST_SCHEDULE]", CommandType.StoredProcedure, param);
                foreach (DataRow row in dt.Rows)
                {
                    MIX_DESIGN_DATA_Dtl dtl = new MIX_DESIGN_DATA_Dtl
                    {
                        LTS_ID = Convert.ToDecimal(row["LTS_ID"] == DBNull.Value ? 0 : row["LTS_ID"]),
                        TEST_CODE = Convert.ToString(row["SC_REFNO"]),
                        TEST_NAME = Convert.ToString(row["TEST_NAME"]),
                        LabName = Convert.ToString(row["LabName"]),
                        Grade_Id = Convert.ToDecimal(row["Grade_Id"] == DBNull.Value ? 0 : row["Grade_Id"]),
                        custName = Convert.ToString(row["custName"]),
                    };
                    result.Add(dtl);
                }
            }
            catch (Exception ex1)
            {
            }
            return result;
        }

        public MIX_DESIGN_DATA_Dtl LAB_TEST_SCHEDULE(decimal? LTS_ID)
        {
            SqlParameter[] param = { new SqlParameter("@LTS_ID", LTS_ID) };

            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX())
                .GetDataTable("[AGG].[USP_SELECT_LAB_TEST_SCHEDULE]", CommandType.StoredProcedure, param);

            MIX_DESIGN_DATA_Dtl dtl = null;
            if (dt.Rows.Count > 0)
            {
                dtl = new MIX_DESIGN_DATA_Dtl
                {
                    LTS_ID = Convert.ToDecimal(dt.Rows[0]["LTS_ID"] == DBNull.Value ? 0 : dt.Rows[0]["LTS_ID"]),
                    TEST_CODE = Convert.ToString(dt.Rows[0]["SC_REFNO"]),
                    TEST_NAME = Convert.ToString(dt.Rows[0]["TEST_NAME"]),
                    LabName = Convert.ToString(dt.Rows[0]["LabName"]),
                    Grade_Id = Convert.ToDecimal(dt.Rows[0]["Grade_Id"] == DBNull.Value ? 0 : dt.Rows[0]["Grade_Id"]),
                    custName = Convert.ToString(dt.Rows[0]["custName"]),
                };
            }

            return dtl;
        }
        public string INSERT_MIX_DESIGN(string Emp_Code, VM_Mix_Design_Entry _smixDesign, out ResultMessage oblMsg)
        {
            string errorMsg = "";
            oblMsg = new ResultMessage();
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString_SGX()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    int IS_FILE_UPLOAD = 0;

                    string DMS_Path = ConfigurationManager.AppSettings["DMSPATH"].ToString();
                    string filePath = "MIX_DESIGN\\";
                    string directoryPath = DMS_Path + filePath;
                    string ext = "";
                    if (_smixDesign.UploadFile != null)
                    {
                        IS_FILE_UPLOAD = 1;
                        ext = new System.IO.FileInfo(_smixDesign.UploadFile.FileName).Extension;
                    }

                    SqlParameter[] param =
                    { 
                      new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                     ,new SqlParameter("@TM_ID", SqlDbType.Decimal) 
                     ,new SqlParameter("@TM_CODE", SqlDbType.VarChar, 25)  
                     ,new SqlParameter("@TRIAL_DATE", _smixDesign.TrailDate)
                     ,new SqlParameter("@GRADE_ID", (_smixDesign.Grade_Id == null)?(object)DBNull.Value : _smixDesign.Grade_Id)  
                     ,new SqlParameter("@CLIENT_NAME", (_smixDesign.Client_Name == null)?(object)DBNull.Value : _smixDesign.Client_Name)    
                     ,new SqlParameter("@CUBIC_METER", (_smixDesign.Cubic_Meter == null)?(object)DBNull.Value : _smixDesign.Cubic_Meter)   
                     ,new SqlParameter("@ADDED_BY", Emp_Code) 
                     ,new SqlParameter("@IS_FILE_UPLOAD", IS_FILE_UPLOAD)  
                     ,new SqlParameter("@FILE_PATH", string.IsNullOrEmpty(ext)?(object)DBNull.Value:ext) 
                     ,new SqlParameter("@LTS_REFNO_ID", (_smixDesign.Sc_Ref_ID == null)?(object)DBNull.Value : _smixDesign.Sc_Ref_ID)   

                    };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;
                    param[2].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[AGG].[USP_INSERT_MIX_DESIGN_HDR]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    decimal TM_ID = (decimal)command.Parameters["@TM_ID"].Value;
                    string TM_CODE = (string)command.Parameters["@TM_CODE"].Value;
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;

                    if (TM_ID == -1) { errorMsg = error_1; }

                    if (TM_ID > 0)
                    {
                        if (_smixDesign.Material_Data != null)
                        {
                            foreach (VM_MixDesign_Material item in _smixDesign.Material_Data)
                            {

                                SqlParameter[] param2 =
                                    {
                                       new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                      ,new SqlParameter("@TMD_ID", SqlDbType.Decimal)  
                                      ,new SqlParameter("@TM_ID", TM_ID)  
                                      ,new SqlParameter("@MATERIAL_ID", (item.Material_Id == null) ? 0 : item.Material_Id)
                                      ,new SqlParameter("@BRAND" , (item.Brand == null)? "" : item.Brand) 
                                      ,new SqlParameter("@UNIT", (item.Unit == null)? "" : item.Unit) 
                                      ,new SqlParameter("@SSD_MIX", (item.SSD_Mix == null)? 0 : item.SSD_Mix) 
                                      ,new SqlParameter("@ABSORPTION", (item.Absorption == null)? 0 : item.Absorption)  

                                      ,new SqlParameter("@MOISTURE", (item.Moisture == null) ? 0 : item.Moisture)
                                      ,new SqlParameter("@CORRECTION_VALUE" , (item.Correction_Value == null)? 0 : item.Correction_Value) 
                                      ,new SqlParameter("@CORRECT_MIX", (item.Correct_Mix == null)? 0 : item.Correct_Mix) 
                                      ,new SqlParameter("@TRIAL_BATCH", (item.TRIAL_BATCH == null)? 0 : item.TRIAL_BATCH) 
                                      ,new SqlParameter("@MATERIAL_BATCH_NO", (item.MaterialBatch_No == null)? "" : item.MaterialBatch_No)  

                                    };
                                param2[0].Direction = ParameterDirection.Output;
                                param2[1].Direction = ParameterDirection.Output;
                                new DataAccess().InsertWithTransaction("[AGG].[USP_INSERT_MIX_DESIGN_DTL]", CommandType.StoredProcedure, out command, connection, transactionScope, param2);
                                decimal TMD_ID = (decimal)command.Parameters["@TMD_ID"].Value;
                                string error_2 = (string)command.Parameters["@ERRORSTR"].Value;
                                if (TMD_ID == -1) { errorMsg = error_2; break; }
                            }
                        }

                        if (_smixDesign.Remarks_Data != null)
                        {
                            foreach (VM_MixDesign_Remarks item in _smixDesign.Remarks_Data)
                            {
                                if (!string.IsNullOrEmpty(item.Time))//|| !string.IsNullOrEmpty(item.Slump) || !string.IsNullOrEmpty(item.Unit) || !string.IsNullOrEmpty(item.Notes)
                                {
                                    SqlParameter[] param3 =
                                    {
                                       new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                      ,new SqlParameter("@TMR_ID", SqlDbType.Decimal)  
                                      ,new SqlParameter("@TM_ID", TM_ID)  
                                      ,new SqlParameter("@DESCRIPTION", (item.Time == null) ? "" : item.Time)                                      
                                      ,new SqlParameter("@VALUE",  (item.IsSelected)?   "Collapse" :  ((item.Slump == null) ? "" : item.Slump))
                                     //new SqlParameter("@VALUE",  (item.Slump == null) ? "" : item.Slump)
                                     
                                     
                                      ,new SqlParameter("@UNIT", (item.Unit == null)? "" : item.Unit) 
                                      ,new SqlParameter("@NOTES", (item.Notes == null)?"" : item.Notes)                                    
                                    };
                                    param3[0].Direction = ParameterDirection.Output;
                                    param3[1].Direction = ParameterDirection.Output;
                                    new DataAccess().InsertWithTransaction("[AGG].[USP_INSERT_MIX_DESIGN_REMARKS]", CommandType.StoredProcedure, out command, connection, transactionScope, param3);
                                    decimal TMR_ID = (decimal)command.Parameters["@TMR_ID"].Value;
                                    string error_3 = (string)command.Parameters["@ERRORSTR"].Value;
                                    if (TMR_ID == -1) { errorMsg = error_3; break; }
                                }
                            }
                        }
                    }

                    if (errorMsg == "")
                    {
                        // Below code is used for attached slip file
                        if (_smixDesign.UploadFile != null)
                        {
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            if (_smixDesign.UploadFile != null)
                            {
                                string fileName = TM_CODE.Replace("/", "_") + ext;
                                _smixDesign.UploadFile.SaveAs(directoryPath + fileName);
                            }
                        }
                        oblMsg.ID = TM_ID;
                        oblMsg.CODE = TM_CODE;
                        oblMsg.MsgType = "Success";
                        transactionScope.Commit();
                    }
                    else
                    {
                        oblMsg.Msg = errorMsg;
                        oblMsg.MsgType = "Error";
                        transactionScope.Rollback();
                    }

                }
                catch (Exception ex)
                {
                    try
                    {
                        transactionScope.Rollback();
                    }
                    catch (Exception ex1)
                    {
                        errorMsg = "Error: Exception occured. " + ex1.StackTrace.ToString();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }
        #endregion

        #region Mix Design List

        public List<MIX_DESIGN_DATA_LIST> Select_MixDesign_Data_List(VM_MIX_DESIGN_LIST _mixDesign)
        {
            SqlParameter[] param = {  
                                       new SqlParameter("@GRADE_ID", _mixDesign.Grade_Id),
                                       new SqlParameter("@TM_CODE", _mixDesign.TM_CODE),
                                       new SqlParameter("@FROM_DT", _mixDesign.From_DT),
                                       new SqlParameter("@TO_DT", _mixDesign.To_DT) 
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataSet("[AGG].[USP_SELECT_MIX_DESIGN_LIST]", CommandType.StoredProcedure, param);

            List<MIX_DESIGN_DATA_LIST> _list = new List<MIX_DESIGN_DATA_LIST>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    _list.Add(new MIX_DESIGN_DATA_LIST
                    {
                        TM_ID = Convert.ToDecimal(row["TM_ID"]),
                        TM_CODE = Convert.ToString(row["TM_CODE"]),
                        TRIAL_DATE = Convert.ToString(row["TRIAL_DATE"]),
                        Grade_Name = Convert.ToString(row["Grade_Name"]),
                        Client_Name = Convert.ToString(row["Client_Name"]),
                        CUBIC_METER = Convert.ToDecimal(row["CUBIC_METER"]),
                        ADDED_BY_CODE = Convert.ToString(row["ADDED_BY_CODE"]),
                        ADDED_BY = Convert.ToString(row["ADDED_BY"]),
                        Sc_Ref_ID = Convert.ToDecimal(row["LTS_REFNO_ID"] == DBNull.Value ? 0 : row["LTS_REFNO_ID"]),
                        IS_LOCKED = Convert.ToInt32(dt.Rows[0]["IS_LOCKED"]),

                    });
                }
            }

            return _list;
        }
        #endregion

        #region MixDesign View

        public MIX_DESIGN_VIEW VIEW_MIX_DESIGN(decimal TM_ID)
        {
            SqlParameter[] param = { new SqlParameter("@TM_ID", TM_ID) };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataSet("[AGG].[USP_VIEW_MIX_DESIGN]", CommandType.StoredProcedure, param);
            MIX_DESIGN_VIEW _objMixDesign = new MIX_DESIGN_VIEW();

            List<MIX_DESIGN_DTL> _list = new List<MIX_DESIGN_DTL>();
            MIX_DESIGN_DTL dtl = null;

            List<MIX_DESIGN_REMARKS> _listRemarks = new List<MIX_DESIGN_REMARKS>();
            MIX_DESIGN_REMARKS dtlRemarks = null;


            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                _objMixDesign.TM_ID = Convert.ToDecimal(dt.Rows[0]["TM_ID"]);
                _objMixDesign.TM_CODE = Convert.ToString(dt.Rows[0]["TM_CODE"]);
                _objMixDesign.GRADE_NAME = Convert.ToString(dt.Rows[0]["GRADE_NAME"]);
                _objMixDesign.TRIAL_DATE = Convert.ToString(dt.Rows[0]["TRIAL_DATE"]);
                _objMixDesign.Client_Name = Convert.ToString(dt.Rows[0]["Client_Name"]);
                _objMixDesign.ADDED_BY_CODE = Convert.ToString(dt.Rows[0]["ADDED_BY_CODE"]);
                _objMixDesign.ADDED_BY = Convert.ToString(dt.Rows[0]["ADDED_BY"]);
                _objMixDesign.CUBIC_METER = Convert.ToDecimal(dt.Rows[0]["CUBIC_METER"]);
                _objMixDesign.IS_FILE_UPLOAD = Convert.ToInt32(dt.Rows[0]["IS_FILE_UPLOAD"]);
                _objMixDesign.FILE_PATH = Convert.ToString(dt.Rows[0]["FILE_PATH"]);
                _objMixDesign.Sc_Ref_ID = Convert.ToDecimal(dt.Rows[0]["LTS_REFNO_ID"] == DBNull.Value ? 0 : dt.Rows[0]["LTS_REFNO_ID"]);
                _objMixDesign.IS_LOCKED = Convert.ToInt32(dt.Rows[0]["IS_LOCKED"]); 
  
            }

            DataTable dt2 = ds.Tables[1];
            foreach (DataRow row in dt2.Rows)
            {
                dtl = new MIX_DESIGN_DTL();

                dtl.TMD_ID = Convert.ToInt32(row["TMD_ID"] == DBNull.Value ? 0 : row["TMD_ID"]);
                dtl.TM_ID = Convert.ToInt32(row["TM_ID"] == DBNull.Value ? 0 : row["TM_ID"]);
                dtl.MATERIAL_ID = Convert.ToInt32(row["MATERIAL_ID"] == DBNull.Value ? 0 : row["MATERIAL_ID"]);
                dtl.Material_Name = Convert.ToString(row["Material_Name"] == DBNull.Value ? "" : row["Material_Name"]);
                dtl.BRAND = Convert.ToString(row["BRAND"] == DBNull.Value ? "" : row["BRAND"]);
                dtl.UNIT = Convert.ToString(row["UNIT"] == DBNull.Value ? "" : row["UNIT"]);
                dtl.SSD_MIX = Convert.ToDecimal(row["SSD_MIX"] == DBNull.Value ? 0 : row["SSD_MIX"]);
                dtl.ABSORPTION = Convert.ToDecimal(row["ABSORPTION"] == DBNull.Value ? 0 : row["ABSORPTION"]);
                dtl.MOISTURE = Convert.ToDecimal(row["MOISTURE"] == DBNull.Value ? 0 : row["MOISTURE"]);
                dtl.CORRECTION_VALUE = Convert.ToDecimal(row["CORRECTION_VALUE"] == DBNull.Value ? 0 : row["CORRECTION_VALUE"]);
                dtl.CORRECT_MIX = Convert.ToDecimal(row["CORRECT_MIX"] == DBNull.Value ? 0 : row["CORRECT_MIX"]);
                dtl.TRIAL_BATCH = Convert.ToDecimal(row["TRIAL_BATCH"] == DBNull.Value ? 0 : row["TRIAL_BATCH"]);
                dtl.MATERIAL_BATCH_NO = Convert.ToString(row["MATERIAL_BATCH_NO"] == DBNull.Value ? "" : row["MATERIAL_BATCH_NO"]);


                _list.Add(dtl);

            }

            _objMixDesign.mixDesignDtlList = _list;


            DataTable dt3 = ds.Tables[2];
            foreach (DataRow row in dt3.Rows)
            {
                dtlRemarks = new MIX_DESIGN_REMARKS();

                dtlRemarks.TMR_ID = Convert.ToInt32(row["TMR_ID"] == DBNull.Value ? 0 : row["TMR_ID"]);
                dtlRemarks.TM_ID = Convert.ToInt32(row["TM_ID"] == DBNull.Value ? 0 : row["TM_ID"]);
                dtlRemarks.TIME = Convert.ToString(row["DESCRIPTION"] == DBNull.Value ? "" : row["DESCRIPTION"]);
                dtlRemarks.Slump = Convert.ToString(row["VALUE"] == DBNull.Value ? "" : row["VALUE"]);
                dtlRemarks.UNIT = Convert.ToString(row["UNIT"] == DBNull.Value ? "" : row["UNIT"]);
                dtlRemarks.NOTES = Convert.ToString(row["NOTES"] == DBNull.Value ? "" : row["NOTES"]); 
                _listRemarks.Add(dtlRemarks);
            }

            _objMixDesign.mixDesignRemarksList = _listRemarks;

            DataTable dt4 = ds.Tables[3];
            foreach (DataRow row in dt4.Rows)
            {               
                _objMixDesign.TMS_ID = Convert.ToInt32(row["TMS_ID"] == DBNull.Value ? 0 : row["TMS_ID"]);
                _objMixDesign.TM_ID = Convert.ToInt32(row["TM_ID"] == DBNull.Value ? 0 : row["TM_ID"]);
                _objMixDesign.SAR_1_DAYS = Convert.ToDecimal(row["SAR_1_DAYS"] == DBNull.Value ? 0 : row["SAR_1_DAYS"]);
                _objMixDesign.SAR_3_DAYS = Convert.ToDecimal(row["SAR_3_DAYS"] == DBNull.Value ? 0 : row["SAR_3_DAYS"]);
                _objMixDesign.SAR_7_DAYS = Convert.ToDecimal(row["SAR_7_DAYS"] == DBNull.Value ? 0 : row["SAR_7_DAYS"]);
                _objMixDesign.SAR_14_DAYS = Convert.ToDecimal(row["SAR_14_DAYS"] == DBNull.Value ? 0 : row["SAR_14_DAYS"]);
                _objMixDesign.SAR_28_DAYS = Convert.ToDecimal(row["SAR_28_DAYS"] == DBNull.Value ? 0 : row["SAR_28_DAYS"]);
                _objMixDesign.IS_1_DAYS = Convert.ToDecimal(row["IS_1_DAYS"] == DBNull.Value ? 0 : row["IS_1_DAYS"]);
                _objMixDesign.IS_3_DAYS = Convert.ToDecimal(row["IS_3_DAYS"] == DBNull.Value ? 0 : row["IS_3_DAYS"]);
                _objMixDesign.IS_7_DAYS = Convert.ToDecimal(row["IS_7_DAYS"] == DBNull.Value ? 0 : row["IS_7_DAYS"]);
                _objMixDesign.IS_14_DAYS = Convert.ToDecimal(row["IS_14_DAYS"] == DBNull.Value ? 0 : row["IS_14_DAYS"]);
                _objMixDesign.IS_28_DAYS = Convert.ToDecimal(row["IS_28_DAYS"] == DBNull.Value ? 0 : row["IS_28_DAYS"]);
            }

            return _objMixDesign;
        }


        public string INSERT_MIX_DESIGN_STRENGTH(string Emp_Code, MIX_DESIGN_VIEW _objEdit, out ResultMessage oblMsg)
        {
            string errorMsg = "";
            oblMsg = new ResultMessage();
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString_SGX()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    decimal TM_ID = _objEdit.TM_ID;
                    _objEdit.TM_ID = TM_ID;



                    SqlParameter[] param =
                    { 
                      new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                     ,new SqlParameter("@TMS_ID", SqlDbType.Decimal)  
                      ,new SqlParameter("@TM_ID", TM_ID)
                   
                     ,new SqlParameter("@SAR_1_DAYS", (_objEdit.SAR_1_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_1_DAYS)  
                     ,new SqlParameter("@SAR_3_DAYS", (_objEdit.SAR_3_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_3_DAYS)  
                     ,new SqlParameter("@SAR_7_DAYS", (_objEdit.SAR_7_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_7_DAYS)  
                     ,new SqlParameter("@SAR_14_DAYS", (_objEdit.SAR_14_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_14_DAYS)  
                     ,new SqlParameter("@SAR_28_DAYS", (_objEdit.SAR_28_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_28_DAYS) 
                     ,new SqlParameter("@IS_1_DAYS", (_objEdit.IS_1_DAYS == null)?(object)DBNull.Value : _objEdit.IS_1_DAYS)  
                     ,new SqlParameter("@IS_3_DAYS", (_objEdit.IS_3_DAYS == null)?(object)DBNull.Value : _objEdit.IS_3_DAYS)  
                     ,new SqlParameter("@IS_7_DAYS", (_objEdit.IS_7_DAYS == null)?(object)DBNull.Value : _objEdit.IS_7_DAYS)  
                     ,new SqlParameter("@IS_14_DAYS", (_objEdit.IS_14_DAYS == null)?(object)DBNull.Value : _objEdit.IS_14_DAYS)  
                     ,new SqlParameter("@IS_28_DAYS", (_objEdit.IS_28_DAYS == null)?(object)DBNull.Value : _objEdit.IS_28_DAYS)  

                    };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[AGG].[USP_INSERT_MIX_DESIGN_STRENGTH]", CommandType.StoredProcedure, out command, connection, transactionScope, param);


                    decimal TMS_ID = (decimal)command.Parameters["@TMS_ID"].Value;
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    if (TMS_ID == -1) { errorMsg = error_1; }


                    if (errorMsg == "")
                    {


                        oblMsg.ID = _objEdit.TM_ID;
                        //oblMsg.CODE = _objEdit.TRAIL_NO;
                        oblMsg.MsgType = "Success";
                        transactionScope.Commit();
                    }
                    else
                    {
                        oblMsg.Msg = errorMsg;
                        oblMsg.MsgType = "Error";
                        transactionScope.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        transactionScope.Rollback();
                    }
                    catch (Exception ex1)
                    {
                        errorMsg = "Error: Exception occured. " + ex1.StackTrace.ToString();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }


        public string UPDATE_MIX_DESIGN_STRENGTH(string Emp_Code, MIX_DESIGN_VIEW _objEdit, out ResultMessage oblMsg)
        {
            string errorMsg = "";
            oblMsg = new ResultMessage();
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString_SGX()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    decimal TMS_ID = _objEdit.TMS_ID;
                    _objEdit.TMS_ID = TMS_ID;

                    SqlParameter[] param =
                    { 
                      new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                      ,new SqlParameter("@TMS_ID",TMS_ID)
                     ,new SqlParameter("@TM_ID", _objEdit.TM_ID)  
                     ,new SqlParameter("@SAR_1_DAYS", (_objEdit.SAR_1_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_1_DAYS)  
                     ,new SqlParameter("@SAR_3_DAYS", (_objEdit.SAR_3_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_3_DAYS)  
                     ,new SqlParameter("@SAR_7_DAYS", (_objEdit.SAR_7_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_7_DAYS)  
                     ,new SqlParameter("@SAR_14_DAYS", (_objEdit.SAR_14_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_14_DAYS)  
                     ,new SqlParameter("@SAR_28_DAYS", (_objEdit.SAR_28_DAYS == null)?(object)DBNull.Value : _objEdit.SAR_28_DAYS) 
                     ,new SqlParameter("@IS_1_DAYS", (_objEdit.IS_1_DAYS == null)?(object)DBNull.Value : _objEdit.IS_1_DAYS)  
                     ,new SqlParameter("@IS_3_DAYS", (_objEdit.IS_3_DAYS == null)?(object)DBNull.Value : _objEdit.IS_3_DAYS)  
                     ,new SqlParameter("@IS_7_DAYS", (_objEdit.IS_7_DAYS == null)?(object)DBNull.Value : _objEdit.IS_7_DAYS)  
                     ,new SqlParameter("@IS_14_DAYS", (_objEdit.IS_14_DAYS == null)?(object)DBNull.Value : _objEdit.IS_14_DAYS)  
                     ,new SqlParameter("@IS_28_DAYS", (_objEdit.IS_28_DAYS == null)?(object)DBNull.Value : _objEdit.IS_28_DAYS)  

                    };

                    param[0].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[AGG].[USP_UPDATE_MIX_DESIGN_STRENGTH]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    if (error_1 != "") { errorMsg = error_1; }
                    if (errorMsg == "")
                    {
                        oblMsg.ID = _objEdit.TM_ID;
                        oblMsg.MsgType = "Success";
                        transactionScope.Commit();
                    }
                    else
                    {
                        oblMsg.Msg = errorMsg;
                        oblMsg.MsgType = "Error";
                        transactionScope.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        transactionScope.Rollback();
                    }
                    catch (Exception ex1)
                    {
                        errorMsg = "Error: Exception occured. " + ex1.StackTrace.ToString();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }

        public string UPDATE_MIX_DESIGN_STRENGTH_LOCK(string Emp_Code, decimal TM_ID, out ResultMessage oblMsg)
        {
            string errorMsg = "";
            oblMsg = new ResultMessage();
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString_SGX()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                    { 
                      new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                     ,new SqlParameter("@TM_ID", TM_ID)   
                     ,new SqlParameter("@LOCKED_BY", Emp_Code)   
                    };

                    param[0].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[AGG].[USP_UPDATE_MIX_DESIGN_STRENGTH_LOCK]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    if (error_1 != "") { errorMsg = error_1; }

                    if (errorMsg == "")
                    {
                        oblMsg.ID = TM_ID;
                       
                        oblMsg.MsgType = "Success";
                        transactionScope.Commit();
                    }
                    else
                    {
                        oblMsg.Msg = errorMsg;
                        oblMsg.MsgType = "Error";
                        transactionScope.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        transactionScope.Rollback();
                    }
                    catch (Exception ex1)
                    {
                        errorMsg = "Error: Exception occured. " + ex1.StackTrace.ToString();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }

        #endregion
    }
}
