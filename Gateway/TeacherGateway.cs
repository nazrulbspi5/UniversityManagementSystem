using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UoUWebApp.Models;

namespace UoUWebApp.Gateway
{
    public class TeacherGateway : DatabaseGateway
    {
        private TeacherModel GetTeacher()
        {
            ExecuteQuery();
            if (reader.HasRows)
            {
                reader.Read();
                return new TeacherModel
                {
                    TeacherId = Convert.ToInt32(reader["TeacherId"]),
                    TeacherName = reader["TeacherName"].ToString(),
                    TeacherAddress = reader["TeacherAddress"].ToString(),
                    TeacherEmail = reader["TeacherEmail"].ToString(),
                    TeacherContact = reader["TeacherContact"].ToString(),
                    TeacherDesignationId = Convert.ToInt32(reader["TeacherDesignationId"]),
                    TeacherDeptId = Convert.ToInt32(reader["TeacherDeptId"]),
                    TotalCredit = (float)reader["TotalCredit"]
                };
            }
            return null;
        }
        private List<TeacherModel> GetTeachers()
        {
            ExecuteQuery();
            if (reader.HasRows)
            {
                List<TeacherModel> teachers = new List<TeacherModel>();
                while (reader.Read())
                {
                    teachers.Add(new TeacherModel {
                        TeacherId = Convert.ToInt32(reader["TeacherId"]),
                        TeacherName = reader["TeacherName"].ToString(),
                        TeacherAddress = reader["TeacherAddress"].ToString(),
                        TeacherEmail = reader["TeacherEmail"].ToString(),
                        TeacherContact = reader["TeacherContact"].ToString(),
                        TeacherDesignationId = Convert.ToInt32(reader["TeacherDesignationId"]),
                        TeacherDeptId = Convert.ToInt32(reader["TeacherDeptId"]),
                        TotalCredit = float.Parse(reader["TotalCredit"].ToString())
                    });
                }
                return teachers;
            }
            return null;
        }
        public List<TeacherModel> GetAllTeachersByDeptId(int deptId)
        {
            command.CommandText = "SELECT * FROM Teachers WHERE TeacherDeptId = @deptId";
            command.Parameters.Clear();
            command.Parameters.Add("deptId", SqlDbType.Int).Value = deptId;

            return GetTeachers();
        }

        /*public List<TeacherCreditDetails> GetAllTeacherCreditDetailsByTeacherId(int teacherId)
        {
            command.CommandText = "SELECT * FROM vTeacherCreditDetails WHERE TeacherId = @teacherId";
            command.Parameters.Clear();
            command.Parameters.Add("teacherId", SqlDbType.Int).Value = teacherId;
            ExecuteQuery();
            if (reader.HasRows)
            {
                List<TeacherCreditDetails> teachers = new List<TeacherCreditDetails>();
                while (reader.Read())
                {
                    teachers.Add(new TeacherCreditDetails() {
                        Id = Convert.ToInt32(reader["TeacherId"]),
                        Name = reader["TeacherName"].ToString(),
                        DeptId = Convert.ToInt32(reader["DeptId"]),
                        TotalCredits = float.Parse(reader["TotalCredit"].ToString()),
                        CreditTaken = float.Parse(reader["CreditTaken"].ToString())
                    });
                }
                return teachers;
            }
            return null;
        }*/
    }
}