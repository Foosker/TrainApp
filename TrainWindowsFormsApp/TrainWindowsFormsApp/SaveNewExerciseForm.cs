﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public partial class SaveNewExerciseForm : Form
    {
        MyMessageBox message;

        string pathToExerciseFile;

        public SaveNewExerciseForm()
        {
            InitializeComponent();
        }

        private void SaveNewExercise_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                exercisesTypeListBox.Items.Add((ExercisesType)i);
            }
        }

        private bool IsValid()
        {
            if (String.IsNullOrWhiteSpace(textTextBox.Text))
            {
                message = new MyMessageBox();
                message.ShowText("Введите название упражнения!");
                return false;
            }

            if (!int.TryParse(repeatTextBox.Text, out int result))
            {
                message = new MyMessageBox();
                message.ShowText("Введите количество повторений!");
                return false;
            }

            if (!int.TryParse(maxRepeatTextBox.Text, out result))
            {
                message = new MyMessageBox();
                message.ShowText("Введите максимальное количество повторений!");
                return false;
            }

            if (String.IsNullOrWhiteSpace(loadTextBox.Text))
            {
                message = new MyMessageBox();
                message.ShowText("Введите нагрузку!");
                return false;
            }

            return true;
        }

        private string AnyRemark()
        {
            if (String.IsNullOrWhiteSpace(remarkTextBox.Text)) return null;
            else return remarkTextBox.Text;
        }

        private void exercisesTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pathToExerciseFile = "ExercisesType/" + exercisesTypeListBox.SelectedItem + ".json";
        }

        private List<Exercise> GetListExercises()
        {
            var deserializableData = new List<Exercise>();

            if (FileProvider.TryGet(pathToExerciseFile, out var data))
            {
                data = FileProvider.GetData(pathToExerciseFile);
                deserializableData = JsonConvert.DeserializeObject<List<Exercise>>(data);
            }
            else FileProvider.Save(pathToExerciseFile, "[]");

            return deserializableData;

        }

        private void SaveNewExercise()
        {
            if (IsValid())
            {
                var newExercise = new Exercise(textTextBox.Text,                // Название
                                            int.Parse(repeatTextBox.Text),      // Повторения
                                            int.Parse(maxRepeatTextBox.Text),   // Максимум повторений
                                            loadTextBox.Text,                   // Нагрузка
                                            AnyRemark());                       // Если введено примечание

                var list = GetListExercises();
                if (!CheckCoincidences(list, newExercise))
                {
                    list.Add(newExercise);
                    var serializableList = JsonConvert.SerializeObject(list, Formatting.Indented);
                    FileProvider.Save(pathToExerciseFile, serializableList);
                }
                else
                {
                    message = new MyMessageBox();
                    message.ShowText("Такое упражнение уже есть в списке!");
                }
            }

            textTextBox.Text   = null;
            repeatTextBox.Text = null;
            loadTextBox.Text   = null;
            remarkTextBox.Text = null;

        }

        private bool CheckCoincidences(List<Exercise> list, Exercise newExercise)
        {   // Проверка на отсутствие такого упражнения в списке
            foreach (var exercise in list)
            {
                if (exercise.Text == newExercise.Text) return true;
            }
            return false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveNewExercise();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
