using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UoUWebApp.Context;
using UoUWebApp.Gateway;
using UoUWebApp.Models;

namespace UoUWebApp.Manager
{
    public class TeacherManager
    {
        private UoUDBContext db = new UoUDBContext();
        
        public List<TeacherModel> GetAllTeachersByDeptId(int deptId)
        {
            return new TeacherGateway().GetAllTeachersByDeptId(deptId);
        } 

        /*public List<TeacherCreditDetails> GetAllTeacherCreditDetailsByTeacherId(int teacherId)
        {
            return new TeacherGateway().GetAllTeacherCreditDetailsByTeacherId(teacherId);
        }*/
    }
}