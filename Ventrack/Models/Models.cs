using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ventrack.Models
{
    /// <summary>
    /// Generic Status Enumeration Type
    /// </summary>
    public enum ItemStatus
    {
        Active = 1,
        Inactive = 2
    }

    public class Department
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [Required]
        public int VentilationCapacity { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public ItemStatus Status { get; set; }
    }

    public class Equipment
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public ItemStatus Status { get; set; }
    }

    public class VentReading
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public Equipment Equipment { get; set; }
        [Required]
        public Department Department { get; set; }
        [Required]
        public int Ventilation { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public ItemStatus Status { get; set; }
    }

    #region Data Transfer Objects
    /// <summary>
    /// Ventillation reading for equipment by departments
    /// </summary>
    public class EquipmentReading
    {
        public Equipment Equipment { get; set; }
        //Department wise reading of respective equipment
        public List<DepReading> DepReadings { get; set; }

        public EquipmentReading()
        {
            DepReadings = new List<DepReading>();
        }
    }

    public class DepReading
    {
        public int ID { get; set; }
        public Department Dep { get; set; }
        public int Reading { get; set; }
        
    }
    #endregion

    #region ViewModels
    public class EquipmentReadingModel
    {
        /// <summary>
        /// Equipment wise ventillation reading for each department
        /// </summary>
        public List<EquipmentReading> Readings { get; set; }
        /// <summary>
        /// Total Capacity, current ventillation per department and Exceed indicator
        /// </summary>
        public List<Tuple<Department, int, bool>> Departments { get; set; }

        public EquipmentReadingModel()
        {
            Readings = new List<EquipmentReading>();
            Departments = new List<Tuple<Department, int, bool>>();
        }
    }
    #endregion

}