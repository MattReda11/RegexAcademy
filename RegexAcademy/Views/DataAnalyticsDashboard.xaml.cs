﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RegexAcademy.Models;
using System.Text;
using System.IO;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for DataAnalyticsDashboard.xaml
    /// </summary>
    public partial class DataAnalyticsDashboard : Page
    {
        public DataAnalyticsDashboard()
        {
            InitializeComponent();
        }

        private void BtnSearchRecords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContentForOptionChosen.Source = new Uri("DataAnalyticsWindowSearch.xaml", UriKind.Relative); //IoException, InvalidOperation
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ," + ex.GetType().Name);
            }
        }



        private void BtnOption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //chart to display start/end dates, or num of courses per day
                // MessageBox.Show("Not yet implemented");
                ContentForOptionChosen.Source = new Uri("DataAnalyticsWindowGraphs.xaml", UriKind.Relative); //IoException, InvalidOperation
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ," + ex.GetType().Name);
            }
        }

        private void BtnViewStats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContentForOptionChosen.Source = new Uri("DataAnalyticsWindowStatistics.xaml", UriKind.Relative); //IoException, InvalidOperation
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ," + ex.GetType().Name);
            }
        }

        private void BtnExportToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*|Data Files (*.data)|*.data";

            if(saveFileDialog.ShowDialog() == true)
            {
                List<Student> students = Globals.dbContext.Students.ToList();
                List<Teacher> teachers = Globals.dbContext.Teachers.ToList();
                List<Course> courses = Globals.dbContext.Courses.ToList();
                List<StudentCourse> studentCourses = Globals.dbContext.StudentCourses.ToList();

                DateTime today = DateTime.Now;

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"Data Exported on {today}:\n");
                sb.AppendLine("All Students: ");
                //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                foreach (Student student in students)
                {
                    sb.AppendLine(student.ToString());
                    //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                }

                sb.AppendLine("\nAll Teachers:");
                //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                foreach (Teacher teacher in teachers)
                {
                    sb.AppendLine(teacher.ToString());
                    //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                }

                sb.AppendLine("\nAll Courses:");
                //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                foreach (Course course in courses)
                {
                    sb.AppendLine(course.ToString());
                    //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                }

                sb.AppendLine("\nStudents Currently in Courses:");
                //File.AppendAllText(saveFileDialog.FileName, sb.ToString());
                foreach (StudentCourse studentCourse in studentCourses)
                {
                    sb.AppendLine(studentCourse.ToString());
                }
                   File.WriteAllText(saveFileDialog.FileName, sb.ToString());
            }

        }
    }
}
