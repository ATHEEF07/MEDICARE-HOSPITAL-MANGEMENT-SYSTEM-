using System;
using System.Collections.Generic;
using MEDICARE_HOSPITAL_MANGAMENT.Helpers;
using MEDICARE_HOSPITAL_MANGAMENT.Models;
using MEDICARE_HOSPITAL_MANGAMENT.Repositories;

namespace MEDICARE_HOSPITAL_MANGAMENT.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _deptRepository;

        public DepartmentService(DepartmentRepository? deptRepository = null)
        {
            _deptRepository = deptRepository ?? new DepartmentRepository();
        }

        public List<Department> GetAllDepartments(bool activeOnly = false) => _deptRepository.GetAllDepartments(activeOnly);

        public Department? GetDepartmentById(int id) => _deptRepository.GetDepartmentById(id);

        public bool CreateDepartment(Department dept, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidationHelper.IsNotEmpty(dept.DepartmentName, "Department Name", out errorMessage))
                return false;

            if (_deptRepository.IsDepartmentNameTaken(dept.DepartmentName))
            {
                errorMessage = $"Department '{dept.DepartmentName.Trim()}' already exists.";
                return false;
            }

            try
            {
                int newId = _deptRepository.CreateDepartment(dept);
                dept.DepartmentID = newId;
                return newId > 0;
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to create department: {ex.Message}";
                return false;
            }
        }

        public bool UpdateDepartment(Department dept, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidationHelper.IsNotEmpty(dept.DepartmentName, "Department Name", out errorMessage))
                return false;

            if (_deptRepository.IsDepartmentNameTaken(dept.DepartmentName, dept.DepartmentID))
            {
                errorMessage = $"Department '{dept.DepartmentName.Trim()}' already exists.";
                return false;
            }

            try
            {
                return _deptRepository.UpdateDepartment(dept);
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to update department: {ex.Message}";
                return false;
            }
        }
    }
}
