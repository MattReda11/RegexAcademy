﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegexAcademy.Models
{
    public class Course
    {
        public Course()
        {

        }
        public Course(string courseName, DateTime startDate, DateTime endDate, WeekdayEnum weekday, DateTime startTime, DateTime endTime)
        {
            CourseName = courseName;
            StartDate = startDate;
            EndDate = endDate;
            Weekday = weekday;
            //Weekday = (WeekdayEnum)Enum.Parse(typeof(WeekdayEnum), weekday);
            StartTime = startTime;
            EndTime = endTime;
        }


        [Key]
        [StringLength(4)]
        public string CourseId { get; set; }

        public int TeacherId { get; set; }


        private string _courseName;
        [Required]
        [StringLength(30)]
        public string CourseName
        {
            get
            {
                return _courseName;
            }
            set
            {
                if (value.Length < 3 || value.Length > 30)
                {
                    throw new ArgumentException("Course name length must be 3-30 characters long");
                }
                if (Globals.HasSpecialChars(value))
                // or Regex.IsMatch(value, @"^([a-zA-Z0-9./,;\-+()*!'""\s])+$")
                {
                    throw new ArgumentException("Course name cannot contain special characters.");
                }
                string CapitalizeFirstCharCourseName = string.Concat(value[0].ToString().ToUpper(), value.Substring(1).ToLower());
                _courseName = CapitalizeFirstCharCourseName;
            }
        }

        private DateTime _startDate;
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Please make sure to select a date.");
                }
                //!!!has to be within 2 weeks of current actual date
                if (value.Year < 1900 || value.Year > 2024)
                {
                    throw new ArgumentException("Please enter a valid start date.");
                }
                _startDate = value;
            }
        }

        private DateTime _endDate;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Please make sure to select a date.");
                }
                //!!!has to be at least one week after start date and cannot be before it
                if (value.Year < 1900 || value.Year > 2024)
                {
                    throw new ArgumentException("Please enter a valid end date.");
                }
                _endDate = value;
            }
        }

        public enum WeekdayEnum
        {
            M = 0,
            Tu = 1,
            W = 2,
            Th = 3,
            F = 4,
            Sa = 5,
            Su = 6
        }

        [Required]
        [EnumDataType(typeof(WeekdayEnum))]
        public WeekdayEnum Weekday { get; set; }

        private DateTime _startTime;
        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Please make sure to select a start time.");
                }
                //!!!has to be from 7am to 7pm
                //if ()
                //{
                //    throw new ArgumentException("Please enter a valid start time.");
                //}
                _startTime = value;
            }
        }

        private DateTime _endTime;
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Please make sure to select an end time.");
                }
                //!!!has to be from 8am to 11pm
                //if ()
                //{
                //    throw new ArgumentException("Please enter a valid end time.");
                //}
                _endTime = value;
            }
        }

        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher Teacher { get; set; }

        public virtual List<StudentCourse> StudentCourse { get; set; }

    }
}
// missing tostring
